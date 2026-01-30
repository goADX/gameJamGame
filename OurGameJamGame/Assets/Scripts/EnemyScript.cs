using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum EnemyMovementType
    {
        Stationary,
        PatrolRightToLeft,
        ChasePlayer,
        ShootWhenInRange
    }
    public EnemyMovementType enemyMovementType;
    public LayerMask PlayerLayer;
    public LayerMask PlayerAttacks;
    public LayerMask GroundMask;
    [Header("Stats")]
    public float health = 1f;
    public float Damage = 10f;
    [Header("Special Stats")]
    public float moveSpeed = 2f;
    public bool currentlyFacingRight = false;
    public bool canFly = false;
    public float gravity = 9.8f;
    [Header("Trackers")]
    public bool IsGrounded = false;
    public bool IsWallGrounded = false;
    public Vector3 velocity;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFly)
        {
            
            if(Physics2D.OverlapCircle(transform.position + new Vector3(0f, -0.5f, 0), 0f, GroundMask))
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
            if(!IsGrounded)
            {
                velocity.y -= gravity * Time.deltaTime;
                
            }
            else if (velocity.y < 0)
            {
            velocity.y = 0;
            }

            Vector3 wallCheckPos = currentlyFacingRight ? new Vector3(0.5f, 0, 0) : new Vector3(-0.5f, 0, 0);

            if (Physics2D.OverlapCircle(transform.position + wallCheckPos, 0.1f, GroundMask))
            {
                velocity.x = 0;
            }
        }
        switch(enemyMovementType)
        {
            case EnemyMovementType.Stationary:
                //do nothing
                break;
            case EnemyMovementType.PatrolRightToLeft:
                //patrol code here
                if(currentlyFacingRight)
                {
                    if(Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0, 0), 0.1f, GroundMask) )
                    {
                     currentlyFacingRight = false;   

                    }
                }
                else
                {
                    if(Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, 0, 0), 0.1f, GroundMask) )
                    {
                     currentlyFacingRight = true;   
                    }
                    
                }

                if (currentlyFacingRight)
                {
                    velocity += Vector3.right * moveSpeed * Time.deltaTime;

                }else
                {
                    velocity -= Vector3.right * moveSpeed * Time.deltaTime;
                    
                }
                break;
            case EnemyMovementType.ChasePlayer:
                //chase player code here
                GameObject FoundPlayer = Physics2D.OverlapCircle(transform.position, 10f, PlayerLayer).gameObject;
                if(FoundPlayer != null)
                {

                currentlyFacingRight =  FoundPlayer.transform.position.x > transform.position.x;
                    
                if (currentlyFacingRight)
                {
                    velocity += Vector3.right * moveSpeed * Time.deltaTime;

                }else
                {
                    velocity -= Vector3.right * moveSpeed * Time.deltaTime;                        
                }

                }


                break;
        }
        spriteRenderer.flipX = !currentlyFacingRight;
        transform.position += velocity * Time.deltaTime;
        velocity += velocity * -0.1f * Time.deltaTime;
        if(health <= 0f)
        {
           Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == PlayerAttacks)
        {
            //take damage
            Destroy(other.gameObject);
            ReciveDamage(other.GetComponent<PlayerAttacksScript>().Damage);
            if(health <= 0f)
            {
               Die();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(( other.gameObject.layer & (1 << PlayerLayer)) != 0)
        {
            //take damage
            print("Damge taken");
            other.gameObject.GetComponent<Player>().ReciveDamage(Damage);
        }
        
    }

    public void ReciveDamage(float DamageGot)
    {
        health -= DamageGot;
        if(health <= 0f)
        {
           Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);

    }
}
