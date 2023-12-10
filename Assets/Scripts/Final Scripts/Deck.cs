using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform deckAnchor;
    public List<GameObject> cardsPrefabs;
    public GameObject cardBackPrefab; // The back of the card, to display as a stand-in for the stacked deck in the center of the game table

    [Header("Set Dynamically")]
    public GameObject cardBack;
    public List<Card> cards;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeCards();
        Shuffle(ref cards);
        BurnCards();
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

    public void BurnCards()
    {
        cards.RemoveRange(0, 9);
    }

    public Card Draw()
    {
        Debug.Log("Drawing");
        if(cards.Count < 1) return null;
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        drawnCard.SetVisible(true);
        if (cards.Count < 1)
        {
           cardBack.SetActive(false);
        }
        return drawnCard;
    }
    
    public void InitializeCards()
    {
        cardBack = Instantiate(cardBackPrefab);
        cardBack.transform.SetParent(deckAnchor);
        cardBack.transform.localPosition = Vector3.zero;
        foreach (GameObject card in cardsPrefabs)
        {
            GameObject cardGameObject = Instantiate(card);
            cardGameObject.transform.parent = deckAnchor;
            cardGameObject.transform.localPosition = Vector3.zero;
            cardGameObject.GetComponent<Card>().SetVisible(false);
            Card newCard = cardGameObject.GetComponent<Card>();
            cards.Add(newCard);
        }
/*        for (int i = 0; i < cards.Count; i++) 
        {
            Card card = cards[i];
            card.transform.localPosition = new Vector3(card.transform.localPosition.x, card.transform.localPosition.y, i);
        }*/
        
    }
}
