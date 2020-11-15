using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    // Energy bar info: https://www.youtube.com/watch?v=gHdXkGsqnlw
    private Image barImg;
    private Energy energy;
    private GameObject player;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        barImg = transform.Find("Energy").GetComponent<Image>();
        energy = new Energy();
        SetColor(Color.green);
    }

    private void Update()
    {
        if(energy != null)
        {
            if(energy.GetCurrent() == 0)
            {
                player.SendMessage("EnergyDepleted");
                energy = null;
            }

            energy.Update();
            barImg.fillAmount = energy.GetEnergyNormalized();
        }
    }

    public void GainPowerup()
    {
        energy.GainEnergy();
    }

    public void SetEnergyLevel(int level)
    {
        switch(level)
        {
            case 1: 
                energy.SetDifficultyEasy();
                break;
            case 2:
                energy.SetDifficultyMedium();
                break;
            case 3:
                energy.SetDifficultyHard();
                break;
            default:
                energy.SetDifficultyEasy();
                break;
        }
    }

    public int GetDifficulty()
    {
        return energy.GetDifficulty();
    }

    // Changing bar colour.
    public void SetColor(Color color)
    {
        barImg.color = color;
    }

}

public class Energy
{
    public const int MAX_ENERGY = 100;

    private float current;
    private float depletionRate;
    private float gainRate;
    private int difficulty;

    private float DEPLETION_RATE_01 = 5f;
    private float DEPLETION_RATE_02 = 10f;
    private float DEPLETION_RATE_03 = 15f;

    private float GAIN_RATE_01 = 50f;
    private float GAIN_RATE_02 = 30f;
    private float GAIN_RATE_03 = 30f;


    public Energy()
    {
        current = 100;
        SetDifficultyEasy();
    }

    public void Update()
    {
        current -= depletionRate * Time.deltaTime;
        current = Mathf.Clamp(current, 0f, MAX_ENERGY);
        //Debug.Log(current);
    }

    public float GetCurrent()
    {
        return current;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    public void GainEnergy()
    {
        current += gainRate;
        current = Mathf.Clamp(current, 0f, MAX_ENERGY);
    }

    public float GetEnergyNormalized()
    {
        return current / MAX_ENERGY;
    }

    public void SetDifficultyEasy()
    {
        difficulty = 1;
        depletionRate = DEPLETION_RATE_01;
        gainRate = GAIN_RATE_01;
    }

    public void SetDifficultyMedium()
    {
        difficulty = 2;
        depletionRate = DEPLETION_RATE_02;
        gainRate = GAIN_RATE_02;
    }

    public void SetDifficultyHard()
    {
        difficulty = 3;
        depletionRate = DEPLETION_RATE_03;
        gainRate = GAIN_RATE_03;
    }
}
