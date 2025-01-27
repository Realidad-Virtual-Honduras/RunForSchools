using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float minimalDistance; //Distancia para activar nuevo nivel
    [SerializeField] private Transform spawPoint;
    [SerializeField] private int inicialAmount; // cantidad inicial de partes por nivel a generar
    private Transform player;


    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;

       for (int i = 0; i < inicialAmount; i++) // Esto es para empezar el juego ya con objetos instanciados.
        {
            GenerateLevelParts();
        }

    }

    private void Update()
    {
        if (Vector2.Distance(player.position, spawPoint.position) < minimalDistance)
        {
            GenerateLevelParts();
        }
    }

    private void GenerateLevelParts()
    {
        GameObject level = LevelPool.Instance.RequestLevel();
        level.transform.position = spawPoint.position;
        spawPoint = FindEndPoint(level , "EndPoint");
    }

    private Transform FindEndPoint(GameObject levelPart , string tag )
    {
        Transform point = null;

        foreach(Transform location in levelPart.transform)
        {
            if(location.CompareTag(tag))
            {
                point = location;
                break;
            }
        }

        return point;
    }
}
