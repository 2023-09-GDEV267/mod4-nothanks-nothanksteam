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


public class Player
{
    public int playerID;
    public string playerName;
    public Color playerColor;
    public PlayerType type;
    public int markers;

    public void AddMarker()
    {
        Debug.Log($"Ding! {playerName} got a marker.");
        markers++;
    }
    public void RemoveMarker() { 
        if (markers > 0) markers--; 
    }

}
public class CoreLoop : MonoBehaviour
{
    public int currentPlayer;
    public int maxPlayers;
    Player[] players = new Player[4];
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = 0;
        maxPlayers = 4;

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
        Debug.Log($"Current player is now: {players[currentPlayer].playerName}\nThey have {players[currentPlayer].markers} markers.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentPlayer >= maxPlayers - 1) { currentPlayer = 0; }
            else { currentPlayer++; }

            Debug.Log($"Current player is now: {players[currentPlayer].playerName}\nThey have {players[currentPlayer].markers} markers.");
        }
    }
}
