using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardState
{
    deck,
    table,
    sequence
}

public class Card : MonoBehaviour
{
    public CardState state;
    public int value;
    public int markers;
    public bool isFaceUp = false;
}
