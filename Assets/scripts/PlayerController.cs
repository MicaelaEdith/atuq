
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private bool doubleJump;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform frontCheck;
    [SerializeField]
    private Transform backCheck;

    [SerializeField]
    private LayerMask layerGround;

    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;
    private bool moveInputOk = true;
    private bool wall = false;


    [SerializeField]
    private float fallSpeed = 5f;

    void Update()
    {
        wall = WallCheck.Instance.isTouchingWall; 
        if (moveInputOk) moveInput = Input.GetAxisRaw("Horizontal");
        else moveInput = 0;

        if (wall)
        {
            if (facingRight && moveInput > 0) moveInput = 0;
            else if (facingRight && moveInput < 0) moveInput = -2;
            if (!facingRight && moveInput < 0) moveInput = 0;
            else if (!facingRight && moveInput > 0) moveInput = 2;

        }
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, layerGround);

        if (isGrounded)
        {
            moveInputOk = true;
        }
        else
        {

        }

        animator.SetInteger("speed", Mathf.Abs((int)moveInput));
        animator.SetBool("jumping", !isGrounded);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = false;
            }
        }

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        CheckPlatformBounds();
    }

    private void FixedUpdate()
    {
        wall = WallCheck.Instance.isTouchingWall;
        if (!isGrounded && rb.velocity.y == 0)
        {
            moveInputOk = false;
            HandleFall();
        }

        if (moveInputOk) moveInput = Input.GetAxisRaw("Horizontal");
        else moveInput = 0;
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void CheckPlatformBounds()
    {
        bool isFrontGrounded = Physics2D.OverlapCircle(frontCheck.position, .03f, layerGround);
        bool isBackGrounded = Physics2D.OverlapCircle(backCheck.position, .03f, layerGround);
        if (isGrounded || rb.velocity.x < 0.1f)
        {
            if (facingRight)
            {
                if (isFrontGrounded != isBackGrounded)
                {
                    transform.position += new Vector3(.6f, 0);
                }
            }
            else
            {
                if (isFrontGrounded != isBackGrounded)
                {
                    transform.position -= new Vector3(.6f, 0);
                }
            }
        }
    }

    void HandleFall()
    {
        rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
    }
}