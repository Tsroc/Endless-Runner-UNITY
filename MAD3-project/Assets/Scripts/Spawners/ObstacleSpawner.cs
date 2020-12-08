//using System.Collections;
//using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner: MonoBehaviour
{
    /*
        ObstacleSpawner spawns obstacle plots along track.
        There are three difficulty levels for obstacles, obstacles spawned are based on the difficulty level selected.
    */

    [SerializeField] private List<GameObject> obstacles01Prefab;
    [SerializeField] private List<GameObject> obstacles02Prefab;
    [SerializeField] private List<GameObject> obstacles03Prefab;

    private SpawnManager spawnManager;
    private GameObject obstacleParent;

    private List<GameObject> obstaclePlots;
    private float plotSize = 25;
    private float prevZPos = 50;
    private float zPos = 0;

    void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
        obstaclePlots = new List<GameObject>();

        obstacleParent = GameObject.Find("ObstacleParent");
        if (!obstacleParent){
            obstacleParent = new GameObject("ObstacleParent");
        }

        SetupInitialPlots();
        
    }

    /*
        Spawns an obstacle based on which difficulty level arg is passed.
    */
    public void SpawnObstacles(int level)
    {
        zPos = prevZPos - plotSize;
        prevZPos += plotSize;
        GameObject obstacle = Instantiate(getRandomObstacle(level), new Vector3(0, 0, zPos), new Quaternion(0, 0, 0, 0), obstacleParent.transform);
        obstaclePlots.Add(obstacle);

        DeleteUnusuedPlots();
    }

    /*
        Deletes the plots at start if the plot size is greater than the specified number.
    */
    private void DeleteUnusuedPlots()
    {
        if(obstaclePlots.Count > spawnManager.GetPlotsToSpawn())
        {
            GameObject obstacle = obstaclePlots[0];
            obstaclePlots.Remove(obstacle);
            Destroy(obstacle);
        }
    }

    /*
        Returns a random obstacle path based on level arg passed.
    */
    private GameObject getRandomObstacle(int level)
    {
        switch (level)
        {
            case 1:
                return obstacles01Prefab[Random.Range(0, obstacles01Prefab.Count)];
                break;
            case 2:
                return obstacles02Prefab[Random.Range(0, obstacles02Prefab.Count)];
                break;
            case 3:
                return obstacles03Prefab[Random.Range(0, obstacles03Prefab.Count)];
                break;

            default:
                return null;
                break;
        }
    }

    /*
        Spawns the initial obstacles.
    */
    private void SetupInitialPlots()
    {
        for(int i = 0; i < spawnManager.GetInitialPlots(); i++)
        {
            SpawnObstacles(1);
        }
    }
}