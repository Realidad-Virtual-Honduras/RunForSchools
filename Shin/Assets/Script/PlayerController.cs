using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float secondJumpMultiplier = 0.5f; //Multiplicador para la fueza del segundo salto.
    private int maxJumps = 2;
    private int jumpCount;
    private Rigidbody2D rb;

    public GameOverManager gameOverManager;
    private Vector3 startPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Guarda la posicion inicial al jugar.
        gameOverManager = FindObjectOfType<GameOverManager>();
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

    }

    private void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector2 (rb.velocity.x, 0f); // Para resetear la velocidad vertical.

            float currentJumpForce = (jumpCount == 1) ? jumpForce * secondJumpMultiplier : jumpForce;
            rb.AddForce(new Vector2(0f, currentJumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    public void Dead()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        float finalDistance = Vector3.Distance(startPosition, transform.position);
        gameOverManager.ShowGameOverScreen(finalDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Dead();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isGrounded = false;
        }
    }

    public Rigidbody2D GetRigidbody2D() {  return rb; }

   

}
