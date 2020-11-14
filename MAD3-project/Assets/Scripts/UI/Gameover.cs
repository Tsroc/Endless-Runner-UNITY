using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;

    private void Start()
    {
        gameoverPanel.SetActive(false);
    }

    private void GameOver()
    {
        gameoverPanel.SetActive(true);
    }
}
