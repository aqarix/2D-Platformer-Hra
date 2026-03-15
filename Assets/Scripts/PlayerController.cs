using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jump;
    bool doubleJump;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator an;
    Vector2 checkpoint = new Vector2(0,7);
    public int speedUpgradeLevel = 0;
    public int jumpUpgradeLevel = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
                doubleJump = false;
            }
        }

        if (horizontal == -1)
        {
            sr.flipX = true;
        }
        else if (horizontal == 1)
        {
            sr.flipX = false;
        }

        if (isGrounded())
        {
            an.SetFloat("speed", Mathf.Abs(horizontal));
        }
        else
        {
            an.SetFloat("speed", 0);
        }

        if (transform.position.y < -30)
        {
            Respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
        {
            checkpoint = transform.position;
        }
        if (collision.CompareTag("trap"))
        {
            Respawn();
        }
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Respawn()
    {
        transform.position = checkpoint;
    }

    public void UpgradeSpeed(float amount)
    {
        speed += 2;
        speedUpgradeLevel++;
    }

    public void UpgradeJump(float amount)
    {
        jump += 4;
        jumpUpgradeLevel++;
    }
}
