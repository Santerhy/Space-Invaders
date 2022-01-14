using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject instructionsPanel;
    public GameObject settingsPanel;
    public Slider musicVolume;
    public Slider sfxVolume;
    public AudioManager audioManager;
    public AudioMixer audioMixer;
    private float currentMusicVol;
    private float currentSfxVol;
    private GameManager gameManager;
    public Text highscoreText;

    public void Start()
    {
        gameManager = GameManager.Instance;
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("MenuTheme");
        SetMusicVolume(gameManager.musicVolume);
        SetSfxVolume(gameManager.sfxVolume);
        audioMixer.GetFloat("MusicVolume", out currentMusicVol);
        audioMixer.GetFloat("SfxVolume", out currentSfxVol);
        musicVolume.value = currentMusicVol;
        sfxVolume.value = currentSfxVol;
        highscoreText.text = "Highscore: " + gameManager.GetHighscore();
    }

    public void Play()
    {
        SceneManager.LoadScene("Play");
    }

    public void Instructions()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        instructionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void Settings()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void  CloseSettings()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        gameManager.musicVolume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", volume);
        gameManager.sfxVolume = volume;
    }

    public void Quit()
    {
        gameManager.Save();
        Application.Quit();
    }
}
