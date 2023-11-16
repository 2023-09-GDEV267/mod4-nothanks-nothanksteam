using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    decision,
    acting
}

[System.Serializable]
public class Player
{
    [Header("Set in inspector")]

    [Header("Set dynamically")]
    public Color playerColor;
    public int playerID;
    public string playerName;
    public PlayerState state;
    public int counters;
}
