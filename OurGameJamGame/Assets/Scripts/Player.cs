using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject cameraObject;
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

    public bool IsFacingRight = true;

    public GameObject MainCamera;
    public float health = 3;
    public bool HeartsSystem = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maskManager = GetComponent<MaskManager>();
        
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

        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
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
            
            if(Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0, 0), 0.4f, groundLayer) && velocity.x > 0)
            {
                velocity.x = Mathf.Min(0f, velocity.x);
            }
            if(Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, 0, 0), 0.4f, groundLayer) && velocity.x < 0)
            {
                velocity.x = Mathf.Max(0f, velocity.x);
            }
            if (transform.position.x >= 18f)
            {
            // LOCK CAMERA: Stay at this specific spot
                MainCamera.transform.position = new Vector3(29.53f, 1f, -10f);
            }
            else
            {
                // FOLLOW PLAYER: Standard movement
                MainCamera.transform.position = new Vector3(3.4f, 1f, -10f);
            }
            
            

            
        }

        lastJumpTime += Time.deltaTime;

        //apply velocity
        transform.position +=  velocity * Time.deltaTime;
        //apply drag
        if(isGrounded)
        velocity -= 1f * velocity * Mathf.Min(Time.deltaTime*1.2f, 1f);


        if(cameraObject != null)
        {
            cameraObject.transform.position = new Vector3(transform.position.x, cameraObject.transform.position.y, cameraObject.transform.position.z);
        }
        if(transform.position.magnitude > 10000f)
        {
            Die();
        }
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
                maskManager.TryDoubleJump();
            }
        }
    }

    public void ReciveDamage(float DamageGot = 1f)
    {
        if(HeartsSystem)
        {
            health -= 1;            
        }else
        {
            health -= DamageGot;

        }
        if(health <= 0f)
        {
           Die();
        }
    }

    public void Die()
    {
        RestartGame();
    }
    public void RestartGame()
    {
        // Get the name of the current scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
