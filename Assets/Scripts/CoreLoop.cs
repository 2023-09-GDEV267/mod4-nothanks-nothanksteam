using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEditor;

public enum GameState
{
    playing,
    standby
} 
public class CoreLoop : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Player playerPrefab;
    public Card cardPrefab;
    public List<GameObject> playerAnchors;
    public Transform markersAnchor;
    public GameObject markerPrefab;
    public float markerSpriteScatter = .75f; // Range in which markers placed on target card scatter from origin anchor
    public float roundSpeed = 1f; // Invoke delay on bot turns
    public List<GameObject> playerUI; // A list of UI elements to hide when it is not the human player's turn
    public TextMeshProUGUI roundPlayerText;
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI winText;

    [Header("Set Dynamically")]
    public GameState gameState = GameState.standby;
    public int currentPlayerIndex;
    public Player currentPlayer;
    public int roundPlayerIndex;
    public Player roundPlayer;
    public Deck deck;
    public Card targetCard;
    public int maxPlayers;
    Player[] players = new Player[4];
    public static CoreLoop S;

    private void Awake()
    {
        if (S == null) { S = this; }
        deck = GetComponent<Deck>();
    }

    public void StartGame()
    {
        // Ensure we can't call StartGame while there is currently already a game in progress
        if (gameState == GameState.standby)
        {
            winText.gameObject.SetActive(false);
            deck.InitializeCards();
            deck.Shuffle(ref deck.cards);
            deck.BurnCards();
            gameState = GameState.playing;
            currentPlayerIndex = 0;

            maxPlayers = 4;



            players[0] = Instantiate(playerPrefab, new Vector3(0, -10, 0), Quaternion.identity);
            players[0].playerName = "Human Player";
            players[0].playerID = 1;
            players[0].playerColor = Color.blue;
            players[0].type = PlayerType.Human;


            players[1] = Instantiate(playerPrefab, new Vector3(-10, 0, 0), Quaternion.identity);
            players[1].playerName = "Krumbo";
            players[1].playerID = 2;
            players[1].playerColor = Color.red;
            players[1].type = PlayerType.Bot;

            players[2] = Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity);
            players[2].playerName = "Glumpus";
            players[2].playerID = 3;
            players[2].playerColor = Color.yellow;
            players[2].type = PlayerType.Bot;

            players[3] = Instantiate(playerPrefab, new Vector3(10, 0, 0), Quaternion.identity);
            players[3].playerName = "Jeff";
            players[3].playerID = 4;
            players[3].playerColor = Color.green;
            players[3].type = PlayerType.Bot;

            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.parent = playerAnchors[i].transform;
                players[i].transform.localPosition = Vector3.zero;
            }
            foreach (Player p in players)
            {
                p.markers = 11;
                p.UpdateHeldMarkersDisplay();
                p.gameObject.transform.parent.Find("Canvas").Find("Name").GetComponent<TMP_Text>().text = p.playerName;
            }
            roundPlayer = players[currentPlayerIndex];
            currentPlayer = roundPlayer;
            UpdateUIDisplay();
            AudioManager.S.ShuffleSound();
            targetCard = deck.Draw();
            targetCard.transform.position = Vector3.zero;
            targetCard.transform.localScale = new Vector3(1, -1, 1);
        }
/*        deck.cards.ForEach(card => { card.targetPos = card.transform.parent.position; });*/

    }

    public static List<List<Card>> SortStreaks(List<Card> cards)
    {
        if (cards == null || cards.Count < 1)
        { return null; }
        else if (cards.Count == 1)
        {
            return new List<List<Card>>() { new List<Card>() { cards[0] } };
        }
        List<Card> sortedCards = cards.OrderBy(card => card.value).ToList();
        List<List<Card>> streaks = new List<List<Card>>();
        List<Card> currentStreak = new List<Card> { sortedCards[0] };

        for (int i = 1; i < sortedCards.Count; i++)
        {
            if (sortedCards[i].value == currentStreak.Select(card => card.value).ToList().Max() + 1)
            {
                currentStreak.Add(sortedCards[i]);
            }
            else
            {
                streaks.Add(currentStreak);
                currentStreak = new List<Card> { sortedCards[i] };
            }
        }
        streaks.Add(currentStreak);
        return streaks;
    }

    public static int CalculateScore(List<Card> cards, int counters)
    {
        List<List<Card>> streaks = new List<List<Card>>();
        int totalScore = 0;
        if (cards != null && cards.Count >= 1)
        {
            streaks = SortStreaks(cards);
            if (streaks != null && streaks.Count > 0)
            {
                foreach (List<Card> streak in streaks)
                {
                    totalScore += streak.Select(card => card.value).ToList().Min();
                }
            }

        }
        totalScore -= counters;
        return totalScore;
    }
    public void UpdateUIDisplay()
    {
        currentPlayerText.text = $"Current Player: {currentPlayer.playerName}";
        roundPlayerText.text = $"Round Player: {roundPlayer.playerName}";
        foreach (GameObject element in playerUI)
        {
            element.SetActive(currentPlayer.type == PlayerType.Human);
        }
        foreach (Player p in players)
        {
            p.currentPlayerSpotlight.SetActive(p == currentPlayer);
            p.gameObject.transform.parent.Find("Canvas").Find("Markers").GetComponent<TMP_Text>().text = $"X{p.markers}";
        }
    }

    public void UpdateTargetCardMarkersDisplayed()
    {
        if (targetCard.markers < 1)
        {
            if (markersAnchor.transform.childCount > 0)
            {
                foreach (Transform child in markersAnchor.transform)
                {
                    Destroy(child.gameObject);
                }
            }

        }
        else
        {
            for (int i = 0; i < targetCard.markers - markersAnchor.transform.childCount; i++)
            {
                GameObject markerSprite = Instantiate(markerPrefab, markersAnchor);
                markerSprite.transform.localPosition = new Vector3(Random.Range(-markerSpriteScatter, markerSpriteScatter), Random.Range(-markerSpriteScatter, markerSpriteScatter), 0);
            }
        }

    }

    public void BotChoice()
    {
        float takeChance = (2.5f * targetCard.value) - (3 * targetCard.markers);

        if (Random.Range(0, 100) > takeChance)
        {
            TakeCard();
        }
        else
        {
            NoThanks();
        }
    }

    public void NoThanks()
    {
        // Player refuses to take the card
        if (currentPlayer.markers < 1)
        {

            AudioManager.S.ErrorSound();

            if (currentPlayer.type == PlayerType.Bot)
            {
                Invoke("TakeCard", roundSpeed);
            }
        }
        else
        {
            Debug.Log($"{currentPlayer.playerName} says NO THANKS!");
            targetCard.markers += 1;
            UpdateTargetCardMarkersDisplayed();
            currentPlayer.RemoveMarker();
            if (currentPlayerIndex >= maxPlayers - 1) { currentPlayerIndex = 0; }
            else { currentPlayerIndex++; }
            currentPlayer = players[currentPlayerIndex];
            /*PrintGameState();*/

            AudioManager.S.PlaceToken();

            if (currentPlayer.type == PlayerType.Bot)
            {
                Invoke("BotChoice", roundSpeed);
            }
        }
        UpdateUIDisplay();
    }

    public void TakeCard()
    {
        // Player accepts the card
        AudioManager.S.CollectTokensSound(targetCard.markers);
        currentPlayer.ReceiveCard(targetCard);
/*        Debug.Log($"{currentPlayer.playerName} has taken the {targetCard.value} card!");*/
        UpdateUIDisplay();
        NewRound();
    }

    public void NewRound()
    {
        foreach (Player player in players)
        {
            AnimateStreaks(player);
        }
        //check if deck is empty
        //if empty, go to scoring
        if (deck.cards.Count <= 0)
        {
            Invoke("FinalScoring", roundSpeed);
        }
        else
        {
            Debug.Log("New Round!");
            targetCard = deck.Draw();
            UpdateTargetCardMarkersDisplayed();
            targetCard.transform.localScale = new Vector3(1,-1,1);
/*            targetCard.targetPos = Vector3.zero;*/
            if (roundPlayerIndex >= maxPlayers - 1) { roundPlayerIndex = 0; }
            else { roundPlayerIndex++; }
            roundPlayer = players[roundPlayerIndex];
            currentPlayerIndex = roundPlayerIndex;
            currentPlayer = players[currentPlayerIndex];
            UpdateUIDisplay() ;
            AudioManager.S.FlipCardSound();
/*            PrintGameState();*/
            if (currentPlayer.type == PlayerType.Bot)
            {
                Invoke("BotChoice", roundSpeed);
            }
        }
    }

    public void AnimateStreaks(Player player)
    {
        Vector3 streakPos = player.cardAnchor.transform.position;
        foreach (List<Card> streak in player.streaks)
        {
            int streakCount = 0;
            int cardCount = 0;

            foreach (Card card in streak)
            {
                card.targetPos = new Vector3(streakPos.x + cardCount, streakPos.y, 0);
                cardCount++;
            }
            streakPos = new Vector3(streakPos.x + cardCount + .5f, player.cardAnchor.transform.position.y, 0);
            cardCount = 0;
            streakCount++;


        }
    }

    public void FinalScoring()
    {
        AudioManager.S.ScoringMusic();
        // Calculate each player's score and display it in the UI
        foreach (Player player in players)
        {
            AnimateStreaks(player);
            player.score = CalculateScore(player.cards, player.markers);
            player.gameObject.transform.parent.Find("Canvas").Find("Score").gameObject.SetActive(true);

            player.gameObject.transform.parent.Find("Canvas").Find("Score").GetComponent<TMP_Text>().text = $"Final Score: {player.score}";
        }

        // Determine the winner(Or winners, if there is a tie)
        List<Player> winners = new List<Player>() { players[0] };
        for (int i = 1; i < players.Length; i++)
        {
            Player scoringPlayer = players[i];
            Player currentWinner = winners[0];
                if ( scoringPlayer.score < currentWinner.score )
                {
                    winners = new List<Player> { scoringPlayer };
                } else if (scoringPlayer.score == currentWinner.score)
                {
                    winners.Add(scoringPlayer);
                }
        }
        string finalScoreString;
        string nameString = "";
        if (winners.Count > 1) 
        {

            foreach (Player player in winners) 
            {
                if (player == winners[winners.Count - 1])
                {
                    nameString += " and";
                }
                else if (player != winners[0])
                {
                    nameString += ",";
                }
                nameString += $" {player.playerName}"; 
            }
            finalScoreString = $"There was a tie! The winners are:{nameString}";
        }
        else
        {
            nameString = winners[0].playerName;
            finalScoreString = $"The winner is {nameString}!";
        }
        winText.gameObject.SetActive(true);
        winText.text = finalScoreString;
    }

    void PrintGameState()
    {
        string heldCards = "";
        foreach (var streak in currentPlayer.streaks)
        {
            foreach (Card card in streak)
            {
                heldCards += $"{card.value} ";
            }
            heldCards += "|";
        }

        Debug.Log($"Current player is now: {currentPlayer.playerName}. They have the following cards: [{heldCards}]. They have {currentPlayer.markers} markers. The current target card is the {targetCard.value} card and has {targetCard.markers} markers on it.");
    }
}
