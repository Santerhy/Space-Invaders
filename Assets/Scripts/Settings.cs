using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Slider musicVolume;
    public Slider sfxVolume;
    public AudioManager audioManager;
    public AudioMixer audioMixer;
    private float currentMusicVol;
    private float currentSfxVol;
    private GameManager gameManager;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = FindObjectOfType<AudioManager>();

        SetMusicVolume(gameManager.musicVolume);
        SetSfxVolume(gameManager.sfxVolume);
        audioMixer.GetFloat("MusicVolume", out currentMusicVol);
        audioMixer.GetFloat("SfxVolume", out currentSfxVol);
        musicVolume.value = currentMusicVol;
        sfxVolume.value = currentSfxVol;
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<AudioManager>().SetVolume("MenuTheme", musicVolume.value);
        FindObjectOfType<AudioManager>().SetVolume("Theme", musicVolume.value);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", volume);
    }
}
