using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public AudioSource audioSource;
    public Canvas gameInfo;
    public Canvas pauseScreen;
    public Canvas mainMenu; 
    public GameObject[] TetriminoPrefabs;
    public GameObject CurrentTetrominoFalling = null;
    public GameObject NextTetromino = null;

    public float speed = 1.0f;
    public static bool isPaused = false;

    // for simplicity, scores will be multiplied by
    // the following multipliers to the current level
    // eg. for level two, oneLine will score 80,
    // twoLines will score 200, threeLines = 600,
    // fourLines = 800... and so forth

    public Text ui_score;
    public static int currentScore ;

    // score multipliers
    public int oneLine = 20;
    public int twoLines = 40;
    public int threeLines = 60;
    public int fourLines = 80;

    // number of full rows in one turn
    public static int numOfRows = 0;

    // current level
    public Text ui_current_level;
    public static int level;

    // lines to be cleared to get to the next level
    // for simplicity, it will be twice as much each level

    // lines cleared since the game started
    public Text ui_lines_cleared;
    public static int lines = 0;

    void Start() {
        SpawnNewTetromino();
    }

    void Update(){
        UpdateScore();
        UpdateUI();
        UpdateLevel();
        UpdateSpeed();
        CheckIfPauseIsRequested();
        CheckIfMenuIsRequested();
    }

    public void UpdateUI() {
        ui_score.text = currentScore.ToString();
        ui_current_level.text = level.ToString();
        ui_lines_cleared.text = lines.ToString();
    }

    //----------------------HELPER METHODS -------------------------------//
    void CheckIfPauseIsRequested()
    {
        if (Input.GetKeyUp(KeyCode.P)) { 
            if(Time.timeScale == 1) {
                Time.timeScale = 0;
                isPaused = true;
                audioSource.Pause();
                gameInfo.enabled = false;
                pauseScreen.enabled = true;
            }
            else {
                Time.timeScale = 1;
                isPaused = false;
                audioSource.Play();
                gameInfo.enabled = true;
                pauseScreen.enabled = false;
            }
        }
    }

    void CheckIfMenuIsRequested()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                isPaused = true;
                gameInfo.enabled = false;
                mainMenu.enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                gameInfo.enabled = true;
                mainMenu.enabled = false;
            }
        }
    }

    // spawns new tetrominos on top of the board and previews the upcoming tetromino
    public void SpawnNewTetromino() {
        if (CurrentTetrominoFalling == null && NextTetromino == null)
            { // new game
                CurrentTetrominoFalling = GameObject.Instantiate
                                          (TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                                          new Vector3(4, 19, 0),
                                          Quaternion.identity) as GameObject;

                NextTetromino = GameObject.Instantiate
                                (TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                                new Vector3(17, 7, 0),
                                Quaternion.identity) as GameObject;

                // prevents the next tetromino from moving/rotating while the
                // current tetromino is falling
                NextTetromino.GetComponent<Tetromino>().enabled = false;
            }
            else
            { // game in progress

                // previewed tetromino becomes the current tetromino and spawns on
                // top of the board/grid
                NextTetromino.transform.position = new Vector3(4, 19, 0);
                CurrentTetrominoFalling = NextTetromino;
                CurrentTetrominoFalling.GetComponent<Tetromino>().enabled = true;

                // next tetromino being previewed
                NextTetromino = GameObject.Instantiate
                                (TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                                new Vector3(17, 7, 0),
                                Quaternion.identity) as GameObject;

                NextTetromino.GetComponent<Tetromino>().enabled = false;
            }
            Debug.Log(CurrentTetrominoFalling.GetComponent<ITetromino>().GetType());
    }

    // ---------------------- SCORING METHODS -----------------------//
    public void UpdateScore(){
        if(numOfRows > 0){
            switch(numOfRows){
                case 1: clearedOne();
                        break;
                case 2: clearedTwo();
                        break;
                case 3: clearedThree();
                        break;
                case 4: clearedFour();
                        break;
            }
            // reset
            numOfRows = 0;
        }
    }

    public void clearedOne(){
        if (level == 0){
            currentScore += 20;
        }
        else {
            currentScore += oneLine * level;
        }
    }

    public void clearedTwo(){
        if (level == 0) {
            currentScore += 40;
        }
        else {
            currentScore += twoLines * level;
        }
    }

    public void clearedThree(){
        if (level == 0) {
            currentScore += 60;
        }
        else {
            currentScore += threeLines * level;
        }
    }

    public void clearedFour(){
        if (level == 0) {
            currentScore += 80;
        }
        else {
            currentScore += fourLines * level;
        }
    }

    // for simplicity update level every 10 lines cleared
    void UpdateLevel() {
        level = lines / 10;
    }

    // update speed after every level for increased difficulty
    void UpdateSpeed() {
        speed = 1.0f - ((float)level * 0.15f);
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
}
