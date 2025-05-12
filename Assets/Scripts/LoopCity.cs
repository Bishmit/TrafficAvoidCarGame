using UnityEngine;

public class LoopCity : MonoBehaviour
{
    [SerializeField] Transform carTransform; 
    [SerializeField] Transform nextCityTransform; 
    [SerializeField] float halfLength = 0f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(carTransform.position.z > transform.position.z + halfLength + 10f){
        transform.position = new Vector3(0, 0, nextCityTransform.position.z + halfLength * 2); 
       }
    }
}
