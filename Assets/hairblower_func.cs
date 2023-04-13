using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;



public class HairBlower : MonoBehaviour, IMixedRealityInputHandler
{
    public float windForce = 10.0f; // Force of the wind
    public float maxDistance = 5.0f; // Maximum distance the wind can reach

    private ParticleSystem particles; // Reference to the particle system component
    private bool isActive; // Flag to track whether the hairblower is active or not

    void Start()
    {
        // Get a reference to the particle system component
        particles = GetComponent<ParticleSystem>();

        // Register the script to receive input events from the MRTK toolkit
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityInputHandler>(this);
    }

    void OnDestroy()
    {
        // Unregister the script from input events when it is destroyed
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityInputHandler>(this);
    }

    void Update()
    {
        // Check if the hairblower is active
        if (isActive)
        {
            // Get the position of the hairblower and the direction it's facing
            Vector3 position = transform.position;
            Vector3 direction = transform.forward;

            // Create a raycast to detect the object in front of the hairblower
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, maxDistance))
            {
                // Check if the object is the ball
                if (hit.transform.CompareTag("Ball"))
                {
                    // Calculate the direction of the wind force
                    Vector3 windDirection = hit.point - position;
                    windDirection.Normalize();

                    // Apply the wind force to the ball
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    rb.AddForce(windDirection * windForce, ForceMode.Force);
                }
            }

            // Play the particle system to simulate wind
            particles.Play();
        }
        else
        {
            // Stop the particle system when the hairblower is not active
            particles.Stop();
        }
    }

    // Handle input events from the MRTK toolkit
    public void OnInputDown(InputEventData eventData)
    {
        // Check if the input was a tap or a click
        if (eventData.MixedRealityInputAction == MixedRealityInputAction.Tap ||
            eventData.MixedRealityInputAction == MixedRealityInputAction.Select)
        {
            // Activate the hairblower
            isActive = true;
        }
    }

    public void OnInputUp(InputEventData eventData)
    {
        // Deactivate the hairblower when the input is released
        isActive = false;
    }
}
