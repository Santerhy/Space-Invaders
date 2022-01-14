using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject friendlyDetector;
    public GameManager gameManager;
    public GameObject[] powerUps;
    public EnemyManager enemyManager;
    public EnemySoundeffectManager soundeffectManager;
    public float shootingTimer;
    public float shootingTimerMax = 1.0f;
    public float shootingForce;
    public float bulletLifetime;
    public bool shootingCheck;
    public float shootMaxTime;
    public float shootMinTime;
    public float shootingChance;
    public int powerupIndex = 0;
    public float powerupChance = 10.0f;
    public float powerupSpeed;
    public float powerupLifetime = 10.0f;

    private void Awake()
    {

    }
    void Start()
    {
        gameManager = GameManager.Instance;
        shootingChance = gameManager.GetComponent<GameManager>().shootChance;
        enemyManager = GetComponent<EnemyManager>();
        soundeffectManager = transform.parent.GetComponent<EnemySoundeffectManager>();

        //shootMinTime = gameManager.GetComponent<GameManager>().GetShootMinTime();
        //shootMaxTime = gameManager.GetComponent<GameManager>().GetShootMaxTime();
        //RandTime();
        shootingTimerMax = 1.0f;
        getPowerupIndex();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingTimer > shootingTimerMax)
        {
            shootingTimer = 0;
            tryToShoot();
        }
        else
            shootingTimer += Time.deltaTime;

    }

    public void Shooting()
    {
        FindObjectOfType<AudioManager>().Play("EnemyShoot");
        GameObject projectile = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, shootingForce), ForceMode2D.Impulse);
        Destroy(projectile, bulletLifetime);
        //RandTime();
    }

    //Generate new time intervall for shooting
    public void RandTime()
    {
        //TODO: Change shooting mechanism from timed event to chance event
        float x = Random.Range(shootMinTime, shootMaxTime);
        //shootingTimerMax = x;
    }

    public void tryToShoot()
    {
        float x = Random.Range(1.0f, 100.0f);
        if (shootingChance > x)
            Shooting();
    }

    public void getPowerupIndex()
    {
        powerupIndex = Random.Range(0, 3);
    }

    public void TryToPowerup()
    {
        float x = Random.Range(0.0f, 100.0f);
        if (powerupChance > x)
            CreatePowerup();
    }

    public void CreatePowerup()
    {
        GameObject powerUp = Instantiate(powerUps[powerupIndex], transform.position, Quaternion.identity);
        powerUp.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, powerupSpeed), ForceMode2D.Impulse);
        Destroy(powerUp, powerupLifetime);
    }

    public void Explode()
    {
        FindObjectOfType<AudioManager>().Play("EnemyExplosion");
        Vector3 location = new Vector3(transform.position.x, transform.position.y, 0);
        transform.parent.GetComponent<EnemyManager>().CreateExplosion(location);
    }

}
