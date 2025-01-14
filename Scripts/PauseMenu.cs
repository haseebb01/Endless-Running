using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;  // Reference to the pause menu UI

    private bool isPaused = false;

    void Update()
    {
        // Check if the player presses the Escape key to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Function to resume the game
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;  // Resume time
        isPaused = false;
    }

    // Function to pause the game
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;  // Freeze time
        isPaused = true;
    }

    // Function to replay the game (reload the current scene)
    public void Replay()
    {
        Time.timeScale = 1f;  // Ensure time is running normally
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Function to go back to the main menu
    public void Home()
    {
        Time.timeScale = 1f;  // Ensure time is running normally
        SceneManager.LoadScene("Menu");  // Replace "MainMenu" with the name of your main menu scene
    }

    // These functions should be assigned to the buttons
    public void OnResumeButtonClicked()
    {
        Resume();
    }

    public void OnReplayButtonClicked()
    {
        Replay();
    }

    public void OnHomeButtonClicked()
    {
        Home();
    }
}
