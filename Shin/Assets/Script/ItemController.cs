using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float energyPoints = 1;
    [SerializeField] private float[] energiesPoints;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;    
    private Collider2D _collider2D;
    private int randomTexture;

    [Space]
    public GameObject particles;
    public GameObject particlesAd;
    [SerializeField] private ParticleSystemRenderer _particleSystem;

    private Material particleFXs;
    private Texture particleTexture;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleFXs = _particleSystem.material;
        _collider2D = GetComponent<Collider2D>();
        SetNewItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<PlayerManager>() != null)
        {            
            StartCoroutine(Reactive());
            LevelManager.instance.energyTotal += energiesPoints[randomTexture];            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<PlayerManager>() != null)
        {
        }
    }
    private IEnumerator Reactive()
    {
        _collider2D.enabled = false;
        spriteRenderer.enabled = false;
        particles.SetActive(false);
        particlesAd.SetActive(false);
        SoundManager.instance.SoundOnPlace(SoundManager.instance.grabClip, new Vector3(0f, 0f, 0f));

        yield return new WaitForSeconds(1);

        _collider2D.enabled = true;
    }

    public void SetNewItem()
    {
        spriteRenderer.enabled = true;
        particles.SetActive(true);
        particlesAd.SetActive(true);
        
        randomTexture = Random.Range(0, sprites.Length);

        spriteRenderer.sprite = sprites[randomTexture];

        particleTexture = sprites[randomTexture].texture;

        particleFXs.SetTexture("_BaseMap", particleTexture);
        particleFXs.SetColor("_BaseColor", Color.white);
    }
}
