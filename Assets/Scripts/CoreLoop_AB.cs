using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a simple test file, in order to get the player number and ID to be
//presented in the console

public enum PlayerType_AB
{
    Human,
    Bot
}

public class Player_AB
{
    public int playerId;

    public static implicit operator Player_AB(Player v)
    {
        throw new NotImplementedException();
    }
    //public Color playerColor;
    //public bool isHuman;
}


public class CoreLoop_AB : MonoBehaviour
{
    int currentPlayer;
    int maxPlayers;
    Player_AB[] players = new Player_AB[4];

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = 0;
        maxPlayers = 4;
        players[0] = new Player_AB();
        players[0].playerId = 4;
        //players[0].playerColor = Color.blue;
        players[1] = new Player_AB();
        players[1].playerId = 1;
        //players[0].playerColor = Color.red;
        players[2] = new Player_AB();
        players[2].playerId = 2;
        //players[0].playerColor = Color.green;
        players[3] = new Player_AB();
        players[3].playerId = 3;
        //players[0].playerColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentPlayer++;
            
            if (currentPlayer >= maxPlayers) currentPlayer = 0;

            Debug.Log("Current player is now: " + players[currentPlayer].playerId);
        }
    }
}