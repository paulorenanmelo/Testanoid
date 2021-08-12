using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Input of the main scene (main menu) to start the gameplay.
/// </summary>
public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}