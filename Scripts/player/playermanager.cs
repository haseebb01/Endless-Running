using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playermanager : MonoBehaviour
{
    public static bool gameover;
    public GameObject Gameoverpanel;

    public static bool isGameStarted;
    public GameObject StartingText;

    public static int numberofCoins;
    public TextMeshProUGUI coinsText;

    // Reference to PlayFabManager
    private PlayFabManager manager;

    void Start()
    {
        Time.timeScale = 1;
        gameover = false;
        isGameStarted = false;
        numberofCoins = 0;

        // Initialize PlayFabManager instance
        manager = new PlayFabManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            Gameoverpanel.SetActive(true);
            Time.timeScale = 0;

            // Send coins to PlayFab leaderboard
            manager.SendLeaderboard(numberofCoins);
        }

        coinsText.text = "Coins: " + numberofCoins;

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(StartingText);
        }
    }
}
