using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PowerboatUI : MonoBehaviour
{
    [SerializeField] private DockingArea dockingArea;
    [SerializeField] private PowerboatController powerBoatController;
    [SerializeField] private GameObject missionCompletePanel;
    [SerializeField] private TextMeshProUGUI currentSpeedText;
    [SerializeField] private TextMeshProUGUI reverseText;
    [SerializeField] private GameObject resetButton;
    void Update()
    {
        HandleMissionComplete();
        UpdateSpeedUI();
        UpdateReversingStateUI();
    }

    // Reset game method used by UI buttons
    public void ResetGame()
    {
        // Loads the current scene (resets game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleMissionComplete()
    {
        if (dockingArea.IsMissionComplete)
        {
            // Displays the mission complete panel
            missionCompletePanel.SetActive(true);
            // Freezes time to stop all movement and collisions
            Time.timeScale = 0;

            // Hides the normal reset button so we don't have two on the screen
            resetButton.SetActive(false);
        }
        else
        {
            // Shows the normal reset button again after mission completion and reset
            resetButton.SetActive(true);
            // Sets the time scale back to normal to ensure we can move again after resetting
            Time.timeScale = 1.0f;
        }
    }

    private void UpdateSpeedUI()
    {
        // Casts current speed to an integer for cleaner UI
        int speedAsInt = (int)powerBoatController.currentSpeed;
        currentSpeedText.text = speedAsInt.ToString();
    }

    private void UpdateReversingStateUI()
    {
        // Toggles reverse text colour to show the player if they are reversing
        if (powerBoatController.isReversing)
        {
            reverseText.color = Color.green;
        }
        else
        {
            reverseText.color = Color.white;
        }
    }
}
