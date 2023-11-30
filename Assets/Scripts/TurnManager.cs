using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("Set in inspector")]
    public int numOfPlayers;

    [Header("Set dynamically")]
    public int currentTurnPlayer;
    public int currentRoundPlayer;

    [SerializeField]
    public Player[] players;

    private void Awake()
    {
        currentRoundPlayer = 0;
        currentTurnPlayer = 0;
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

        Debug.Log(players[currentRoundPlayer].playerName + " is starting the first round.");
        Debug.Log("It is now " + players[currentTurnPlayer].playerName + "'s turn.");
    }

    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            AdvanceTurn();
        }

        if (Input.GetKeyDown("x"))
        {
            AdvanceRound();
        }
    }

    public void AdvanceTurn()
    {
        players[currentTurnPlayer].state = PlayerState.idle;
        currentTurnPlayer++;

        if (currentTurnPlayer == players.Length)
        {
            currentTurnPlayer = 0;
        }

        players[currentTurnPlayer].state = PlayerState.decision;

        Debug.Log("It is now " + players[currentTurnPlayer].playerName + "'s turn.");
    }

    public void AdvanceRound()
    {
        players[currentTurnPlayer].state = PlayerState.idle;
        Debug.Log(players[currentTurnPlayer].playerName + " takes the card.");
        currentRoundPlayer++;

        if (currentRoundPlayer == players.Length)
        {
            currentRoundPlayer = 0;
        }

        currentTurnPlayer = currentRoundPlayer;
        players[currentTurnPlayer].state = PlayerState.decision;

        Debug.Log(players[currentRoundPlayer].playerName + " is starting the next round.");
    }
}
