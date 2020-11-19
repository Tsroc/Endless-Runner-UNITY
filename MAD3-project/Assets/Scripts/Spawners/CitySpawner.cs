//using System.Collections;
//using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    /*
        CitySpawner spawns city plots to the right and left of the track.
    */

    private SpawnManager spawnManager;
    private GameObject cityplotParent;
    [SerializeField] private List<GameObject> plotsPrefab;
    private List<GameObject> leftPlots;
    private List<GameObject> rightPlots;
    
    private float size = 25;
    private float xPosL = -17.5f;
    private float xPosR = 17.5f;
    private float zPosPrev = 0;
    private float zPos = 0;

    void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
        leftPlots = new List<GameObject>();
        rightPlots = new List<GameObject>();

        cityplotParent = GameObject.Find("CityplotParent");
        if (!cityplotParent){
            cityplotParent = new GameObject("CityplotParent");
        }

        SetupInitialPlots();
    }

    /*
        Spawns city plots on the left and right of the path.
    */
    public void SpawnCityPlot()
    {
        zPos = zPosPrev - size;
        zPosPrev += size;

        GameObject leftPlot = Instantiate(GetRandomPlot(), new Vector3(xPosL, 0, zPos), new Quaternion(0, 0, 0, 0), cityplotParent.transform);
        GameObject rightPlot = Instantiate(GetRandomPlot(), new Vector3(xPosR, 0, zPos), new Quaternion(0, 180, 0, 0), cityplotParent.transform);

        leftPlots.Add(leftPlot);
        rightPlots.Add(rightPlot);

        DeleteUnusuedPlots();

    }

    /*
        Deletes the plots at start if the plot size is greater than the specified number.
    */
    private void DeleteUnusuedPlots()
    {
        if(leftPlots.Count > spawnManager.GetPlotsToSpawn())
        {
            GameObject leftDel = leftPlots[0];
            GameObject rightDel = rightPlots[0];
            leftPlots.Remove(leftDel);
            rightPlots.Remove(rightDel);
            Destroy(leftDel);
            Destroy(rightDel);
        }
    }

    /*
        Returns a random plot from the plotPrefab list.
    */
    private GameObject GetRandomPlot()
    {
        return plotsPrefab[Random.Range(0, plotsPrefab.Count)];
    }

    /*
        Spawns the initial city plots.
    */
    private void SetupInitialPlots()
    {
        for(int i = 0; i < spawnManager.GetInitialPlots()+2; i++)
        {
            SpawnCityPlot();
        }
    }

}