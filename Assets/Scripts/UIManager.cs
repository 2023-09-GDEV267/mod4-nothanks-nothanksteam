using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Animator optionsMenu;
    public Animator gamePanel;
    public Animator playObjects;
    public Animator startMenu;

    public static UIManager S;

    public void Awake()
    {
        if (S == null) { S = this; }
        optionsMenu.SetBool("optionsActive", false);
        gamePanel.SetBool("gameIn", false);
        playObjects.SetBool("playIn", false);
        startMenu.SetBool("startActive", true);
    }

    public void StartSingleButton()
    {
        gamePanel.SetBool("gameIn", true);
        playObjects.SetBool("playIn", true);
        startMenu.SetBool("startActive", false);
        AudioManager.S.GameMusic();
        Invoke("CallToStart", 2);
    }

    private void CallToStart()
    {
        CoreLoop.S.StartGame();
    }

    public void OptionsButton()
    {
        optionsMenu.SetBool("optionsActive", true);
    }

    public void BackButton()
    {
        optionsMenu.SetBool("optionsActive", false);
    }

    public static void GoToTitle()
    {
        SceneManager.LoadScene("ScreenFlowIntegration");
    }

    //public static void LoadSinglePlayerGame()
    //{
    //    PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
    //    SceneManager.LoadScene("GameLoop");
    //}

    //public static void LoadMultiplayerGame()
    //{
    //    PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
    //    Debug.Log("Multiplayer Game not implemented.");
    //}
    //public static void LoadOptionsScreen()
    //{
    //    PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
    //    SceneManager.LoadScene("Options");
    //}

    //public static void Back()
    //{ 
    //    if (PlayerPrefs.HasKey("previousScene") && PlayerPrefs.GetString("previousScene")!= null)
    //    {
    //        SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
    //    }
    //    else
    //    {
    //        Debug.Log("Error: No previous scene set");
    //    }
    //}

    public static void ExitGame()
    {
        Application.Quit();
    }

}
