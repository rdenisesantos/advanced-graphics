using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text ui_total_score;
    public Text ui_last_level;
    public Text ui_total_lines_cleared;

    void Update(){
            ui_total_score.text = GameMaster.currentScore.ToString();
            ui_last_level.text = GameMaster.level.ToString();
            ui_total_lines_cleared.text = GameMaster.lines.ToString();
    }
    public void PlayAgain() {
        SceneManager.LoadScene("Game");
        ResetScores();
    }
    public void MainMenu() {
        SceneManager.LoadScene("StartUpMenu");
        ResetScores();
    }
    public void ResetScores() {
        GameMaster.currentScore = 0;
        GameMaster.level = 0;
        GameMaster.lines = 0;
    }
}
