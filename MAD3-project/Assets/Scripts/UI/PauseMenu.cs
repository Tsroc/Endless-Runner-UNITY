using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    /*
        PauseMenu will toggle the pause panel on and off.
    */

    [SerializeField] private GameObject pausePanel;

    private static bool pause = false;

    void Start()
    {
        TogglePause(pause);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(pause);
        }
    }
    
    /*
        Toggles the pause functions.
        If true, sets timescale to 0 and displays the pause panel.
        If false, sets timescale to 1 and hides the pause panel.
    */
    public void TogglePause(bool paused)
    {
        pausePanel.SetActive(paused);
        
        if(paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        pause = !paused;
    }

}
