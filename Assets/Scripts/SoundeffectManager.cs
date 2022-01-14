using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundeffectManager : MonoBehaviour
{
    public AudioSource explosion;
    public AudioSource shooting;
    public AudioSource powerup;

    public void PlayExplosion()
    {
        explosion.Play();
    }

    public void PlayShooting()
    {
        shooting.Play();
    }

    public void PlayPowerup()
    {
        powerup.Play();
    }
}
