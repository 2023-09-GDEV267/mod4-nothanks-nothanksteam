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
    [Header("Set in Inspector")]
    public float speed = 2;

    [Header("Set Dynamically")]
    public CardState state;
    public int value;
    public int markers;
    public bool isFaceUp = false;

    public Vector3 targetPos;
    
    public void Start()
    {
        targetPos = transform.position;
    }
/*    public void FixedUpdate()
    {
    
        if (transform.localPosition != targetPos)
        {
            float step = speed*Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }

    }*/
}
