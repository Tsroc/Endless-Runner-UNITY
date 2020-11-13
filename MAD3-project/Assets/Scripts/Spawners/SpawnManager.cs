using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    TrackSpawner trackSpawner;
    CitySpawner citySpawner;
    ObstacleSpawner obstacleSpawner;

    // Start is called before the first frame update
    void Start()
    {
        trackSpawner = GetComponent<TrackSpawner>();
        citySpawner = GetComponent<CitySpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTriggerEntered()
    {
        trackSpawner.MoveTrack();
        citySpawner.SpawnCityPlot();
        obstacleSpawner.SpawnObstacles();

    }
}
