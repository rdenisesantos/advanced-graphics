using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ui_total_score;
    public Text ui_last_level;
    public Text ui_total_lines_cleared;
    public Toggle musicToggle;
    public Slider Volume;

    void Update(){
         // Scores after playing
        ui_total_score.text = GameMaster.currentScore.ToString();
        ui_last_level.text = GameMaster.level.ToString();
        ui_total_lines_cleared.text = GameMaster.lines.ToString();
    }

    public void PlayAgain() {
        Time.timeScale = 1;
        GameMaster.isPaused = false;
        SceneManager.LoadScene("Game");
        ResetScores();
    }

    // Title Screen
    public void MainMenu() {
        ResetScores();
        SceneManager.LoadScene("StartUpMenu");
    }

    public void EndCurrentGame() {
        SceneManager.LoadScene("GameOver");
    }

    public void BackToGame() {
        Time.timeScale = 1;
        GameMaster.isPaused = false;
        FindObjectOfType<GameMaster>().gameInfo.enabled = true;
        FindObjectOfType<GameMaster>().mainMenu.enabled = false;
    }

    public void ResetScores() {
        GameMaster.currentScore = 0;
        GameMaster.level = 0;
        GameMaster.lines = 0;
    }

    // Manage volume slider
    public void ManageVolume()
    {
        FindObjectOfType<GameMaster>().audioSource.volume = Volume.value;
        if(Volume.value.Equals(0)) {
            musicToggle.isOn = false;
        }
        else {
            musicToggle.isOn = true;
        }
    }

    // Manage music toggle button
    public void ToggleMusic(){
        if (!musicToggle.isOn){
           Volume.value = 0;
        }
        if (musicToggle.isOn){
           Volume.value = 0.7f;
        }
    }

    public void QuitGame(){
        Application.Quit();
    }
}
