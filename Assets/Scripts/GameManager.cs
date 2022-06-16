using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameOver;

    public GameObject gameOverUI;
    public GameObject scoreUI;
    public GameObject levelFinishedUI;

    public string nextlevel = "Level2";
    public int levelToUnlockIndex = 2;

    public int shrooms;
    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
            return;

        if (Input.GetKeyDown("e"))
        {
            LevelFailed();
        }

        if (Input.GetKeyDown("f"))
        {
            LevelWon();
        }
      
    }

    void LevelFailed()
    {
        GameOver = true;
        scoreUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void LevelWon()
    {
        scoreUI.SetActive(false);
        levelFinishedUI.SetActive(true);
        Debug.Log("LevelPassed");

       PlayerPrefs.SetInt("levelReached", levelToUnlockIndex);
       /* SceneManager.LoadScene(nextlevel);*/
    }
}
