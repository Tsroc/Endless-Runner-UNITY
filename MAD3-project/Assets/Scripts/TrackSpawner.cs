using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner: MonoBehaviour
{
    public List<GameObject> tracks; 
    public GameObject trackPrefab;
    private float offset = 25f;
    private GameObject trackParent;

    // Start is called before the first frame update
    void Start()
    {
        if(tracks != null && tracks.Count > 0)
        {
            tracks = tracks.OrderBy(t => t.transform.position.z).ToList();
        }

        trackParent = GameObject.Find("TrackParent");
        if (!trackParent){
            trackParent = new GameObject("TrackParent");
        }
    }

    public void MoveTrack()
    {
        // Trying to move the objects position - run into issues.
        GameObject movedTrack = tracks[0];
        tracks.Remove(movedTrack);
        Destroy(movedTrack);
        float newZ = tracks[tracks.Count - 1].transform.position.z + offset;


        GameObject newTrack = Instantiate(trackPrefab, new Vector3(0, 0, newZ), Quaternion.identity, trackParent.transform);
        tracks.Add(newTrack);
    }
}
