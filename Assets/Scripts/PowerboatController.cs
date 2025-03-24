using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerboatController : MonoBehaviour
{
    private Rigidbody powerboatRB;
    public float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxReverseSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    public bool isReversing = false;
    private Vector3 moveDirection;


    [SerializeField] private float turnSpeed;
    [SerializeField] private float angularDrag;
    private float turnInput;
    void Start()
    {
        // Get the Rigidbody component of the powerboat
        powerboatRB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        HandleInput();
        HandleCollision();
        MoveBoat();
        SteerBoat();
    }

    private void Accelerate()
    {
        // Check if the boat is in reverse gear
        if (!isReversing)
        {
            // Gradually increase the current speed towards max speed based on acceleration
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // Gradually increase the current speed towards max reverse speed based on acceleration
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxReverseSpeed, acceleration * Time.deltaTime);
        }
        
    }

    private void Decelerate()
    {
        // Gradually decrease the current speed towards 0 based on deceleration
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
    }

    private void MoveBoat()
    {
        // Move the boat forward or backward based on current speed and whether its in reverse
        if (!isReversing)
        {
            // Forward
            moveDirection = transform.forward * currentSpeed;
        }
        else
        {
            // Backward
            moveDirection = -transform.forward * currentSpeed;
        }
        // Apply the new velocity while maintaining the existing y velocity
        powerboatRB.velocity = new Vector3(moveDirection.x, powerboatRB.velocity.y, moveDirection.z);
    }
    private void SteerBoat()
    {
        // Checks if we are getting steering input and ensures the boat is moving before allowing steering
        if (turnInput != 0 && currentSpeed >= 1f)
        {
            // Ensures some turning even at high speeds
            float minTurnFactor = 0.3f;

            // Turning is stronger at low speeds and weaker at high speeds
            float speedFactor = Mathf.Max(1 - (currentSpeed / maxSpeed), minTurnFactor);

            // Adjust turn speed based on speed factor
            float adjustedTurnSpeed = turnSpeed * speedFactor;

            // Calculate turning amount based on input
            float turnAmount = turnInput * adjustedTurnSpeed * Time.deltaTime;

            // Apply rotational force using torque for smooth turning
            powerboatRB.AddTorque(transform.up * turnAmount, ForceMode.Acceleration);
        }

        // Apply angular drag to prevent infinite spinning and stabilize turning
        powerboatRB.angularDrag = angularDrag;
        
    }

   


    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Accelerate();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Decelerate();
        }

        // Checks for R key press and if the boat is near stationary (speed < 0.1) to avoid switching gears while the boat is moving
        if (Input.GetKeyDown(KeyCode.R) && Mathf.Abs(currentSpeed) < 0.1f)
        {
            // Toggle reverse gear
            isReversing = !isReversing; 
        }

        // Input for A and D (-1 and 1) for steering
        turnInput = Input.GetAxis("Horizontal");
    }

    private void HandleCollision()
    {
        if (isColliding)
        {
            Decelerate();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Obstacle"))
    //    {
    //        isColliding = true;
    //    }
    //    else
    //    {
    //        isColliding = false;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Obstacle"))
    //    {
    //        isColliding = false;
    //    }
    //    else
    //    {
    //        isColliding = true;
    //    }
    //}
}
