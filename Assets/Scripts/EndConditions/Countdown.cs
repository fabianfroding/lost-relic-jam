using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI countdownText;
    
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.countdown != 0)
        {
            countdownText.text = gameManager.countdown.ToString();
        }
        else {
            countdownText.text = "";
        }
    }

    public void ShowUIText()
    {

    }
}