using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        Time.timeScale = 1;
        GameMaster.isPaused = false;
        GameMaster.currentScore = 0;
        GameMaster.level = 0;
        GameMaster.lines = 0;
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
