using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI shroomsLeft;
    public static event Action OnLevelRestartResetScore;

    private void OnEnable()
    {
        //shroomsLeft.text = PlayerStats.Shrooms.ToString();
    }

    public void Restart()
    {
        OnLevelRestartResetScore?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        OnLevelRestartResetScore?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}
