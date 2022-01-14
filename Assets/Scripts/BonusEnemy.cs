using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemy : MonoBehaviour
{
    public float moveSpeed;
    public ParticleSystem p;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Vector2 loc = transform.position;
            FindObjectOfType<EnemyManager>().BonusEnemyEffect(loc);
            FindObjectOfType<EnemyManager>().BonusDestroyed();
            Destroy(collision.gameObject);
            Destroy(transform.gameObject);
        }
    }
}
