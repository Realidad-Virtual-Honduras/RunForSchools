using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float followSpeed = 1.0f;
    [SerializeField]
    private Vector3 offSet;
    [SerializeField] private Vector3 offSetdead;
    private Vector3 lastPlayerPosition;


    void Update()
    {
        if (player != null && player.gameObject.activeSelf)
        {
            // Calcular la posición objetivo con el offset
            Vector3 targetPosition = transform.position + offSet;

            // Moverse hacia la posición objetivo con suavizado
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            lastPlayerPosition = targetPosition; //Actualiza la ultima posicion del jugador
        } else
        {
            transform.position = lastPlayerPosition + offSetdead;  
        }
    }
}
