using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public string lastScene; 
    public void LoadSinglePlayerGame()
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameLoop");
    }

    public void LoadMultiplayerGame()
    {
        lastScene = SceneManager.GetActiveScene().name;
        Debug.Log("Multiplayer Game not implemented.");
    }
    public void LoadOptionsScreen()
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Options");
    }

    public void Back()
    {
        SceneManager.LoadScene(lastScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
