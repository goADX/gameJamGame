using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeBoyMaskScript : Genral_Mask
{

    public GameObject ClonePrefab;
    public GameObject CurrentClone;
    public GameObject TimeExplosionPrefab;
    

    public float PhaseDashRange = 10f;
    public float PhaseDashDamage = 100f;
    public int CloneNumberOfSaves = 30;

    [Header("CoolDowns")]
    public float CloneRewindCooldown = 5f;
    public float PhaseDashCooldown = 3f;
    [Header("Trackers")]
    public float lastCloneRewindTime = 0f;
    public float lastPhaseDashTime = 3f;
    public GameObject ClosestEnemy;
    public bool isInPhaseDashMode = false;
    public List<Vector3> clonesPositions = new List<Vector3>();
    

    public override void OnInitiate(GameObject[] preferbs)
    {
        
    }
    public override void ability1()
    {
    // 1. Check Cooldown
    if (lastPhaseDashTime >= PhaseDashCooldown)
    {
        // 2. Find the closest enemy within range
        GameObject closestEnemy = Physics2D.OverlapCircleAll(player.transform.position, PhaseDashRange, maskManager.EnemyMask)
            .OrderBy(t => Vector3.Distance(t.transform.position, player.transform.position))
            .FirstOrDefault()?.gameObject;

        // 3. If an enemy is found, Dash and Attack
        if (closestEnemy != null)
        {
            // Teleport to enemy position
            player.transform.position = closestEnemy.transform.position;

            // Deal Damage
            closestEnemy.GetComponent<EnemyScript>().ReciveDamage(PhaseDashDamage);

            // Reset Cooldown
            lastPhaseDashTime = 0f;
            
            // Optional: If you use this bool for animations, set it here
            isInPhaseDashMode = true; 
        }
    }
    }
    
    public override void ability2()
    {
        player.transform.position = CurrentClone.transform.position;
        GameObject.Instantiate(TimeExplosionPrefab, CurrentClone.transform.position, Quaternion.identity);
        lastCloneRewindTime = 0f;
    
    
    }
    public override void onEquip()
    {
        lastCloneRewindTime = 0;
    }
    public override void onUnequip()
    {
        
    }
    public override void passiveUpdate()
    {
        lastCloneRewindTime += Time.deltaTime;
        CurrentClone.transform.position = clonesPositions[clonesPositions.Count - 1];
    }
    public override void GlobalUpdate()
    {
        lastPhaseDashTime += Time.deltaTime;
        clonesPositions.Insert(0, player.transform.position);
        if(clonesPositions.Count > CloneNumberOfSaves){
            clonesPositions.RemoveAt(clonesPositions.Count - 1);
        }

    }
    public override void TryDoubleJump()
    {
        
    }


}
