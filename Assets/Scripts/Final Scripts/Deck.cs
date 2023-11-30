using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Card card in cards)
        {
            Debug.Log(card);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
