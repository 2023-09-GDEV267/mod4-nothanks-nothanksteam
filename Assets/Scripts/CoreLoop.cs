using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Human,
    Bot
}


public class Player
{
    public int playerId;
    //public Color playerColor;
    //public bool isHuman;
}


public class CoreLoop : MonoBehaviour
{
    int currentPlayer;
    int maxPlayers;
    Player[] players = new Player[4];

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = 0;
        maxPlayers = 4;
        players[0] = new Player();
        players[0].playerId = 1;
        //players[0].playerColor = Color.blue;
        players[1] = new Player();
        players[0].playerId = 2;
        //players[0].playerColor = Color.red;
        players[2] = new Player();
        players[0].playerId = 3;
        //players[0].playerColor = Color.green;
        players[3] = new Player();
        players[0].playerId = 4;
        //players[0].playerColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer++;
        
        if(currentPlayer >= maxPlayers) currentPlayer = 0;

        Debug.Log("Current player is now: " + currentPlayer);
    }
}