using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    TrackSpawner trackSpawner;
    CitySpawner citySpawner;
    ObstacleSpawner obstacleSpawner;
    DifficultyManager difficultyManager;

    private int PLOTS_PER_DIFFICULTY = 10;
    private int initialPlots = 3;    //this variable may be changed.
    private int plotsPlaced;

    // Start is called before the first frame update
    void Start()
    {
        trackSpawner = GetComponent<TrackSpawner>();
        citySpawner = GetComponent<CitySpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        difficultyManager = GetComponent<DifficultyManager>();

        plotsPlaced = initialPlots;
        Debug.Log("Difficulty: " + difficultyManager.GetDifficulty());
    }

    public void SpawnTriggerEntered()
    {
        SpawnPlot();
    }

    private void SpawnPlot()
    {
        trackSpawner.MoveTrack();
        citySpawner.SpawnCityPlot();
        obstacleSpawner.SpawnObstacles(difficultyManager.GetDifficulty());

        plotsPlaced++;

        if (plotsPlaced == PLOTS_PER_DIFFICULTY)
        {
            difficultyManager.SendMessage("IncreaseDifficulty");
        }
        if (plotsPlaced == PLOTS_PER_DIFFICULTY*2)
        {
            difficultyManager.SendMessage("IncreaseDifficulty");
        }
    }

    // == Accessor Methods
    public int GetInitialPlots()
    {
        return initialPlots;
    }

    public int GetPlotsPlaced()
    {
        return plotsPlaced;
    }
}
