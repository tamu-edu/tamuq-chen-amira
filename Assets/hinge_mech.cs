using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hinge_mech : MonoBehaviour
{
    public float speed = 1.0f; // Speed of the pendulum swing
    public float angle = 30.0f; // Angle of the pendulum swing
    public float radius = 1.0f; // Radius of the pendulum swing

    private HingeJoint hinge; // Reference to the hinge joint component
    private float time; // Current time

    void Start()
    {
        // Get a reference to the hinge joint component
        hinge = GetComponent<HingeJoint>();
    }

    void Update()
    {
        // Calculate the current angle of the pendulum based on time and speed
        float currentAngle = Mathf.Sin(time * speed) * angle;

        // Convert the angle to radians and calculate the position of the sphere
        float radians = currentAngle * Mathf.Deg2Rad;
        float x = Mathf.Sin(radians) * radius;
        float y = Mathf.Cos(radians) * radius;

        // Set the position of the sphere
        transform.localPosition = new Vector3(x, y, 0);

        // Update the time
        time += Time.deltaTime;
    }
}

