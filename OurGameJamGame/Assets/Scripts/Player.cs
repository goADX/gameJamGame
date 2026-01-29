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
        if(Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.55f, 0), 0.1f, groundLayer)&& velocity.y <= 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        if (isGrounded)
        {   
            float moveInput = Input.GetAxis("Horizontal");
            //print(moveInput);
            if(moveInput != 0)
            {
                if(velocity.magnitude < MinSpeedToInstantatnious|| Mathf.Sign(moveInput) != Mathf.Sign(velocity.x))
                {
                    print("hey");
                    velocity = new Vector3(moveInput * speed, velocity.y , 0);   
                }
                else
                {
                    velocity += new Vector3(moveInput * speed*Time.deltaTime, 0 , 0);     
                }
            }
            else
            {
                if (Mathf.Abs(velocity.x) < SpeedToStop)
                {
                    velocity = new Vector3(0, velocity.y, 0);
                }

            }
            
                 
            
            
            

            
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            velocity += new Vector3(0f, jumpForce,0f);
            isGrounded = false;
        }
    }

}
