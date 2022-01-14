using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float shootingTimer;
    public float shootingTimerCounter;
    public bool shootingBool;
    private bool flashing = false;
    public float shootingForce;
    public float bulletLifetime;
    private float z;
    public int lives;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public GameObject playerSkin;
    public GameObject lifeManager;
    public Animator animator;
    public GameManager gameManager;
    public GameObject soundeffectManager;
    public ParticleSystem explodeEffect;
    public ParticleSystem pickupEffect;

    Vector3 spawn;


    // Start is called before the first frame update
    void Start()
    {
        shootingBool = true;
        spawn = Camera.main.ViewportToWorldPoint(Vector3.right / 2);
        shootingTimerCounter = shootingTimer;
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
        lives = GameManager.Instance.GetLives();
        if (gameManager.currentLevel < 2)
        {
            FindObjectOfType<AudioManager>().Stop("MenuTheme");
            FindObjectOfType<AudioManager>().Play("Theme");
        }
        ManageLives(0);
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        if (moveDirection > 0) 
        {
            animator.SetBool("TurnLeft", false);
            animator.SetBool("TurnRight", true);
        } else if (moveDirection < 0)
        {
            animator.SetBool("TurnRight", false);
            animator.SetBool("TurnLeft", true);
        } else
        {
            animator.SetBool("TurnRight", false);
            animator.SetBool("TurnLeft", false);
        }

        if (Input.GetButton("Jump") && shootingBool)
            Shooting();

        if (shootingTimer > shootingTimerCounter)
        {
            shootingBool = true;
        }
        else
            shootingTimer += Time.deltaTime;
    }

    public void Shooting()
    {
        shootingBool = false;
        shootingTimer = 0;
        Debug.Log("Ammutaan");
        GameObject projectile = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, shootingForce), ForceMode2D.Impulse);
        Destroy(projectile, bulletLifetime);
        FindObjectOfType<AudioManager>().Play("PlayerShoot");
        //soundeffectManager.GetComponent<SoundeffectManager>().PlayShooting();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 newPos = transform.position;
        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            TakeHit();
        } else if (collision.CompareTag("Powerup_health"))
        {
            FindObjectOfType<AudioManager>().Play("PowerupPickup");
            ManageLives(1);
            Destroy(collision.gameObject);
            PlayEffect();
        }
        else if (collision.CompareTag("Powerup_A-speed"))
        {
            FindObjectOfType<AudioManager>().Play("PowerupPickup");
            GetComponent<PowerupManager>().SetAspeed();
            Destroy(collision.gameObject);
            PlayEffect();
        }
        else if (collision.CompareTag("Powerup_M-speed"))
        {
            FindObjectOfType<AudioManager>().Play("PowerupPickup");
            GetComponent<PowerupManager>().SetMspeed();
            Destroy(collision.gameObject);
            PlayEffect();
        }

    }

    private void PlayEffect()
    {
        Vector3 newPos = transform.position;
        ParticleSystem p = Instantiate(pickupEffect, newPos, Quaternion.identity);
        p.Play();
    }

    private void TakeHit()
    {
        if (!flashing)
        {
            Vector3 newPos = transform.position;
            ParticleSystem p = Instantiate(explodeEffect, newPos, Quaternion.identity);
            p.Play();
            newPos.x = spawn.x;
            transform.position = newPos;
            FindObjectOfType<AudioManager>().Play("PlayerExplosion");
            Debug.Log("takehit aloitettu");
            flashing = true;
            ManageLives(-1);
            if (lives <= 0)
                GameManager.Instance.GameOver();
            else
                StartCoroutine(Flasher());
        }
    }

    private IEnumerator Flasher()
    { 
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Coroutine vaihe 2 aloitettu");
            playerSkin.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(.3f);
            playerSkin.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(.3f);
        }
        flashing = false;
    }

    //Get ether negative parameter for decreasing lifes or positive for increasing
    //Set all life-icons to false and then only active as many as there is lifes
    void ManageLives(int i)
    {
        if (i < 0)
            lives--;
        else if (i > 0)
            lives++;
        if (lives > 6)
            lives = 6;
        for (int x = 0; x < 6; x++)
        {
            lifeManager.transform.GetChild(x).gameObject.SetActive(false);
        }
        for (int a=0; a<lives; a++)
        {
            lifeManager.transform.GetChild(a).gameObject.SetActive(true);
        }
        gameManager.SetLives(lives);
    }
}
