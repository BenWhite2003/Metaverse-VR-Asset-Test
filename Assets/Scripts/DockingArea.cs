using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingArea : MonoBehaviour
{
    public bool playerIsDocked = false;
    private float timeInDock = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsDocked)
        {
            timeInDock += Time.deltaTime;
            if (timeInDock > 5f)
            {
                Debug.Log("Mission Compelte!");
            }
        }
        else
        {
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
