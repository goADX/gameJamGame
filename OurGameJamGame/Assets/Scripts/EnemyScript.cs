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
    public LayerMask PlayerAttacks;
    public LayerMask GroundMask;
    [Header("Stats")]
    public float health = 100f;
    public float Damage = 10f;
    [Header("Special Stats")]
    public float moveSpeed = 2f;
    public bool currentlyFacingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyMovementType)
        {
            case EnemyMovementType.Stationary:
                //do nothing
                break;
            case EnemyMovementType.PatrolRightToLeft:
                //patrol code here
                if(currentlyFacingRight)
                {
                    if(Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, -0.5f, 0), 0.1f, GroundMask) )
                    {
                     currentlyFacingRight = false;   
                    }
                }
                else
                {
                    if(Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, -0.5f, 0), 0.1f, GroundMask) )
                    {
                     currentlyFacingRight = true;   
                    }
                    
                }

                if (currentlyFacingRight)
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

                }else
                {
                    transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
                    
                }
                break;
            case EnemyMovementType.ChasePlayer:
                //chase player code here
                break;
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
