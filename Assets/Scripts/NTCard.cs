using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardState
{
    deck,
    table,
    sequence
}

public class NTCard : MonoBehaviour
{
    public CardState state;
    public int num;
    public Color color;


}
