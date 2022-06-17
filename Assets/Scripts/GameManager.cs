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
    public int enemyNumberAtStart;
    public bool hasExploded = false;

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

        //tried to limit the function call to only when it explodes, still doesn't work
        if(hasExploded)
             CheckRemainingEnemies();

        if (Input.GetKeyDown("e"))
        {
            LevelFailed();
        }

        if (Input.GetKeyDown("f"))
        {
            LevelWon();
        }
      
    }

    //should check if there are any enemies left, if no level won, if yes level failed but does not work
    void CheckRemainingEnemies()
    {
        Debug.Log("Check remaining enemies called");
        if (enemyNumberAtStart <= 0)
        {
            LevelWon();
        }
        else if (enemyNumberAtStart > 0)
        {
            LevelFailed();
        }
    }

    public void LevelFailed()
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
