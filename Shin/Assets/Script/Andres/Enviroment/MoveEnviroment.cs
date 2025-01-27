using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnviroment : MonoBehaviour
{
    [Header("Speed Movemnt")]
    public float moveSpeed = 7f;
    [SerializeField] private float addSpeed = 10f;
    public float deceleration = 0.5f;

    [Header("Distance")]
    [SerializeField] private float distace = 0f;
    [SerializeField] private float limit = 50f;
    [SerializeField] private float porcetage = 225f;

    [SerializeField] private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void FixedUpdate()
    {
        if (levelManager.canConsumeEnergy)
        {
            if (levelManager.energyTotal > 0)
            {
                distace = transform.position.x;

                if (distace > limit)
                {
                    moveSpeed += (moveSpeed / addSpeed);
                    limit += porcetage;
                }

                transform.Translate(Time.fixedDeltaTime * moveSpeed, 0f, 0f);
            }
        }
    }
}
