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

public class CoreLoop : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Player playerPrefab;
    public Card cardPrefab;
    public List<GameObject> playerAnchors;
    public Transform markersAnchor;
    public GameObject markerPrefab;
    public float markerSpriteScatter = .75f;

    [Header("Set Dynamically")]
    public int currentPlayerIndex;
    public Player currentPlayer;
    public int roundPlayerIndex;
    public Player roundPlayer;
    public Deck deck;
    public Card targetCard;
    public int maxPlayers;
    Player[] players = new Player[4];


    void Start()
    {
        deck = GetComponent<Deck>();    

/*        // Fixed list for testing score calculation
        List<int> testList = new List<int> { 32, 29, 35, 21, 20, 22, 15 };
        List<Card> testCards = new List<Card>();

        // Randomized list
        List<Card> randomCards = new List<Card>();
        HashSet<int> randomSet = new HashSet<int>();
        
        //Populating fixed card list
        foreach (int value in testList)
        {
            Card newCard = new Card();
            newCard.value = value;
            testCards.Add(newCard);
        }

        // Generating HashSet of non-repeating random numbers within range 
        for (int i = 0; i < Random.Range(3, 15); i++)
        {

            randomSet.Add(Random.Range(3, 35));
        }
        
        // Creating a set of random cards
        foreach(int value in randomSet)
        {
            Card newCard = new Card();
            newCard.value = value;
            randomCards.Add(newCard);
        }

        // Testing scoring
        Debug.Log("Testing scoring on fixed cards");
        CalculateScore(testCards,0);

        Debug.Log("Testing Scoring on random cards");
        CalculateScore(randomCards, 0);*/


        currentPlayerIndex = 0;
        
        maxPlayers = 4;

        targetCard = deck.Draw();
        targetCard.transform.position = Vector3.zero;
        targetCard.transform.localScale = new Vector3(1,-1,1);


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
            p.gameObject.transform.parent.Find("Canvas").Find("Name").GetComponent<TMP_Text>().text = p.playerName;
        }
        currentPlayer = players[currentPlayerIndex];
        PrintGameState();
    }

    // Old method for calculating score from when I was working with a list of ints
    public void OldCalculateScore(List<int> cardValues)
    {
        
        cardValues.Sort();
        List<List<int>> streaks= new List<List<int>>();
        List<int> currentStreak = new List<int> { cardValues[0] };
        
        for (int i = 1; i < cardValues.Count; i++){
            if( cardValues[i] == currentStreak.Max() + 1)
            {
                currentStreak.Add(cardValues[i]);
            }
            else
            {
                streaks.Add(currentStreak);
                currentStreak = new List<int> { cardValues[i] };
            }
        } 
        
        foreach(List<int> streak in streaks)
        {
            string line = "";
            foreach(int value in streak)
            {
                line += $"{value} ";
            }
            Debug.Log(line);
        }
    }

    public static List<List<Card>> SortStreaks(List<Card> cards)
    {
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
        return streaks;
    }

    public void UpdateTargetCardMarkersDisplayed()
    {
        if (targetCard.markers < 1)
        {
            foreach (Transform child in markersAnchor.transform)
            {
                Destroy(child.gameObject);
            }
        } 
        else
        {
            for (int i = 0; i < targetCard.markers - markersAnchor.transform.childCount; i++)
            {
                GameObject markerSprite = Instantiate(markerPrefab, markersAnchor);
                markerSprite.transform.localPosition = new Vector3(Random.Range(-markerSpriteScatter,markerSpriteScatter), Random.Range(-markerSpriteScatter, markerSpriteScatter), 0);
            }
        }

    }

    public static int CalculateScore(List<Card> cards, int counters)
    {
        List<List<Card>> streaks = SortStreaks(cards);
        int totalScore = 0;
        foreach (List<Card> streak in streaks)
        {
            totalScore += streak.Select(card => card.value).ToList().Min();
        }
        totalScore -= counters;

        // Printing the score and streaks for now. 
        // TODO separate methods for sorting streaks and calculating score
        // (So we can show streaks in a player's hand)
        foreach (List<Card> streak in streaks)
        {
            string line = "";
            foreach (Card card in streak)
            {
                line += $"{card.value} ";
            }
            Debug.Log(line);
        }
        Debug.Log($"The total score was {totalScore}");
        return totalScore;
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log($"{currentPlayer.playerName} is out of markers!");

            AudioManager.S.ErrorSound();

            if (currentPlayer.type == PlayerType.Bot)
            {
                Invoke("TakeCard", 3);
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
            PrintGameState();

            AudioManager.S.PlaceToken();

            if (currentPlayer.type == PlayerType.Bot)
            {
                Invoke("BotChoice", 3);
            }
        }

    }

    public void TakeCard()
    {
        // Player accepts the card
        currentPlayer.AddMarkers(targetCard.markers);
        currentPlayer.ReceiveCard(targetCard);
        AudioManager.S.CollectTokensSound(targetCard.markers);
        targetCard.markers = 0;
        Debug.Log($"{currentPlayer.playerName} has taken the {targetCard.value} card!");



        NewRound();
    }

    public void NewRound()
    {
        //check if deck is empty
        //if empty, go to scoring
        //if (deck.cards.Count <= 0)
        //{
        //    Invoke("FinalScoring", 3);
        //}
        //else
        {
            Debug.Log("New Round!");
            targetCard = deck.Draw();
            UpdateTargetCardMarkersDisplayed();
            targetCard.transform.position = Vector3.zero;
            targetCard.transform.localScale = new Vector3(1,-1,1);
            if (roundPlayerIndex >= maxPlayers - 1) { roundPlayerIndex = 0; }
            else { roundPlayerIndex++; }
            roundPlayer = players[roundPlayerIndex];
            currentPlayerIndex = roundPlayerIndex;
            currentPlayer = players[currentPlayerIndex];
            AudioManager.S.FlipCardSound();
            PrintGameState();
            if (currentPlayer.type == PlayerType.Bot)
            {
                Invoke("BotChoice", 3);
            }
        }
    }

    public void FinalScoring()
    {
        AudioManager.S.ScoringMusic();

        foreach (Player player in players)
        {
            player.score = CalculateScore(player.cards, player.markers);
            player.gameObject.transform.parent.Find("Canvas").Find("Score").gameObject.SetActive(true);
            player.gameObject.transform.parent.Find("Canvas").Find("Score").GetComponent<TMP_Text>().text = $"{player.score}";
        }
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
