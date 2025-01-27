using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BorderOutPos { Up, Down, Behind }
public class OutBorders : MonoBehaviour
{
    [SerializeField] private BorderOutPos borderOutPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        switch (borderOutPos)
        {
            case BorderOutPos.Up:
                if (obj.GetComponent<PlayerManager>() != null)
                {
                    LevelManager.instance.GameOver();
                }
                break;
            case BorderOutPos.Down:
                if (obj.GetComponent<PlayerManager>() != null)
                {
                    obj.GetComponent<PlayerManager>().rb.bodyType = RigidbodyType2D.Static;
                    LevelManager.instance.GameOver();
                }
                break;
            case BorderOutPos.Behind:
                if (obj.GetComponent<ItemController>() != null)
                {
                    obj.GetComponent<ItemController>().SetNewItem();
                }
                break;
            default:
                break;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
