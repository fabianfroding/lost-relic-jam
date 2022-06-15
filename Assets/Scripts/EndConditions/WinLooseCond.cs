using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLooseCond : MonoBehaviour
{
    bool gameEnd;

    public void WinGame()
    {
        if (!gameEnd)
        {
            Debug.Log("Ayy you won!");
            gameEnd = true;
        }
    }

    public void LooseGame()
    {
        if (!gameEnd)
        {
            Debug.Log("Aww you lost!");
            gameEnd = true;
        }
    }
}
