using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner: MonoBehaviour
{
    /*
        TrackSpawner spawns the tracks along which the player travels.
    */

    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private List<GameObject> tracks; 
    private GameObject trackParent;

    private float offset = 25f;

    void Start()
    {
        OrderTracks();

        trackParent = GameObject.Find("TrackParent");
        if (!trackParent){
            trackParent = new GameObject("TrackParent");
        }
    }
    
    /*
        Orders tracks by z position
    */
    private void OrderTracks()
    {
        if(tracks != null && tracks.Count > 0)
        {
            tracks = tracks.OrderBy(t => t.transform.position.z).ToList();
        }
    }

    public void SpawnTrack()
    {
        GameObject movedTrack = tracks[0];
        tracks.Remove(movedTrack);
        Destroy(movedTrack);
        float newZ = tracks[tracks.Count - 1].transform.position.z + offset;

        GameObject newTrack = Instantiate(trackPrefab, new Vector3(0, 0, newZ), Quaternion.identity, trackParent.transform);
        tracks.Add(newTrack);
    }
}
