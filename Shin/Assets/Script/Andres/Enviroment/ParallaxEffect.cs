using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject cam = null;
    [SerializeField, Range(0,1)] private float parallaxFX = 0f;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    private float length;
    private float startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * (parallaxFX));
        float temp = (cam.transform.position.x * (1 - parallaxFX));

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}
