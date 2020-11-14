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
    }

    private void Update()
    {
        if(energy.GetCurrent() == 0)
        {
            player.SendMessage("EnergyDepleted");
        }

        energy.Update();
        barImg.fillAmount = energy.GetEnergyNormalized();
    }

    public void GainEnergy()
    {
        energy.GainEnergy();
    }

}

public class Energy
{
    public const int MAX_ENERGY = 100;

    private float current;
    private float depletionRate;
    private float gainRate;


    public Energy()
    {
        current = 100;
        depletionRate = 20f;
        gainRate = 20f;
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

    public void GainEnergy()
    {
        current += gainRate;
        current = Mathf.Clamp(current, 0f, MAX_ENERGY);
    }

    public float GetEnergyNormalized()
    {
        return current / MAX_ENERGY;
    }
}
