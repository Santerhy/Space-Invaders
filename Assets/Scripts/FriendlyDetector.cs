using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyDetector : MonoBehaviour
{
    public bool friendlyInRange = false;

    public bool FriendlyInRage()
    {
        return friendlyInRange;
    }
    // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            friendlyInRange = true;
        else
            friendlyInRange = false;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            friendlyInRange = false;
    }


}

