using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ExplosiveSelect explosiveSelect;

    public static bool GameOver;
    public GameObject gameOverUI;
    public GameObject scoreUI;
    public GameObject levelFinishedUI;

    public string nextlevel = "Level2";
    public int levelToUnlockIndex = 2;

    private Explosive[] allShroomsInLevel;
    public int currentShroomNumber;

    private Enemy[] allEnemiesInLevel;
    public int currentEnemyNumber;


    // Start is called before the first frame update
    void Start()
    {
        explosiveSelect = GetComponent<ExplosiveSelect>();
        GameOver = false;

        allEnemiesInLevel = FindObjectsOfType<Enemy>();
        currentEnemyNumber = allEnemiesInLevel.Length;

        allShroomsInLevel = FindObjectsOfType<Explosive>();
        currentShroomNumber = allShroomsInLevel.Length;
        // Debug.Log("Initial Shrooms: " + currentShroomNumber);

        Enemy.OnEnemyDeath += OnEnemyDeath;
        Explosive.OnExplosion += OnExplosion;
        PlaceShrooms.OnShroomPlaced += OnShroomPlaced;
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDeath -= OnEnemyDeath;
        Explosive.OnExplosion -= OnExplosion;
        PlaceShrooms.OnShroomPlaced -= OnShroomPlaced;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
            return;

        if (currentShroomNumber <= 0)
        {
            CheckRemainingEnemies();
        }

        if (Input.GetKeyDown("e"))
        {
            LevelFailed();
        }

        if (Input.GetKeyDown("f"))
        {
            LevelWon();
        }
    }

    private void OnEnemyDeath(int score)
    {
        currentEnemyNumber--;
        Debug.Log("Enemies left: " + currentEnemyNumber);
    }

    private void OnExplosion()
    {
        currentShroomNumber--;
        Debug.Log("Shrooms left: " + currentShroomNumber);
    }

    private void OnShroomPlaced()
    {
        currentShroomNumber++;
        Debug.Log("Current Shrooms: " + currentShroomNumber);
    }

    //should check if there are any enemies left, if no level won, if yes level failed but does not work
    private void CheckRemainingEnemies()
    {
        if (explosiveSelect.hasTriggeredBarrel && currentShroomNumber <= 0)
        {
            Debug.Log("Check remaining enemies called");
            if (currentEnemyNumber <= 0)
            {
                LevelWon();
            }
            else if (currentEnemyNumber > 0)
            {
                LevelFailed();
            }
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
