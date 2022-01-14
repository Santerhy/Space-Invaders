using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private PlayerController playerController;
    public float powerupAspeedTimer = 0.0f;
    public float powerupMspeedTimer = 0.0f;
    public float powerupTimerMaxA = 6.0f;
    public float powerupTimerMaxM = 10.0f;
    public bool powerupAspeedCheck = false;
    public bool powerupMspeedCheck = false;
    public bool aAnimationCalled = false;
    public bool mAnimationCalled = false;
    public UiManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        //uiManager = GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupAspeedCheck)
        {
            powerupAspeedTimer += Time.deltaTime;
        }
        if (powerupAspeedTimer > 7.0f && aAnimationCalled)
        {
            uiManager.StartASpeedAnimation();
            aAnimationCalled = true;
        }
        if (powerupAspeedTimer > powerupTimerMaxA)
            ResetAspeed();

        if (powerupMspeedCheck)
        {
            powerupMspeedTimer += Time.deltaTime;
        }
        if (powerupMspeedTimer > 7.0f && mAnimationCalled)
        {
            uiManager.StartMSpeedAnimation();
            mAnimationCalled = true;
        }
            if (powerupMspeedTimer > powerupTimerMaxM)
            ResetMspeed();
    }

    public void SetAspeed()
    {
        playerController.shootingTimerCounter = 0.5f;
        powerupAspeedCheck = true;
        powerupAspeedTimer = 0;
        uiManager.ActivateASpeed();
        
    }

    public void ResetAspeed()
    {
        powerupAspeedTimer = 0;
        powerupAspeedCheck = false;
        playerController.shootingTimerCounter = 1.2f;
        uiManager.DisableASpeed();
    }

    public void SetMspeed()
    {
        playerController.moveSpeed = 5.0f;
        powerupMspeedCheck = true;
        powerupMspeedTimer = 0;
        uiManager.ActivateMSpeed();

    }

    public void ResetMspeed()
    {
        powerupMspeedTimer = 0;
        powerupMspeedCheck = false;
        playerController.moveSpeed = 3.0f;
        uiManager.DisableMSpeed();
    }
}
