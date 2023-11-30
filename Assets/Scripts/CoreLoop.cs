using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;




public class CoreLoop : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Player playerPrefab;
    public Card cardPrefab;
    public List<Card> deck;

    [Header("Set Dynamically")]
    public int currentPlayerIndex;
    public Player currentPlayer;
    public int roundPlayerIndex;
    public Player roundPlayer;
    public Card targetCard;
    /*public Deck deck;*/
    public int maxPlayers;
    Player[] players = new Player[4];


    // Start is called before the first frame update
    void Start()
    {
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

        targetCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        targetCard.markers = 0;
        targetCard.value = Random.Range(3,35);


        players[0] = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        players[0].playerName = "Slorpo";
        players[0].playerID = 1;
        players[0].playerColor = Color.blue;


        players[1] = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        players[1].playerName = "Krumbo";
        players[1].playerID = 2;
        players[1].playerColor = Color.red;

        players[2] = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        players[2].playerName = "Glumpus";
        players[2].playerID = 3;
        players[2].playerColor = Color.yellow;

        players[3] = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        players[3].playerName = "Jeff";
        players[3].playerID = 4;
        players[3].playerColor = Color.green;

        foreach(Player p in players)
        {
            p.markers = 11;
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


    public void CalculateScore(List<Card> cards, int counters)
    {   
        List<Card> sortedCards = cards.OrderBy(card => card.value).ToList();
        List<List<Card>> streaks = new List<List<Card>>();
        List<Card> currentStreak = new List<Card> { sortedCards[0] };

        for (int i = 1; i < sortedCards.Count; i++)
        {
            if (sortedCards[i].value == currentStreak.Select(card=>card.value).ToList().Max() + 1)
            {
                currentStreak.Add(sortedCards[i]);
            }
            else
            {
                streaks.Add(currentStreak);
                currentStreak = new List<Card> { sortedCards[i] };
            }
        }

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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            NoThanks();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeCard();
        }

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    // Pass turn test
        //    if (currentPlayerIndex >= maxPlayers - 1) { currentPlayerIndex = 0; }
        //    else { currentPlayerIndex++; }
        //    currentPlayer = players[currentPlayerIndex];

        //    PrintGameState();
        //}
    }

    public void NoThanks()
    {
        // Player refuses to take the card
        if (currentPlayer.markers < 1)
        {
            Debug.Log($"{currentPlayer.playerName} is out of markers!");
        }
        else
        {
            Debug.Log($"{currentPlayer.playerName} says NO THANKS!");
            targetCard.markers += 1;
            currentPlayer.RemoveMarker();
            if (currentPlayerIndex >= maxPlayers - 1) { currentPlayerIndex = 0; }
            else { currentPlayerIndex++; }
            currentPlayer = players[currentPlayerIndex];
            PrintGameState();
        }
    }

    public void TakeCard()
    {
        // Player accepts the card
        currentPlayer.AddMarkers(targetCard.markers);
        currentPlayer.TakeCard(targetCard);
        targetCard.markers = 0;
        Debug.Log($"{currentPlayer.playerName} has taken the {targetCard.value} card!");

        targetCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity); ;
        targetCard.markers = 0;
        targetCard.value = Random.Range(3, 35);
        if (currentPlayerIndex >= maxPlayers - 1) { currentPlayerIndex = 0; }
        else { currentPlayerIndex++; }
        currentPlayer = players[currentPlayerIndex];

        PrintGameState();

        NewRound();
    }

    public void NewRound()
    {
        //check if deck is empty
        //if empty, go to scoring

        if (roundPlayerIndex >= maxPlayers - 1) { roundPlayerIndex = 0; }
        else { roundPlayerIndex++; }
        roundPlayer = players[roundPlayerIndex];
        currentPlayerIndex = roundPlayerIndex;
        currentPlayer = players[currentPlayerIndex];
    }

    void PrintGameState()
    {
        string heldCards = "";
        foreach (Card card in currentPlayer.cards)
        {
            heldCards += $"{card.value} ";
        }
        Debug.Log($"Current player is now: {currentPlayer.playerName}. They have the following cards: [{heldCards}]. They have {currentPlayer.markers} markers. The current target card is the {targetCard.value} card and has {targetCard.markers} markers on it.");
    }
}
