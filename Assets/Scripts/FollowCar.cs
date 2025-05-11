using UnityEngine;

public class FollowCar : MonoBehaviour
{
    private Transform carTransform; 
    private Transform followCarCamera; 

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Find the player car by tag
        GameObject playerCar = GameObject.FindWithTag("Player");
        if (playerCar != null)
        {
            carTransform = playerCar.transform;
            // Find the CameraPoint child inside the car
            followCarCamera = playerCar.transform.Find("followCarCamera");

            if (followCarCamera == null)
            {
                Debug.LogError("CameraPoint not found under Player car!");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
        }
    }

    void FixedUpdate()
    {
        if (carTransform == null || followCarCamera == null)
            return;

        // Make the camera look at the car
        transform.LookAt(carTransform);

        // Smoothly move the camera to the follow point
        transform.position = Vector3.SmoothDamp(
            transform.position,
            followCarCamera.position,
            ref velocity,
            0.3f  // Smoother and more realistic response time
        );
    }
}

