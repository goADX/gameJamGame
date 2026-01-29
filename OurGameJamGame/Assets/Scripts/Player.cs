using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Velocity velocity;
    public float jumpForce;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if(Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
            {
                rb.velocity = new Vector2(-velocity.speed, rb.velocity.y);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                rb.velocity = new Vector2(velocity.speed, rb.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

}
