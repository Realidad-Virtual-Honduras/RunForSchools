using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private MoveEnviroment moveEnviroment;
    [SerializeField] private PlayerManager playerManager;
    private UiManager uiManager;

    public bool canConsumeEnergy;
    [Space]
    public float energyTotal;
    public float energyTotasl;
    [SerializeField] private float energyConsume;
    [Space]
    public Transform distance;
    [Space]
    [SerializeField] private float delayTiming;
    [SerializeField] private GameObject player;
    public float totalTime;


    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;

        uiManager = FindObjectOfType<UiManager>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Energy Consume
        if (canConsumeEnergy)
        {
            if(energyTotal >= 100)
                energyTotal = 100f;

            if (energyTotal > 0)
            {
                energyTotal -= energyConsume * Time.fixedDeltaTime;
                uiManager.SetEnergy(energyTotal);
            }
            else
            {
                uiManager.SetEnergy(0f);
                //uiManager.SetEnergyText((0f).ToString("F2"));                
                //GameOver();
            }

            totalTime += Time.fixedDeltaTime;
        }

        #endregion

        //Distance
        uiManager.SetDistanceText((distance.position.x / 100f).ToString("F2"));
    }

    #region GameOver
    public void GameOver()
    {
        if(canConsumeEnergy)
        {
            StartCoroutine(SlowDownAndStop());
        }
    }

    private IEnumerator SlowDownAndStop()
    {
        playerManager.animator.SetBool("Daño", true);
        while (moveEnviroment.moveSpeed > 0 || playerManager.bounceCount > 0)
        {
            moveEnviroment.moveSpeed -= moveEnviroment.deceleration * Time.deltaTime;

            if(playerManager.bounceCount > 0 && playerManager.IsGrounded())
            {
                playerManager.rb.velocity = new Vector2(playerManager.rb.velocity.x, playerManager.initialBounceForce);
                playerManager.initialBounceForce *= playerManager.bounceDecay;
                playerManager.rb.AddForce(new Vector2(0f, playerManager.initialBounceForce), ForceMode2D.Impulse);
                playerManager.bounceCount--;


                yield return new WaitForSeconds(playerManager.bounceDelay);
                playerManager.animator.SetBool("Daño", false);
                SoundManager.instance.SoundOnPlace(SoundManager.instance.damageClip, new Vector3(0f, 0f, 0f));
            }

            yield return null;
        }

        moveEnviroment.moveSpeed = 0;

        canConsumeEnergy = false;
        UiMediator.instance.GoToUi(UiManager.instance.gameOverUiId);
    }
    #endregion


    public void PauseGame(bool active)
    {
        canConsumeEnergy = !    active;

        if(canConsumeEnergy)
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
        else
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
    

    public void StartGame(bool active)
    {
        Timing.RunCoroutine(StartGameDelay(active));
    }

    private IEnumerator<float> StartGameDelay(bool active)
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return Timing.WaitForSeconds(delayTiming);

        canConsumeEnergy = active;
    }
}
