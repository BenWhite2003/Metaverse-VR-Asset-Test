using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PowerboatUI : MonoBehaviour
{
    [SerializeField] private DockingArea dockingArea;
    [SerializeField] private GameObject missionCompletePanel;
    [SerializeField] private Button missionCompleteResetButton;

    void Update()
    {
        if (dockingArea.IsMissionComplete)
        {
            // Displays the mission complete panel
            missionCompletePanel.SetActive(true);
            // Freezes time to stop all movement and collsions
            Time.timeScale = 0;
        }
    }

    // Reset game method used by UI buttons
    public void ResetGame()
    {
        // Loads the current scene (resets game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
