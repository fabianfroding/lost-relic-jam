using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinished : MonoBehaviour
{
    [SerializeField]
    string nextLevelName;

    public void NextLevel()
    {
      //TODO: check if there are levels left to load!!
      SceneManager.LoadScene(nextLevelName);
      
    }
}
