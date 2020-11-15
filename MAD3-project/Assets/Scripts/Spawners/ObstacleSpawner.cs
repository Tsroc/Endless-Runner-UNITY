//using System.Collections;
//using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner: MonoBehaviour
{
    SpawnManager spawnManager;
    // Setting up the plots. 
    private float plotSize = 25;
    private float prevZPos = 50;
    private float zPos = 0;

    public List<GameObject> obstacles01;
    public List<GameObject> obstacles02;
    public List<GameObject> obstacles03;
    private GameObject obstacleParent;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GetComponent<SpawnManager>();

        obstacleParent = GameObject.Find("ObstacleParent");
        if (!obstacleParent){
            obstacleParent = new GameObject("ObstacleParent");
        }

        SetupInitialPlots();

    }

    public void SpawnObstacles(int level)
    {
        switch(level)
        {
            case 1:
                SpawnEasyLevel();
                break;
            case 2:
                SpawnMediumLevel();
                break;
            case 3:
                SpawnHardLevel();
                break;
            default:
                SpawnEasyLevel();
                break;
        }
    }

    private void SetupInitialPlots()
    {
        for(int i = 0; i < spawnManager.GetInitialPlots(); i++)
        {
            SpawnObstacles(1);
        }
    }

    private void SpawnEasyLevel()
    {
        /*
            Destroy obstacles after they are no longer required.
        */
        GameObject obstacle = obstacles01[Random.Range(0, obstacles01.Count)];

        zPos = prevZPos - plotSize;

        Instantiate(obstacle, new Vector3(0, 0, zPos), obstacle.transform.rotation, obstacleParent.transform);
        prevZPos += plotSize;
    }

    private void SpawnMediumLevel()
    {
        /*
            Destroy obstacles after they are no longer required.
        */
        GameObject obstacle = obstacles02[Random.Range(0, obstacles02.Count)];

        zPos = prevZPos - plotSize;

        Instantiate(obstacle, new Vector3(0, 0, zPos), obstacle.transform.rotation, obstacleParent.transform);
        prevZPos += plotSize;
    }

    private void SpawnHardLevel()
    {
        /*
            Destroy obstacles after they are no longer required.
        */
        GameObject obstacle = obstacles03[Random.Range(0, obstacles03.Count)];

        zPos = prevZPos - plotSize;

        Instantiate(obstacle, new Vector3(0, 0, zPos), obstacle.transform.rotation, obstacleParent.transform);
        prevZPos += plotSize;
    }
}