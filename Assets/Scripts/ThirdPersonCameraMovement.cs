using UnityEngine;

public class ThirdPersonCameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform playerCarTransform; 
    [SerializeField] float offSet = -10; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
       Vector3 cameraPos = transform.position;
       cameraPos.z = playerCarTransform.position.z + offSet;
       cameraPos.x = playerCarTransform.position.x;  
       transform.position = cameraPos;   
    }
}
