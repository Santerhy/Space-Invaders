using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundeffectManager : MonoBehaviour
{
    public AudioSource explosion;
    public AudioSource shooting;

    
    public void PlayExplosion()
    {
        explosion.Play();
        //explosion.PlayOneShot(explosion.GetComponent<AudioClip>(), 1.0f);
    }

    public void PlayShooting()
    {
        //shooting.Play();
        shooting.PlayOneShot(shooting.GetComponent<AudioClip>(), 1.0f);
    }
}
