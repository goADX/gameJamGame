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
    [Header("Stats")]
    public float health = 100f;
    public float Damage = 10f;
    [Header("Special Stats")]
    public float moveSpeed = 2f;
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
            health -= 20f;
            if(health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
