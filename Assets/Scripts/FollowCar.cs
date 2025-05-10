using UnityEngine;

public class FollowCar : MonoBehaviour
{

    public Transform carTransform; 
    public Transform followCarCamera; 

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(carTransform); 
        transform.position = Vector3.SmoothDamp(
        transform.position,             // current position of the camera
        followCarCamera.position,       // target position you want to reach
        ref velocity,                  // a reference to the current velocity, updated by SmoothDamp
        5f * Time.deltaTime           // smooth time (how fast to reach the target)
        );
 
    }
}
