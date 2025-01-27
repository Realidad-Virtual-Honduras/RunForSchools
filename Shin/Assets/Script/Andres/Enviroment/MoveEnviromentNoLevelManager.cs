using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnviromentNoLevelManager : MonoBehaviour
{
    [Header("Speed Movemnt")]
    public float moveSpeed = 7f;

    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * moveSpeed, 0f, 0f);
    }
}
