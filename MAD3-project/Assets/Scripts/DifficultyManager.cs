using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    /*
        DifficultyManager controlls the game difficulty. Difficulty has a level of 1, 2 or 3.
        Game difficulty is based on energy generation and depletion rates along with obstacle spawns. 
        The energy bar will change colour to communicate the change in difficulty to the player.
    */

    private int difficulty;
    private EnergyBar energy;

    void Start()
    {
        energy = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();
        difficulty = 1;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    /*
        Increases the difficulty to a max of difficulty level 3.
    */
    private void IncreaseDifficulty()
    {
        switch(difficulty)
        {
            case 1:
                difficulty = 2;
                energy.SetEnergyLevel(difficulty);
                energy.SetColor(Color.yellow);
                break;
            case 2:
            case 3:
                difficulty = 3;
                energy.SetEnergyLevel(difficulty);
                energy.SetColor(Color.red);
                break;
            default:
                difficulty = 1;
                energy.SetEnergyLevel(difficulty);
                break;
        }
        Debug.Log("Increased difficulty.");
    }
}
