using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI speedText; 
    [SerializeField] TextMeshProUGUI distanceText;

    [SerializeField] TextMeshProUGUI scoreText; 
    [SerializeField] CarController carController; 

    [SerializeField] Transform playerCar; 

    [SerializeField] TrafficManager trafficManager; 
    private float previousZ; 
    private float speed = 0f; 

    private int score = 0; 
    private HashSet<GameObject> passedVehicles = new(); 
    void Start()
    {
        previousZ = playerCar.position.z;     
    }

    // Update is called once per frame
    void Update()
    {
        FindSpeedOfCarInUI();
        FindDistanceOfCarInUI(); 
        FindScoreOfCarUI();  
    }

    void FindSpeedOfCarInUI(){  
      speed = carController.GetCarSpeed(); 
      speedText.text = speed.ToString("0" + "Km/h");  
    }
    void FindDistanceOfCarInUI(){
        float currentZ = playerCar.position.z;
        float distanceMoved = Mathf.Abs(currentZ - previousZ);
        float distanceKm = distanceMoved / 1000f;
        distanceText.text = distanceKm.ToString("0.00") + "Km";
    }

    int FindScoreOfCar(){
       foreach(GameObject vehicle in trafficManager.spawnedVehicles){
        if(vehicle == null || passedVehicles.Contains(vehicle)) continue; 
        if(playerCar.position.z > vehicle.transform.position.z){
            passedVehicles.Add(vehicle); 
            score += 5; 
        }
       } 
       return score; 
    }

    void FindScoreOfCarUI(){
        scoreText.text = FindScoreOfCar().ToString("0"); 
    }
}
