using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    
    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
        //Application.Quit();
    }
}
