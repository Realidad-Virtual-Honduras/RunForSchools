using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordDistance : MonoBehaviour
{
    public TMP_Text distanceText;
    private Vector3 startPosition;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
    }

    void Update()
    {
        //Calcular la distancia recorrida
        float distance = Vector3.Distance(startPosition,playerTransform.position);

        //Convertir la distancia a kilometros y mostrar en la UI
        distanceText.text = (distance / 100f).ToString("F2") + "KM";
        
    }
}
