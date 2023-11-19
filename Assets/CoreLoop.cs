using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public enum PlayerType
{
    Human,
    Bot
}
public enum BotPlayLevel
{
    easy,medium, hard
}

public class Card
{
    public int value;
    public int markers;
}
public class Player
{
    public int playerID;
    public string playerName;
    public Color playerColor;
    public PlayerType type;
    public int markers;

    public void AddMarkers(int quantity)
    {

        markers += quantity;
        Debug.Log($"Ding! {playerName} got {quantity} markers.");
    }
    public void RemoveMarker() { 
        if (markers > 0) markers--;
        Debug.Log($"{playerName} spent a marker. They have {markers} markers left.");
    }

}
public class CoreLoop : MonoBehaviour
{
    public int currentPlayerIndex;
    public Player currentPlayer;
    public Card targetCard;
    public int maxPlayers;
    Player[] players = new Player[4];
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerIndex = 0;
        
        maxPlayers = 4;

        targetCard = new Card();
        targetCard.markers = 0;
        targetCard.value = (int)Mathf.Round(Random.Range(3f,35f));


        players[0] = new Player();
        players[0].playerName = "Slorpo";
        players[0].playerID = 1;
        players[0].playerColor = Color.blue;


        players[1] = new Player();
        players[1].playerName = "Krumbo";
        players[1].playerID = 2;
        players[1].playerColor = Color.red;

        players[2] = new Player();
        players[2].playerName = "Glumpus";
        players[2].playerID = 3;
        players[2].playerColor = Color.yellow;

        players[3] = new Player();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentPlayer.AddMarkers(targetCard.markers);
            targetCard.markers = 0;
            Debug.Log($"{currentPlayer.playerName} has taken the {targetCard.value} card!");

            targetCard = new Card();
            targetCard.markers = 0;
            targetCard.value = (int)Mathf.Round(Random.Range(3f, 35f));
            if (currentPlayerIndex >= maxPlayers - 1) { currentPlayerIndex = 0; }
            else { currentPlayerIndex++; }
            currentPlayer = players[currentPlayerIndex];

            PrintGameState();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentPlayerIndex >= maxPlayers - 1) { currentPlayerIndex = 0; }
            else { currentPlayerIndex++; }
            currentPlayer = players[currentPlayerIndex];

            PrintGameState();
        }
    }

    void PrintGameState()
    {
        Debug.Log($"Current player is now: {currentPlayer.playerName}. They have {currentPlayer.markers} markers. The current target card is the {targetCard.value} card and has {targetCard.markers} markers on it.");
    }
}
