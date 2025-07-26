using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPlayer : MonoBehaviour
{
    public float move;
    public float speed;
    public Animator ani;
    public Rigidbody2D rb;
    public bool Isfacing = true;
    public Vector3 theScale;
    public float jumpForce = 5f;
    private bool isDead = false;
    public Transform firePoint;
    private int jumpCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {

        move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);


        if (move > 0 && !Isfacing)
        {
            Flip();
        }
        else if (move < 0 && Isfacing)
        {
            Flip();
        }

        ani.SetFloat("move", Mathf.Abs(move));


        if (Input.GetMouseButtonDown(0))
        {
            ani.SetTrigger("Attack");
        }


        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;


            if (jumpCount == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                ani.SetTrigger("Jump");
            }

            else if (jumpCount == 2)
            {
                rb.velocity = new Vector2(transform.localScale.x * 5f, jumpForce);
                ani.SetTrigger("FlyKick");
            }
        }


        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            ani.SetBool("Crouch", true);
        }
        else
        {
            ani.SetBool("Crouch", false);
        }

        if (Input.GetKey(KeyCode.J))
            {
            ani.SetTrigger("Strike");
        }
    }

    private void Flip()
    {
        Isfacing = !Isfacing;
        theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    void Die()
    {
        if (isDead) return;

        isDead = true;
        ani.SetTrigger("Die");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        Invoke("Disappear", 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0; // Reset l?i khi ch?m ??t
        }
        if (collision.gameObject.CompareTag("Potion"))
        {
            Hurt();
        }
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }
    void Hurt()
    {
        ani.SetTrigger("Hurt");
        // Tùy ch?n: ??y lùi nhân v?t khi b? th??ng
        float knockback = 3f;
        Vector2 direction = Isfacing ? Vector2.left : Vector2.right;
        rb.velocity = new Vector2(direction.x * knockback, rb.velocity.y);
    }
    void Strike()
    {
        ani.SetTrigger("Strike");

        // Ki?m tra va ch?m v?i enemy
        Collider2D[] hits = Physics2D.OverlapCircleAll(firePoint.position, 0.5f, LayerMask.GetMask("Enemy"));
        
       
    }
}
