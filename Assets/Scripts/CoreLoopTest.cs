using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerType
{
    Human,
    Bot
}
public enum BotPlayLevel
{
    low,medium,hard
}
public class Player
{
    public int playerID;
    public string playerName;
    public Color playerColor;
    //public bool isHuman;
}
public class CoreLoopTest : MonoBehaviour
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
        players[0].playerName = "Steve";
        players[0].playerID = 1;
        players[0].playerColor = Color.blue;
        players[1] = new Player();
        players[1].playerName = "Amy";
        players[1].playerID = 2;
        players[1].playerColor = Color.yellow;
        players[2] = new Player();
        players[2].playerName = "Bob";
        players[2].playerID = 3;
        players[2].playerColor = Color.green;
        players[3] = new Player();
        players[3].playerName = "Shannon";
        players[3].playerID = 4;
        players[3].playerColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentPlayer++;
            if (currentPlayer >= maxPlayers) currentPlayer = 0;
            Debug.Log("Current player is now " + players[currentPlayer].playerName);
        }
    }
}
