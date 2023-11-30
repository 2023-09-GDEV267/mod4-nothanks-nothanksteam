using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene("CoreLoop");
    }
    public void PlayerSelect()
    {
        SceneManager.LoadScene("SelectPlayers");
    }
}
