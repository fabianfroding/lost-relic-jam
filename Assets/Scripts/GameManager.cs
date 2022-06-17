using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ExplosiveSelect explosiveSelect;

    public static bool GameOver;
    public GameObject gameOverUI;

    public float remainingGameoverTimeout;
    public const float gameoverTimeoutDelay = 5f;
    private Coroutine gameoverTimeoutCoroutine = null;

    public GameObject scoreUI;
    public GameObject levelFinishedUI;


    // hard coded scene name to the next level
    public string nextlevel;
    // progression - tells which level player can access
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
        Debug.Log("Initial Shrooms: " + currentShroomNumber);

        InputManager.OnWinLevel += OnWinLevel;
        InputManager.OnRestartLevel += OnRestartLevel;
        Enemy.OnEnemyDeath += OnEnemyDeath;
        Explosive.OnExplosion += OnExplosion;
        PlaceShrooms.OnShroomPlaced += OnShroomPlaced;
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
    }

    private void OnRestartLevel()
    {
        LevelFailed();
    }
    private void OnWinLevel()
    {
        LevelWon();
    }

    private void OnEnemyDeath(int score)
    {
        currentEnemyNumber--;
        // Debug.Log("Enemies left: " + currentEnemyNumber);
    }

    private void OnExplosion()
    {
        currentShroomNumber--;
        Debug.Log("Shrooms left: " +  currentShroomNumber);

        BeginTimeoutUntilLose();
    }

    public void BeginTimeoutUntilLose()
    {
        // ensure only 1 coroutine is run
        if (gameoverTimeoutCoroutine != null)
        {
            StopCoroutine(gameoverTimeoutCoroutine);
        }
        gameoverTimeoutCoroutine = StartCoroutine(LoseAfterDelay());
    }

    private IEnumerator LoseAfterDelay()
    {
        // yield return new WaitForSeconds(gameoverTimeoutDelay);
        int countdown = 0;
        int prevCountdown = 0;

        for(remainingGameoverTimeout = gameoverTimeoutDelay; remainingGameoverTimeout > 0; remainingGameoverTimeout -= Time.deltaTime)
        {
            if (remainingGameoverTimeout <= 3.5f)
            {
                countdown = Mathf.FloorToInt(remainingGameoverTimeout % 60);
                if (countdown != prevCountdown) 
                {
                    Debug.Log("Losing in: " + countdown);
                    prevCountdown = countdown;
                }
            }
            yield return null;
        }
        LevelFailed();
    }


    private void OnShroomPlaced()
    {
        currentShroomNumber++;
        // Debug.Log("Current Shrooms: " + currentShroomNumber);
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

    private void OnDestroy()
    {
        InputManager.OnWinLevel -= OnWinLevel;
        InputManager.OnRestartLevel -= OnRestartLevel;
        Enemy.OnEnemyDeath -= OnEnemyDeath;
        Explosive.OnExplosion -= OnExplosion;
        PlaceShrooms.OnShroomPlaced -= OnShroomPlaced;
    }

}
