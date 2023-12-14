using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform deckAnchor;
    public List<GameObject> cardsPrefabs;
    public GameObject cardBackPrefab; // The back of the card, to display as a stand-in for the stacked deck in the center of the game table
    public float cardMovementSpeed = .3f;

    [Header("Set Dynamically")]
    public GameObject cardBack;
    public List<Card> cards;
    
/*    void Awake()
    {
        InitializeCards();
        Shuffle(ref cards);
        BurnCards();
    }*/



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
        for (int i = 0; i < 9; i++) {
            Destroy(cards[i].gameObject);
            cards.Remove(cards[i]);
        }
    }

    public Card Draw()
    {
        if(cards.Count < 1) return null;
        Card drawnCard = cards[0];
        drawnCard.targetPos = Vector3.zero;
        cards.RemoveAt(0);
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
            cardGameObject.GetComponent<Card>().speed = cardMovementSpeed; // Setting this here to avoid editing each prefab individually
/*            cardGameObject.transform.SetParent(deckAnchor);*/
            cardGameObject.transform.position = deckAnchor.transform.position;
            Card newCard = cardGameObject.GetComponent<Card>();
            cards.Add(newCard);
        }
        
    }

}
