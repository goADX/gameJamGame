using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 velocity;
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    public bool isGrounded = false;
    public float MinSpeedToInstantatnious = 2f;
    public float SpeedToStop = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }
        if(Physics2D.OverlapCircle(transform.position + new Vector3(0, -1, 0), 0.2f, groundLayer)&& rb.velocity.y <= 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        if (isGrounded)
        {   
            float SideSpeed = Mathf.Abs(rb.velocity.x);
            if(SideSpeed < MinSpeedToInstantatnious)
            {
                if(Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
                {
                    rb.velocity = new Vector2(-speed*01f, rb.velocity.y);
                }else
                if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rb.velocity = new Vector2(speed*01f, rb.velocity.y);
                }else if(SideSpeed <= SpeedToStop)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
            else
            {
                
            
                if(Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
                {
                    rb.velocity += new Vector2(-speed*Time.deltaTime, rb.velocity.y);
                }else
                if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rb.velocity += new Vector2(speed*Time.deltaTime, rb.velocity.y);

                }else if(SideSpeed <= SpeedToStop)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

}
