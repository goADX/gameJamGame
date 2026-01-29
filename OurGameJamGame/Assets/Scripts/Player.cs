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
    public float SpeedToStop = 0.6f;
    public float jumpCooldown = 0.3f;
    private float lastJumpTime = 0f;
    public MaskManager maskManager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maskManager = GetComponent<MaskManager>();
        foreach (var mask in maskManager.masksScripts)
        {
            if (mask != null)
            {
                mask.Initialize(this, maskManager);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        
        if(Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.55f, 0), 0.1f, groundLayer)&& velocity.y <= 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            //apply gravity
            velocity += new Vector3(Physics2D.gravity.x, Physics2D.gravity.y, 0) * Time.deltaTime;
            
        }

        maskManager.currentMaskScript.passiveUpdate();

        if(Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }
        
        
        if (isGrounded)
        {   
            if(velocity.y < 0)
            {
                velocity = new Vector3(velocity.x, 0, 0);
            }

            float moveInput = Input.GetAxis("Horizontal");
            //print(moveInput);
            if(moveInput != 0)
            {
                if(velocity.magnitude < MinSpeedToInstantatnious|| Mathf.Sign(moveInput) != Mathf.Sign(velocity.x))
                {
                    //print("hey");
                    velocity = new Vector3(moveInput * MinSpeedToInstantatnious*1.1f, velocity.y , 0);   
                }
                else
                {
                    velocity += new Vector3(moveInput * speed*Time.deltaTime, 0 , 0);     
                }
            }
            else
            {
                if (Mathf.Abs(velocity.x) <= SpeedToStop)
                {
                    velocity.x = 0;
                }

            }
            
                 
            
            
            

            
        }

        lastJumpTime += Time.deltaTime;

        //apply velocity
        transform.position +=  velocity * Time.deltaTime;
        //apply drag
        if(isGrounded)
        velocity -= 1f * velocity * Mathf.Min(Time.deltaTime*1.2f, 1f);
    }

    private void Jump()
    {
        if(lastJumpTime >= jumpCooldown){
            if(isGrounded)
            {
                velocity += new Vector3(0f, jumpForce,0f);
                isGrounded = false;
                lastJumpTime = 0f;
            }
            else
            {
                maskManager.currentMaskScript.TryDoubleJump();
            }
        }
    }

}
