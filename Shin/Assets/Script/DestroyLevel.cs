using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DestroyLevel : MonoBehaviour
{
    private Transform player;
    [SerializeField] private int distanceToDestroy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        LevelDestroy();
    }

    private void LevelDestroy()
    {
        if (Vector2.Distance(player.position, transform.position) > distanceToDestroy)
        {
            gameObject.SetActive(false);
        }
    }
}

