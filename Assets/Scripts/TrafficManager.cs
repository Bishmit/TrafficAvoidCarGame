using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private GameObject[] trafficVehicles;
    [SerializeField] private CarController carController;
    [SerializeField] private float minSpeedToSpawn = 10f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float maxSpawnsPerSecond = 5f;

    private float spawnTimer = 0f;

    void Update()
    {
        float carSpeed = carController.GetCarSpeed();

        if (carSpeed >= minSpeedToSpawn)
        {
            float t = Mathf.InverseLerp(minSpeedToSpawn, maxSpeed, carSpeed);
            float spawnsPerSecond = Mathf.Lerp(0.5f, maxSpawnsPerSecond, t);
            float spawnInterval = 1f / spawnsPerSecond;

            spawnTimer += Time.deltaTime;

            while (spawnTimer >= spawnInterval)
            {
                spawnTimer -= spawnInterval;
                SpawnVehicle();
            }
        }
    }

    void SpawnVehicle()
    {
        int laneIndex = Random.Range(0, lanes.Length);
        int vehicleIndex = Random.Range(0, trafficVehicles.Length);

        Instantiate(trafficVehicles[vehicleIndex], lanes[laneIndex].position, Quaternion.identity);
    }
}


