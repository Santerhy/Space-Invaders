using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text newHighScore;
    public Text scores;
    public Text highscore;
    public Text level;
    public float scoresNum;
    public float highscoreNum;

    // Start is called before the first frame update
    void Start()
    {
        scoresNum = GameManager.Instance.GetCurrentScores(); //Get current scores and current highscore
        highscoreNum = GameManager.Instance.GetHighscore();
        if (scoresNum > highscoreNum)
        {                                                    //If scores are higher than highscore, set "New highscore"-text to visible
            GameManager.Instance.SetHighscore(scoresNum);
            newHighScore.gameObject.SetActive(true);
        }
        scores.text = "Scores: " + scoresNum.ToString();
        highscore.text = "Highscore: " + highscoreNum.ToString();
        level.text = "Level: " + GameManager.Instance.GetCurrentLevel().ToString();
        GameManager.Instance.Save();
        Debug.Log("Highscores: " + highscoreNum);
    }

     public void BackToMenu()
    {
        GameManager.Instance.ResetStats();
        SceneManager.LoadScene("MainMenu");
    }
}
