using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightBigAbility : MonoBehaviour
{
    public LayerMask TargetablesMask;
    public float FirstStageTime = 1f;
    public GameObject FirstStageVFX;
    public float SecondStageTime = 1f;
    public GameObject SecondStageVFX;

    public GameObject DamageVFX;
    public bool facingRight = true;
    public float Range = 4f;
    public GameObject[] EnemiesTargeted;
    public float Timer = 0f;
    public int CurrentStage = 0; //0 not started 1 first stage 2 second stage 3 ended
    Collider2D[] EnemiesTargetedColliders;

    /*
    we will first make big rectange, detect all enemies in the rectangle
    at the same time we summon the first VFX 
    and we will stay for the entire diration of the first stage without being able to move or walk
    second stage summon a particale on each enemy detected and wait for the end of the second stage to end
    then we deal damage to all enemies remove all vfx teleport to the furthest enemy 
    */
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentStage == 0)
        {
            EnemiesTargetedColliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(facingRight?Range:-Range,0,0), new Vector3(Range,2f,1f), 0f, TargetablesMask);
            Instantiate(FirstStageVFX, transform.position, Quaternion.identity);
            CurrentStage = 1;
            EnemiesTargeted = new GameObject[EnemiesTargetedColliders.Length];
        }else if(CurrentStage == 1&& Timer >= FirstStageTime)
        {
            //summon VFX on each enemy
            int i = 0;
            foreach(Collider2D enemyCollider in EnemiesTargetedColliders)
            {
                EnemiesTargeted[i] = enemyCollider.gameObject;
                Instantiate(SecondStageVFX, enemyCollider.transform.position, Quaternion.identity);
                i++;
            }
            CurrentStage = 2;
            Timer = 0f;
        
            //summon VFX ontop of each enemy


        }else if(CurrentStage == 2&& Timer >= SecondStageTime)
        {
            
            Vector3 FurthestEnemyPos = transform.position;
            float FurthestDistance = 0f;
            foreach(Collider2D enemyCollider in EnemiesTargetedColliders)
            {
                
                //deal damage
                EnemyScript enemyScript = enemyCollider.GetComponent<EnemyScript>();
                if(enemyScript != null)
                {
                    enemyScript.health -= 100f; //deal 100 damage
                }
                //find furthest enemy
                float distance = Vector3.Distance(transform.position, enemyCollider.transform.position);
                if(distance > FurthestDistance)
                {
                    FurthestDistance = distance;
                    FurthestEnemyPos = enemyCollider.transform.position;
                }
            }
            //teleport to furthest enemy
            transform.position = FurthestEnemyPos;
            CurrentStage = 3;

        }
        if(CurrentStage == 3)
        {
            Destroy(this.gameObject);
        }








        Timer += Time.deltaTime;   
    }
}
