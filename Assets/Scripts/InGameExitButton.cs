using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameExitButton : MonoBehaviour
{
    public void ExitToMainMenu() => SceneManager.LoadScene("MainMenu");
}
