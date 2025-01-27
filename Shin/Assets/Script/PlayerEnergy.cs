using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    //[SerializeField] private int energyPoints = 0;
    [SerializeField] private int maxEnergyPoints = 100;
    [SerializeField] private float currentEnergyPoints; // puntos de vida actuales.
    [SerializeField] private float damageOverTime = 0.05f;//Cantidad de daño por segundo.
    public TMP_Text energyPointText;

    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentEnergyPoints = maxEnergyPoints;
        UpdateEnergyPointUI();
    }

    private void Update()
    {
        if (currentEnergyPoints > 0)
        {
            //Reduce la salud en funcion al tiempo.
            currentEnergyPoints -= damageOverTime * Time.deltaTime;
            //currentEnergyPoints -= Mathf.RoundToInt(damageOverTime * Time.deltaTime);
            UpdateEnergyPointUI();

            if (currentEnergyPoints <= 0)
            {
                currentEnergyPoints = 0;
                UpdateEnergyPointUI();

                if(playerController != null)
                {
                    playerController.Dead();
                }
            }
        }
    }

    private void UpdateEnergyPointUI()
    {
        if (energyPointText != null)
        {
            energyPointText.text = $"{currentEnergyPoints}";
        }
    }

    public void AddEneryPoit(int points)
    {
        // Incrementa la vida del jugador, asegurandose que no se supere el maximo.
        currentEnergyPoints = Mathf.Clamp(currentEnergyPoints + points, 0, maxEnergyPoints);
        UpdateEnergyPointUI();
    }

}
