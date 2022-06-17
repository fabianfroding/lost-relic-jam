using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public static int scoreValue = 0;

    public TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        Enemy.OnEnemyDeath += AddToScore;
        GameOver.OnLevelRestart += ResetScore;
    }

    void Update()
    {
        scoreText.text = "Score: " + scoreValue;
    }
    private void AddToScore(int amount)
    {
        scoreValue += amount;
        scoreText.text = "Score: " + scoreValue;
    }

    private void ResetScore()
    {
        scoreValue = 0;
        scoreText.text = "Score: " + scoreValue;
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= AddToScore;
        GameOver.OnLevelRestart -= ResetScore;
    }
}
