using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro input field

public class MainMenu : MonoBehaviour
{
    public TMP_InputField playerNameInputField; // Reference to the input field for player name

    public void PlayGame()
    {
        // Save player name before starting the game
        SavePlayerName();
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SavePlayerName()
    {
        if (playerNameInputField != null && !string.IsNullOrEmpty(playerNameInputField.text))
        {
            PlayerPrefs.SetString("PlayerName", playerNameInputField.text);
            PlayerPrefs.Save();
            Debug.Log("Player Name Saved: " + playerNameInputField.text);
        }
        else
        {
            Debug.LogWarning("Player Name is empty or input field is not assigned.");
        }
    }
}
