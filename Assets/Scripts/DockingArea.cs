using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingArea : MonoBehaviour
{
    public bool playerIsDocked = false;

    // Timer for the powerboat being in the dock area
    private float timeInDock = 0;

    // The maximum amount of time the powerboat needs to be docked before mission completion
    [SerializeField] private float maxTimeInDock = 5f;

    // Reference to the powerboats game object to access its rotation for a portside check
    [SerializeField] private GameObject powerboat;
    
    // Update is called once per frame
    void Update()
    {
        CheckDockingStatus();
    }

    private bool IsBoatPortside()
    {
        // Checks if the power boats rotation is within 20f of 180f (portside)
        return powerboat.transform.eulerAngles.y < 160f && powerboat.transform.eulerAngles.y > 200f;
    }

    private void CheckDockingStatus()
    {
        // Track docking time only if the power boat is docked and properly aligned
        if (playerIsDocked && IsBoatPortside())
        {
            timeInDock += Time.deltaTime;
            if (timeInDock > maxTimeInDock)
            {
                // When it reaches max time in dock, debug a message for now
                Debug.Log("Mission Compelte!");
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
