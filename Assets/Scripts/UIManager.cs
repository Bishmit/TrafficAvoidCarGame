using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement; 
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
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI totalDistance;
    [SerializeField] TextMeshProUGUI maxSpeed;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] GameObject speedIcon;
    [SerializeField] GameObject distanceIcon;
    [SerializeField] GameObject scoreIcon;

    private float previousZ;
    private float speed = 0f;
    private float maxSpeed_ = 0f;
    private int score = 0;
    private HashSet<GameObject> passedVehicles = new();
    void Start()
    {
        Time.timeScale = 1f;
        previousZ = playerCar.position.z;
        gameOverPanel.SetActive(false);
        speedIcon.SetActive(true);
        distanceIcon.SetActive(true);
        scoreIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        FindSpeedOfCarInUI();
        FindDistanceOfCarInUI();
        FindScoreOfCarUI();
        Console.Write(MaximumSpeed());
    }

    void FindSpeedOfCarInUI()
    {
        speed = carController.GetCarSpeed();
        speedText.text = speed.ToString("0" + "Km/h");
    }
    void FindDistanceOfCarInUI()
    {
        float currentZ = playerCar.position.z;
        float distanceMoved = Mathf.Abs(currentZ - previousZ);
        float distanceKm = distanceMoved / 1000f;
        distanceText.text = distanceKm.ToString("0.00") + "Km";
    }

    int FindScoreOfCar()
    {
        foreach (GameObject vehicle in trafficManager.spawnedVehicles)
        {
            if (vehicle == null || passedVehicles.Contains(vehicle)) continue;
            if (playerCar.position.z > vehicle.transform.position.z)
            {
                passedVehicles.Add(vehicle);
                score += 5;
            }
        }
        return score;
    }

    void FindScoreOfCarUI()
    {
        scoreText.text = FindScoreOfCar().ToString("0");
    }

    public void GameOver()
    {
        speedIcon.SetActive(false);
        distanceIcon.SetActive(false);
        scoreIcon.SetActive(false);

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        totalScoreText.text = scoreText.text;
        totalDistance.text = distanceText.text;
        maxSpeed.text = MaximumSpeed().ToString("0" + "Km/hr");
    }

    float MaximumSpeed()
    {
        float currentSpeed = speed;
        maxSpeed_ = Mathf.Max(currentSpeed, maxSpeed_);
        return maxSpeed_;
    }

    public void TryAgain()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); 
    }
}
