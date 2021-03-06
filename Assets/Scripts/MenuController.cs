using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject creditsCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("levelReached", 1);
        SceneManager.LoadScene("LevelSelect");
    }

    public void OpenCredits()
    {
        creditsCanvas.SetActive(true);
    }
    public void CloseCredits()
    {
        creditsCanvas.SetActive(false);
    }
}
