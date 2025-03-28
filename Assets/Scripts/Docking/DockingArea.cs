using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingArea : MonoBehaviour
{
    // Bool to keep track of docking status
    public bool playerIsDocked = false;

    // Timer for the powerboat being in the dock area
    private float timeInDock = 0;

    // The maximum amount of time the powerboat needs to be docked before mission completion
    [SerializeField] private float maxTimeInDock = 5f;

    // Reference to the powerboats game object to access its rotation for a portside check
    [SerializeField] private GameObject powerboat;

    public bool IsMissionComplete = false;

    void Update()
    {
        CheckDockingStatus();
    }

    private bool IsBoatPortside()
    {
        // Checks if the power boat is portside by looking at its rotation with a buffer of 20f
        // Its 180f becuase in this case that's portside to the dock
        if (powerboat.transform.eulerAngles.y < 180f - 20f || powerboat.transform.eulerAngles.y > 180f + 20f)
        {
            return false;
        }
        else
        {
            return true;
        }
        // If we had multiple docks with different orientations then we could look at the rotation of that and compare it to our boats rotation
    }

    private void CheckDockingStatus()
    {
        // Track docking time only if the power boat is docked and properly aligned
        if (playerIsDocked && IsBoatPortside())
        {
            timeInDock += Time.deltaTime;
            if (timeInDock > maxTimeInDock)
            {
                // When it reaches max time in dock set a bool for the UI
                IsMissionComplete = true;
            }
        }
        else
        {
            // Resets docking timer if conditions aren't met
            timeInDock = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsDocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsDocked = false;
        }
    }
}
