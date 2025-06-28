using UnityEngine;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour
{
    public GameObject instructionScreen; // Assign the UI panel
    public Button startButton;

    void Start()
    {
        // Pause game
        Time.timeScale = 0f;

        // Show instructions
        instructionScreen.SetActive(true);

        // Listen for button click
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        // Unpause
        Time.timeScale = 1f;

        // Hide instructions
        instructionScreen.SetActive(false);
    }
}
