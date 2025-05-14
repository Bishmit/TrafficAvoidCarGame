using UnityEngine;

public class VehiclesDestroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   void OnTriggerEnter(Collider other)
   {
    Destroy(other.transform.parent.gameObject); 
   } 
}
