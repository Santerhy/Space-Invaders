using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public ParticleSystem explosion;


    public void Explode(Vector2 location)
    {
        ParticleSystem p = Instantiate(explosion, location, Quaternion.identity);
        p.Play();
    }
}
