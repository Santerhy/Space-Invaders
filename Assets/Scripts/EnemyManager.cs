using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float moveDistance;
    public float teleportTimer;
    public float teleportTimerMax;
    public float moveDirection = -1;
    public float moveSpeed;
    public float descendSpeed;
    public int lastEnemyCount;
    public List<GameObject> allEnemies;
    public GameManager gameManager;
    public Rigidbody2D rb;
    public bool directionChanged;
    public int enemyCount;
    public float speedMultiplier;
    public float score;
    public Text scoreField;
    public ParticleSystem explosionEffect;
    public EnemySoundeffectManager enemySoundeffect;
    public GameObject bonusEnemy;
    public ParticleSystem bonusEnemyEffect;

    public int rows;
    public int columns;
    public List<GameObject> prefabs;
    private GameObject enemy;

    private float checkCounter;
    private float checkCounterMax = 0.3f;

    private Vector2 velocity;

    private void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector3 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, 4 + row * 1.2f, 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                enemy = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * 1.2f;
                enemy.transform.position = position;
                allEnemies.Add(enemy);
            }
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        lastEnemyCount = allEnemies.Count;
        SetScores();
        moveSpeed = GameManager.Instance.GetMoveSpeed();
        CountEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.MovePosition(new Vector2(moveDirection * moveSpeed * Time.deltaTime, 0));
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        CheckForWall();
        CheckEnd();
    }

    private void FixedUpdate()
    {
        if (checkCounter > checkCounterMax)
        {
            checkCounter = 0;
            RemoveNulls();
            CountEnemies();
        }
        else
            checkCounter += Time.deltaTime;
    }

    public void CheckForWall()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
                continue;
            
            if (moveDirection == 1 && invader.position.x > (rightEdge.x - 1.0f))
            {
                moveDirection = moveDirection * -1;
                MoveDown();
            } else if (moveDirection == -1 && invader.position.x < (leftEdge.x + 1.0f))
            {
                moveDirection = moveDirection * -1;
                MoveDown();
            }

        }
    }

    public void EnemyKilled()
    {
        moveSpeed = moveSpeed * speedMultiplier;
        GameManager.Instance.scores += 100;
        score += 100;
        scoreField.text = score.ToString();
        if (enemyCount % 16 == 0)
            CreateBonusEnemy();
    }
    public void RemoveNulls()
    {
        allEnemies.RemoveAll(GameObject => GameObject == null);
        Debug.Log("Nulls removed");
    }

    public void MoveDown()
    {
        Vector3 hight = this.transform.position;
        hight.y -= 0.3f;
        this.transform.position = hight;

        //transform.Translate(Vector2.down * descendSpeed * Time.deltaTime);
    }

    public void CountEnemies()
    {
        enemyCount = allEnemies.Count;

        lastEnemyCount = enemyCount;
        scoreField.text = score.ToString();

        if (enemyCount <= 0) {
            Debug.Log("Voitto!");
            gameManager.GetComponent<GameManager>().LevelCleared(score);
        }
    }

    public void CheckEnd()
    {
        Vector3 bottom = Camera.main.ViewportToWorldPoint(Vector3.down);
        foreach (Transform invader in this.transform)
        {
            if (invader.position.y < bottom.y + 22.5f)
            {
                Debug.Log("GAME OVER!!!");
                GameManager.Instance.GameOver();
            }
        }
    }

    public float GetScores()
    {
        return this.score;
    }

    public void SetScores()
    {
        score = gameManager.GetCurrentScores();
    }

    public void CreateExplosion(Vector2 location)
    {
        FindObjectOfType<AudioManager>().Play("EnemyExplosion");
        Quaternion rotation = Quaternion.identity;
        rotation.x = explosionEffect.transform.localRotation.eulerAngles.x + 45;
        ParticleSystem p = Instantiate(explosionEffect, location, rotation);
        p.Play();
    }

    public void CreateBonusEnemy()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        pos.y -= 2.0f;
        pos.z = 0;
        GameObject g = Instantiate(bonusEnemy, pos, Quaternion.identity);
        Destroy(g, 20.0f);
    }

    public void BonusDestroyed()
    {
        GameManager.Instance.scores += 500;
        score += 500;
        scoreField.text = score.ToString();
    }

    public void BonusEnemyEffect(Vector2 loc)
    {
        ParticleSystem p = Instantiate(bonusEnemyEffect, loc, Quaternion.identity);
        p.Play();
        FindObjectOfType<AudioManager>().Play("BonusExplosion");
    }

}
