using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Human,
    Bot
}

public enum PlayerState
{
    idle,
    decision,
    acting
}

public enum BotPlayLevel
{
    easy, medium, hard
}
public class Player : MonoBehaviour
{
    public int playerID;
    public string playerName;
    public Color playerColor;
    public PlayerType type;
    public int markers;
    public List<Card> cards = new List<Card>();
    public List<List<Card>> streaks = new List<List<Card>>();
    public PlayerState state;
    public GameObject cardAnchor;

    public void AddMarkers(int quantity)
    {
        markers += quantity;
        Debug.Log($"Ding! {playerName} got {quantity} markers.");
    }
    public void RemoveMarker()
    {
        if (markers > 0) markers--;
        Debug.Log($"{playerName} spent a marker. They have {markers} markers left.");
    }
    public void ReceiveCard(Card card)
    {
        cards.Add(card);
        streaks = CoreLoop.SortStreaks(cards);
    }
    public void DisplayCards()
    {

    }
}
