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
    [Header("Set In Inspector")]
    public GameObject cardAnchor;
    public GameObject lowMarkers;
    public GameObject midMarkers;
    public GameObject highMarkers;
    public GameObject currentPlayerSpotlight;

    [Header("Set Dynamically")]
    public int playerID;
    public string playerName;
    public Color playerColor;
    public PlayerType type;
    public int markers;
    public List<Card> cards = new List<Card>();
    public List<List<Card>> streaks = new List<List<Card>>();
    public PlayerState state;
    public int score;

    public void UpdateHeldMarkersDisplay()
    {
        /*Debug.Log($"{playerName} has {markers} markers!");*/
        if (markers < 5)
        {
            lowMarkers.SetActive(true);
            midMarkers.SetActive(false);
            highMarkers.SetActive(false);
        } else if (markers < 11) {
            lowMarkers.SetActive(false);
            midMarkers.SetActive(true);
            highMarkers.SetActive(false);
        }
        else
        {
            lowMarkers.SetActive(false);
            midMarkers.SetActive(false);
            highMarkers.SetActive(true);
        }
    }

    public void AddMarkers(int quantity)
    {
        markers += quantity;
/*        Debug.Log($"Ding! {playerName} got {quantity} markers.");*/
        UpdateHeldMarkersDisplay();
    }
    public void RemoveMarker()
    {
        if (markers > 0)
        {
            markers--;
/*            Debug.Log($"{playerName} spent a marker. They have {markers} markers left.");*/
        }
        else
        {
            Debug.Log($"{playerName} is out of markers!");
        }
        UpdateHeldMarkersDisplay();
    }
    public void ReceiveCard(Card card)
    {
        cards.Add(card);
        AddMarkers(card.markers);
        card.markers = 0;
        card.transform.SetParent(cardAnchor.transform);
        card.transform.localPosition = new Vector3(0 + cards.Count - 1, 0, 0);
        card.transform.localScale = new Vector3(.5f, -.5f, 1);
        streaks = CoreLoop.SortStreaks(cards);
    }
    public void DisplayCards()
    {

    }
}
