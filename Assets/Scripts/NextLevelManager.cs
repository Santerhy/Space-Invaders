using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelManager : MonoBehaviour
{
    public Text clearedText;
    private GameManager gameManager;

    public void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void LevelCleared(int level)
    {
        clearedText.gameObject.SetActive(true);
        clearedText.text = "Level " + level + " cleared";

    }
}
