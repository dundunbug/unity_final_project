using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add this api
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // When Unity Build, Scenes are queued with Index (like 0.GameMenu, 1.GameScene ...)
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
        // Debug message for preview
        print("Now you press QUIT");
    }
}
