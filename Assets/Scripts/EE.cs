using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EE : MonoBehaviour

{
    private int clickCounter = 0;
    public GameObject eEPanel;
    public Text eEText;
    private float delay = 0.1f;
    public string fullText;
    private string currentText = "";
    private bool textShown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Clicker()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        clickCounter++;
        if (clickCounter >= 5)
            SetPanelActive();
    }

    public void SetPanelActive()
    {
        eEPanel.SetActive(true);
        StartCoroutine(ShowText());
    }

    public void ClosePanel()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        currentText = "";
        eEPanel.SetActive(false);
        textShown = false;
    }

    IEnumerator ShowText()
    {
        if (textShown == false)
        {
            textShown = true;
            for (int i = 0; i < fullText.Length; i++)
            {
                currentText += fullText[i];
                if (i == 10)
                    currentText += "\n";
                else if (i == 54)
                    currentText += "\n";
                eEText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }
    }


}
