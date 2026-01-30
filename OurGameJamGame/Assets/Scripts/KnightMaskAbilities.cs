using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMaskAbilities : Genral_Mask
{
    public GameObject SmallAttackPrefab;
    public GameObject HeavyAttackPrefab;
    public GameObject AttackBelowPrefab;
    public GameObject ShieldPrefab;

    public float DashDownForce = 10f;

    [Header("Ability Cooldowns")]
    public float SmallAttackCooldown = 1f;
    public float HeavyAttackCooldown = 3f;

    [Header("Ability Trackers")]
    public float SmallAttackLastUsed = 1f;
    public float HeavyAttackLastUsed = 3f;
    public override void OnInitiate(GameObject[] preferbs)
    {
        
    }

    public override void ability1()
    {
        
    }

    public override void ability2()
    {
        
    }

    public override void onEquip()
    {
        
    }

    public override void onUnequip()
    {
        
    }

    public override void passiveUpdate()
    {
        
    }

    public override void GlobalUpdate()
    {
        
    }
    public override void TryDoubleJump()
    {
        player.velocity = new Vector3(player.velocity.x, -DashDownForce, 0f);
    }
}
