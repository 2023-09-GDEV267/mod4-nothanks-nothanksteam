using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChoice : MonoBehaviour
{
    [Header("Color")]
    public Text playerOneText;
    public Text playerTwoText;
    public Text playerThreeText;
    public Text playerFourText;
    
    void Awake()
    {
        if (playerOneText == null)
        {
            playerOneText = GetComponent<Text>();
            playerOneText.color = Color.red;
            playerTwoText.color = Color.green;
            playerThreeText.color = Color.blue;
            playerFourText.color = Color.yellow;
        }
    }

    // Start is called before the first frame update
    public void Red()
    {
        playerOneText = gameObject.GetComponent<Text>();
        playerOneText.color = Color.red;
        playerTwoText.color = Color.green;
        playerThreeText.color = Color.blue;
        playerFourText.color = Color.yellow;
    }

    public void Green()
    {
        playerOneText = gameObject.GetComponent<Text>();
        playerOneText.color = Color.green;
        playerTwoText.color = Color.red;
        playerThreeText.color = Color.blue;
        playerFourText.color = Color.yellow;
    }

    public void Blue()
    {
        playerOneText = gameObject.GetComponent<Text>();
        playerOneText.color = Color.blue;
        playerTwoText.color = Color.yellow;
        playerThreeText.color = Color.green;
        playerFourText.color = Color.red;
    }

    public void Yellow()
    {
        playerOneText = gameObject.GetComponent<Text>();
        playerOneText.color = Color.yellow;
        playerTwoText.color = Color.blue;
        playerThreeText.color = Color.red;
        playerFourText.color = Color.green;
    }
}
