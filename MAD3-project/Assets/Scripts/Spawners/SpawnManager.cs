using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*
        SpawnManager manages the three spawners, TrackSpawner, ObstacleSpawner and City Spawner.
    */

    [SerializeField] private int PLOTS_PER_DIFFICULTY = 10;
    [SerializeField] private int initialPlots = 3;
    [SerializeField] private int plotsToSpawn;

    private TrackSpawner trackSpawner;
    private CitySpawner citySpawner;
    private ObstacleSpawner obstacleSpawner;
    private DifficultyManager difficultyManager;

    private int plotsPlaced;

    void Start()
    {
        trackSpawner = GetComponent<TrackSpawner>();
        citySpawner = GetComponent<CitySpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        difficultyManager = GetComponent<DifficultyManager>();

        plotsPlaced = initialPlots;
        plotsToSpawn = initialPlots+2;
        Debug.Log("Difficulty: " + difficultyManager.GetDifficulty());
    }

    // == Accessor Methods
    public int GetInitialPlots()
    {
        return initialPlots;
    }

    public int GetPlotsToSpawn()
    {
        return plotsToSpawn;
    }

    public int GetPlotsPlaced()
    {
        return plotsPlaced;
    }

    /*
        When trigger is entered (Called from player script),
        Spawns plots and increases difficulty if necessary.
    */
    public void SpawnTriggerEntered()
    {
        SpawnPlot();

        if (plotsPlaced == PLOTS_PER_DIFFICULTY)
        {
            difficultyManager.SendMessage("IncreaseDifficulty");
        }
        if (plotsPlaced == PLOTS_PER_DIFFICULTY*2)
        {
            difficultyManager.SendMessage("IncreaseDifficulty");
        }
    }

    /*
        Spawns track, city and obstacle plots
    */
    private void SpawnPlot()
    {
        trackSpawner.SpawnTrack();
        citySpawner.SpawnCityPlot();
        obstacleSpawner.SpawnObstacles(difficultyManager.GetDifficulty());

        plotsPlaced++;
    }

}
