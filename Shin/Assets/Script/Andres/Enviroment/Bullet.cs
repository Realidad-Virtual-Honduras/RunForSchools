using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DestroyObj(gameObject);
    }

    private IEnumerator DestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        Destroy(obj);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<OutBorders>() != null)
        {
            Destroy(gameObject);
        }
    }
}
