//using System.Collections;
//using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner: MonoBehaviour
{
    // Setting up the plots. 
    private int initialPlots = 3;
    private float plotSize = 25;
    private float prevZPos = 50;
    private float zPos = 0;

    private int trackCounter;
    [SerializeField] private int trackPerLevel;

    public List<GameObject> obstacles01;
    public List<GameObject> obstacles02;
    public List<GameObject> obstacles03;
    private GameObject obstacleParent;


    // Start is called before the first frame update
    void Start()
    {
        obstacleParent = GameObject.Find("ObstacleParent");
        if (!obstacleParent){
            obstacleParent = new GameObject("ObstacleParent");
        }

        for(int i = 0; i < initialPlots; i++)
        {
            SpawnObstacles();
        }
    }

    public void SpawnObstacles()
    {
        // Creating levels
        if(trackCounter < trackPerLevel)
        {
            SpawnEasyLevel();
        }
        else if (trackCounter < trackPerLevel*2)
        {
            SpawnMediumLevel();
        }
        else
        {
            SpawnHardLevel();    
        }

        trackCounter++;
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