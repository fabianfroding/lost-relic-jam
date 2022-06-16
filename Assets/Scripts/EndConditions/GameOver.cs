using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI shroomsLeft;

    private void OnEnable()
    {
        //shroomsLeft.text = PlayerStats.Shrooms.ToString();
    }

    public void Restart()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
