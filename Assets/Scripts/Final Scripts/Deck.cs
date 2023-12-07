using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Transform deckAnchor;
    public List<GameObject> cardsPrefabs;
    public List<Card> cards;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeCards();
        // Assigning values to each card in the deck, Assuming cards in list are in correct order. 

        /*        foreach (Card card in cards)
                {
                    Debug.Log(card);
                }        */
    }



    public void Shuffle(ref List<Card> oCards)
    {
        List<Card> tCards = new List<Card>();

        int index;   // which card to move

        while (oCards.Count > 0)
        {
            // find a random card, add it to shuffled list and remove from original deck
            index = Random.Range(0, oCards.Count);
            tCards.Add(oCards[index]);
            oCards.RemoveAt(index);
        }

        oCards = tCards;

        //because oCards is a ref parameter, the changes made are propogated back
        //for ref paramters changes made in the function persist.


    }

    public void Draw()
    {
        
    }
    
    public void InitializeCards()
    {
        
        foreach (GameObject card in cardsPrefabs)
        {
            GameObject cardGameObject = Instantiate(card);
            cardGameObject.transform.parent = deckAnchor;
            cardGameObject.transform.localPosition = Vector3.zero;
            Card newCard = cardGameObject.GetComponent<Card>();
            cards.Add(newCard);
        }

        for (int i = 0, j = 3; i < cards.Count; i++, j++)
        {
            cards[i].value = j;
        }
    }
}
