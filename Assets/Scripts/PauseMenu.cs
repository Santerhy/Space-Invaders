using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public bool gamePaused = true;
    public GameManager gameManager;
    public AudioMixer mixer;
    public Slider musicVolume;
    public Slider sfxVolume;
    private float currentMusicVol;
    private float currentSfxVol;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        mixer.GetFloat("MusicVolume", out currentMusicVol);
        mixer.GetFloat("SfxVolume", out currentSfxVol);
        musicVolume.value = currentMusicVol;
        sfxVolume.value = currentSfxVol;
        gamePaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Play("MenuClick");
            PauseMenuPopUp();

        }
    }

    public void PauseMenuPopUp()
    {
        if (gamePaused)
        {
            gamePaused = !gamePaused;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            gamePaused = !gamePaused;
            Time.timeScale = 1;
            FindObjectOfType<AudioManager>().Play("MenuClick");
            pausePanel.SetActive(false);
        }
        
    }

    public void PauseMenuButton()
    {
        gameManager.ResetStats();
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().Play("MenuClick");
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        gameManager.musicVolume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        mixer.SetFloat("SfxVolume", volume);
        gameManager.sfxVolume = volume;
    }
}
