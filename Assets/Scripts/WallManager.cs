using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {

        Vector2 rightSide = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector2 leftSide = Camera.main.ViewportToWorldPoint(Vector3.zero);

        rightSide.x += 0.5f;
        leftSide.x -= 0.5f;

        GameObject wallR = Instantiate(wall, this.transform);
        GameObject wallL = Instantiate(wall, this.transform);
        wallR.transform.position = rightSide;
        wallL.transform.position = leftSide;
    }
}
