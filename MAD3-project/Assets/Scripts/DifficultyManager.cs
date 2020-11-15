using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private int difficulty;
    private EnergyBar energy;

    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();
        difficulty = 1;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

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
        Debug.Log("Increase difficulty invoked.");
    }
}
