using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void OptionsButton()
    {
        SceneManager.LoadScene("Options");
    }
    public void PlayerSelectButton()
    {
        SceneManager.LoadScene("PlayerSelect");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void FourPButton()
    {
        SceneManager.LoadScene("CoreLoop");
    }
}
