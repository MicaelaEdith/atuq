using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private Rigidbody2D rb;
    private float moveInput;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogWarning("Animator no asignado en el script PlayerMovement.");
        }
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log("entrada axisraw horizontal: " + moveInput);

        if (animator != null)
        {
            animator.SetInteger("speed", Mathf.Abs((int)moveInput));
        }

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
