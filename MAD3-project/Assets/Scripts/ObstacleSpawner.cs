//using System.Collections;
//using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner: MonoBehaviour
{
    private int initialPlots =5;
    //Change name
    private float size = 25;
    private float prevZPos = 50;
    private float zPos = 0;

    public List<GameObject> obstacles;
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
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];

        zPos = prevZPos - size;

        Instantiate(obstacle, new Vector3(0, 0, zPos), obstacle.transform.rotation, obstacleParent.transform);
        prevZPos += size;
    }
}