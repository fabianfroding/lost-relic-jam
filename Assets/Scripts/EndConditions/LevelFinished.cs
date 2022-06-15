using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinished : MonoBehaviour
{  
    public void NextLevel()
    {
      //TODO: check if there are levels left to load!!
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
      
    }
}
