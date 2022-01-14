using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentLevel = 1;
    private bool levelCleared;

    public int lives = 3;
    public float scores;
    public float highscore;

    public float moveSpeed;
    public float shootChance = 5.0f;
    public AudioMixer audioMixer;
    public float musicVolume;
    public float sfxVolume;

    public Scene scene;


    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
        levelCleared = true;
    }

    public void LevelCleared(float s)
    {
        if (levelCleared)
        {
            scores = s;
            currentLevel += 1;
            moveSpeed += 1.0f;
            shootChance += 2.5f;
            StartCoroutine(WaitForSceneLoad());
            levelCleared = false;
        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        FindObjectOfType<NextLevelManager>().LevelCleared(currentLevel - 1);
        yield return new WaitForSeconds(5);
        levelCleared = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    { 
        SceneManager.LoadScene("GameOver");
    }

    public void ResetStats()
    {
        this.scores = 0f;
        this.moveSpeed = 2f;
        this.shootChance = 5.0f;
        //this.shootMinTime = 5;
        //this.shootMaxTime = 15;
        this.currentLevel = 1;
        this.lives = 3;

    }

    public void TakeHit()
    {
        lives--;
        if (lives <= 0)
            GameOver();
    }

    /*
    public float GetShootMaxTime() 
    {
        return shootMaxTime;
    }

    public float GetShootMinTime()
    {
        return shootMinTime;
    }
    */

    public float GetShootingChance()
    {
        return shootChance;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetCurrentScores()
    {
        return this.scores;
    }

    public float GetHighscore()
    {
        return this.highscore;
    }

    public void SetHighscore(float score)
    {
        this.highscore = score;
    }

    public int GetCurrentLevel()
    {
        return this.currentLevel;
    }

    public int GetLives()
    {
        return this.lives;
    }

    public void SetLives(int i)
    {
        this.lives = i;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerdata.dat");
        PlayerData data = new PlayerData();
        data.highscores = highscore;
        audioMixer.GetFloat("MusicVolume", out data.musicVolume);
        audioMixer.GetFloat("SfxVolume", out data.sfxVolume);
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerdata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerdata.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            highscore = data.highscores;
            musicVolume = data.musicVolume;
            sfxVolume = data.sfxVolume;
        }
    }






}

[Serializable]
class PlayerData
{
    public float highscores;
    public float musicVolume;
    public float sfxVolume;
}
