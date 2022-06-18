using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public static int countdownNumber;
    public TextMeshProUGUI countdownText;
    
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownNumber != 0)
        {
            countdownText.text = countdownNumber.ToString();
        }
        else {
            countdownText.text = "";
        }
    }

}