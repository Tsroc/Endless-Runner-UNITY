using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    /*
        EnergyBar manages the displayed energy bar on the UI, it is linked to the energy class.
            Energy bar info: https://www.youtube.com/watch?v=gHdXkGsqnlw
    */

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

    /*
        Adds energy. 
    */
    public void GainPowerup()
    {
        energy.GainEnergy();
    }

    /*
        Sets the energy difficulty level.
    */
    public void SetEnergyLevel(int level)
    {
        energy.SetDifficulty(level);
    }

    /*
        Returns the energy difficulty level.
    */
    public int GetDifficulty()
    {
        return energy.GetDifficulty();
    }

    /*
        Changes bar colour.
    */
    public void SetColor(Color color)
    {
        barImg.color = color;
    }

}

public class Energy
{
    /*
        Energy class manages the energy resource.
    */

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
        SetDifficulty(0);
    }

    public void Update()
    {
        current -= depletionRate * Time.deltaTime;
        current = Mathf.Clamp(current, 0f, MAX_ENERGY);
    }

    // == Accessor Methods
    public float GetCurrent()
    {
        return current;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    /*
        Increases the energy by the gainRate value. 
    */
    public void GainEnergy()
    {
        current += gainRate;
        current = Mathf.Clamp(current, 0f, MAX_ENERGY);
    }

    /*
        Returns an energy value between 0-1.
    */
    public float GetEnergyNormalized()
    {
        return current / MAX_ENERGY;
    }

    public void SetDifficulty(int iDifficulty)
    {
        difficulty = iDifficulty;

        switch(difficulty)
        {
            case 0:
                depletionRate = 0;
                break;
            case 1:
                depletionRate = DEPLETION_RATE_01;
                gainRate = GAIN_RATE_01;
                break;
            case 2:
                depletionRate = DEPLETION_RATE_02;
                gainRate = GAIN_RATE_02;
                break;
            case 3:
                depletionRate = DEPLETION_RATE_03;
                gainRate = GAIN_RATE_03;
                break;
            default:
                depletionRate = DEPLETION_RATE_01;
                gainRate = GAIN_RATE_01;
                break;
        }

    }

}
