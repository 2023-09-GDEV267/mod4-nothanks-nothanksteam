using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager S;
    public void Awake()
    {
        if (S == null) { S = this; }
    }


    public static void LoadSinglePlayerGame()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameLoop");
    }

    public static void LoadMultiplayerGame()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        Debug.Log("Multiplayer Game not implemented.");
    }
    public static void LoadOptionsScreen()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }

    public static void Back()
    { 
        if (PlayerPrefs.HasKey("previousScene") && PlayerPrefs.GetString("previousScene")!= null)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
        }
        else
        {
            Debug.Log("Error: No previous scene set");
        }

    }

    public static void ExitGame()
    {
        Application.Quit();
    }

}
