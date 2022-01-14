using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetector : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyController parentScript;

    private void Start()
    {
        enemyManager = gameObject.GetComponentInParent<EnemyManager>();
        parentScript = this.transform.parent.GetComponent<EnemyController>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            parentScript.TryToPowerup();
            enemyManager.EnemyKilled();
            parentScript.Explode();
            Destroy(collision.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }

}
