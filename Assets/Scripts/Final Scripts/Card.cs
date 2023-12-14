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
    [Header("Set Dynamically")]
    public CardState state;
    public int value;
    public int markers =0;
    public bool isFaceUp = false;
    public float speed = .5f;
    public Vector3 targetPos;
    
    public void Start()
    {
        speed = .5f;
        targetPos = transform.position;
    }
    public void Update()
    {
        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }

    }

/*    public void SetVisible(bool isVisible)
    {
        gameObject.GetComponent<Renderer>().enabled = isVisible;
        foreach (Transform spriteTransform in transform)
        {
            spriteTransform.gameObject.GetComponent<Renderer>().enabled = isVisible;
        }
    }*/
/*    public void FixedUpdate()
    {
    
        if (transform.localPosition != targetPos)
        {
            float step = speed*Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }

    }*/
}
