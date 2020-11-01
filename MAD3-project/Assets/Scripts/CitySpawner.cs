//using System.Collections;
//using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    private int initialPlots =5;
    //Change name
    private float size = 25;
    private float xPosL = -17.5f;
    private float xPosR = 17.5f;
    private float lastSPos = 0;
    private float seePos = 0;

    public List<GameObject> plots;
    private GameObject cityplotParent;


    // Start is called before the first frame update
    void Start()
    {
        cityplotParent = GameObject.Find("CityplotParent");
        if (!cityplotParent){
            cityplotParent = new GameObject("CityplotParent");
        }

        for(int i = 0; i < initialPlots; i++)
        {
            SpawnCityPlot();
        }
    }

    public void SpawnCityPlot()
    {
            GameObject plotLeft = plots[Random.Range(0, plots.Count)];
            GameObject plotRight = plots[Random.Range(0, plots.Count)];

            seePos = lastSPos - size;

            Instantiate(plotLeft, new Vector3(xPosL, 0, seePos), plotLeft.transform.rotation, cityplotParent.transform);
            Instantiate(plotRight, new Vector3(xPosR, 0, seePos), new Quaternion(0, 180, 0, 0), cityplotParent.transform);
            lastSPos += size;
    }
}