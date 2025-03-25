using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SunseekerMovement : MonoBehaviour
{
    // The 3 points in our quadratic bazier curve
    [SerializeField] private Transform startPoint, controlPoint, endPoint;
    private int currentCurveIndex = 0;

    // Time variable ranging from 0 to 1
    private float t = 0f;

    private SunseekerCircuit sunseekerCircuit;

    void Start()
    {
        sunseekerCircuit = GetComponent<SunseekerCircuit>();
    }

    void Update()
    {
        // Create a bezier curve using an array of bezier curves, the current index, and the start, control and end points
        Vector3 bezierCurve = CalculateBezierCurve(sunseekerCircuit.circuit[currentCurveIndex].startPoint.position, sunseekerCircuit.circuit[currentCurveIndex].controlPoint.position, sunseekerCircuit.circuit[currentCurveIndex].endPoint.position, t);

        // Apply the bezier curve to the boats position (using the boats y positon to ensure it stays in the right place
        transform.position = new Vector3(bezierCurve.x, transform.position.y, bezierCurve.z);

        // Slowly increase time variable over time
        t += Time.deltaTime * 0.1f;

        // Resets time back to 0 if it exceeds 1
        if (t > 1f)
        {
            t = 0f;
            // Moves onto the next bezier curve when we reach the end of the current one
            currentCurveIndex++;
        }

        if (currentCurveIndex >= sunseekerCircuit.circuit.Length)
        {
            // Reset the curve index back to 0 when it reaches the end of the array
            currentCurveIndex = 0;
        }
        // Calculate the bezier curve and store it in a Vector3
        Vector3 bezierPoint = CalculateBezierCurve(startPoint.position, controlPoint.position, endPoint.position, t);

        

        // Calculate direction to look at (minus becuase it was initially facing the opposite way)
        Vector3 direction = -(CalculateBezierCurve(sunseekerCircuit.circuit[currentCurveIndex].startPoint.position, sunseekerCircuit.circuit[currentCurveIndex].controlPoint.position, sunseekerCircuit.circuit[currentCurveIndex].endPoint.position, t + 0.01f) - transform.position).normalized;

        // Rotate the boat in the desired direction
        RotateBoat(direction);
    }

    private Vector3 CalculateBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        // Equation to calculate the bezier curve the boat will be following
        Vector3 b = (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;

        return b;
    }

    private void RotateBoat(Vector3 direction)
    {
        // Get the angle of rotation around Y-axis only
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Create a rotation around the Y-axis only
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}
