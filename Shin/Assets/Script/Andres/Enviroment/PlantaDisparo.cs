using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaDisparo : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float speed;

    public void ShootBullet()
    {
        GameObject obj = Instantiate(bulletPrefab,shootPoint.position,Quaternion.identity, shootPoint);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed,0,0);
    }    
}
