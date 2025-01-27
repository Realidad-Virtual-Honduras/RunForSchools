using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Collider2D colliders;
    private void Awake()
    {
        colliders = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.GetComponent<PlayerManager>() != null)
        {
            colliders.enabled = false;
            LevelManager.instance.GameOver();
        }
    }
}
