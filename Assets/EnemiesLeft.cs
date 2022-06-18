using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour
{
    public TextMeshProUGUI enemiesLeftText;
    int enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        enemiesLeftText = GetComponent<TextMeshProUGUI>();
        enemiesLeft = FindObjectsOfType<Enemy>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesLeftText.text = enemiesLeft + " ";
    }
}
