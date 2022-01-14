using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public int health = 1;
    public ShieldManager shieldManager;

    void Start()
    {
        shieldManager = this.transform.parent.GetComponent<ShieldManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
            Vector2 loc = transform.position;
            shieldManager.Explode(loc);
        }
    }
}
