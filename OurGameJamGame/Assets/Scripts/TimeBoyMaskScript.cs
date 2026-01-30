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

    [Header("CoolDowns")]
    public float CloneRewindCooldown = 5f;
    public float PhaseDashCooldown = 3f;
    [Header("Trackers")]
    public float lastCloneRewindTime = 0f;
    public float lastPhaseDashTime = 3f;
    public GameObject ClosestEnemy;
    public bool isInPhaseDashMode = false;

    public override void OnInitiate(GameObject[] preferbs)
    {
        
    }
    public override void ability1()
    {
        if (!isInPhaseDashMode)
        {
            if (lastPhaseDashTime >= PhaseDashCooldown)
            {
                
                isInPhaseDashMode = true;
                lastPhaseDashTime = 0f;
            }


        }
        else
        {
            ClosestEnemy = Physics2D.OverlapCircleAll(player.transform.position, PhaseDashRange, maskManager.EnemyMask)
                .OrderBy(t => Vector3.Distance(t.transform.position, player.transform.position))
                .FirstOrDefault()?.gameObject;

            if (ClosestEnemy != null)
            {
                player.transform.position = ClosestEnemy.transform.position;
                ClosestEnemy.GetComponent<EnemyScript>().ReciveDamage(PhaseDashDamage);
            }
        }
    }
    public override void ability2()
    {
        player.transform.position = CurrentClone.transform.position;
        GameObject.Instantiate(TimeExplosionPrefab, CurrentClone.transform.position, Quaternion.identity);
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
    }
    public override void GlobalUpdate()
    {
        lastPhaseDashTime += Time.deltaTime;
    }
    public override void TryDoubleJump()
    {
        
    }


}
