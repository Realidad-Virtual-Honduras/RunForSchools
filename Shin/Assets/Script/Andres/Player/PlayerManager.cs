using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    [Header("Animations")]
    public Animator animator;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float secondJumpMultiplier = 0.5f; //Multiplicador para la fueza del segundo salto.
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckerRadius = 0.2f;
    private Collider2D _collider;
    private bool isGround;
    private int jumpCount;
    private const int maxJumps = 1;

    [Header("Dead FX")]
    public int bounceCount = 3;
    public int maxBounceCount = 3;
    public float initialBounceForce = 5f;
    public float bounceDecay = 0.5f;
    public float bounceDelay = 0.5f;
   

    [HideInInspector] public Rigidbody2D rb;
    public PlayerControl playerControlHardware;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        playerControlHardware = new PlayerControl();

        bounceCount = maxBounceCount;
    }

    public bool IsGrounded()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckerRadius, groundLayer);
        animator.SetBool("IsGround", isGround);
        return isGround;
    }

    private void Update()
    {
        IsGrounded();

        animator.SetFloat("BajoNivel", LevelManager.instance.energyTotal);

        if(IsGrounded())
        {           
            jumpCount = 0;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (LevelManager.instance.canConsumeEnergy)
        {            
            if (jumpCount < maxJumps)
            {
                float currentJumpForce = (jumpCount == 1) ? jumpForce * secondJumpMultiplier : jumpForce;
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Para resetear la velocidad vertical.
                rb.AddForce(new Vector2(0f, currentJumpForce), ForceMode2D.Impulse);
                SoundManager.instance.SoundOnPlace(SoundManager.instance.jumpClip, new Vector3(0f, 0f, 0f));
                animator.SetTrigger("Salto");
                jumpCount++;
            }
        }
    }

    public void Dead()
    {        
        gameObject.SetActive(false);
        LevelManager.instance.GameOver();
        Destroy(gameObject);
    }

    public void FinishDamage()
    {
        animator.SetBool("Daño", false);
    }

    private void OnEnable()
    {
        playerControlHardware.Actions.Enable();

        playerControlHardware.Actions.Jump.performed += Jump; 
    }

    private void OnDisable()
    {
        playerControlHardware.Actions.Disable();

        playerControlHardware.Actions.Jump.canceled -= Jump;
    }
}
