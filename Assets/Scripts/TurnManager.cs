using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("Set in inspector")]
    public int numOfPlayers;

    [Header("Set dynamically")]
    public int currentPlayer;

    [SerializeField]
    public Player[] players;

    private void Awake()
    {
        currentPlayer = 0;
        players = new Player[numOfPlayers];
        
        for (int i = 0; i < numOfPlayers; i++)
        {
            players[i] = new Player();
            players[i].playerID = i;
            players[i].state = PlayerState.idle;
        }

        players[0].playerColor = Color.white;
        players[0].playerName = "Lily";
        players[1].playerColor = Color.red;
        players[1].playerName = "Rose";
        players[2].playerColor = Color.gray;
        players[2].playerName = "Amanita";
        players[3].playerColor = Color.blue;
        players[3].playerName = "Hyacinth";

        Debug.Log("It is now " + players[currentPlayer].playerName + "'s turn.");
    }

    private void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            currentPlayer++;

            if (currentPlayer == players.Length)
            {
                currentPlayer = 0;
            }

            Debug.Log("It is now " + players[currentPlayer].playerName + "'s turn.");
        }
    }
}
