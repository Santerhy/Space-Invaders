using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Animator mSpeedA;
    public Animator aSpeedA;
    public Image mSpeedS;
    public Image aSpeedS;

    // Start is called before the first frame update
    void Start()
    {
        mSpeedS.gameObject.SetActive(false);
        aSpeedS.gameObject.SetActive(false);
        mSpeedA = mSpeedS.GetComponent<Animator>();
        aSpeedA = aSpeedS.GetComponent<Animator>();
    }

    public void ActivateMSpeed()
    {
        mSpeedS.gameObject.SetActive(true);
    }

    public void ActivateASpeed()
    {
        aSpeedS.gameObject.SetActive(true);
    }

    public void StartASpeedAnimation()
    {
        aSpeedA.SetBool("IsEnding", true);
    }

    public void StartMSpeedAnimation()
    {
        mSpeedA.SetBool("IsEnding", true);
    }

    public void DisableASpeed()
    {
        aSpeedA.SetBool("IsEnding", false);
        aSpeedS.gameObject.SetActive(false);
    }

    public void DisableMSpeed()
    {
        mSpeedA.SetBool("IsEnding", false);
        mSpeedS.gameObject.SetActive(false);
    }
}
