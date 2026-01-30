using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMaskAbilities : Genral_Mask
{
    public GameObject SmallAttackPrefab;
    public GameObject HeavyAttackPrefab;
    public GameObject AttackBelowPrefab;
    public GameObject ShieldPrefab;

    public float DashDownForce = 20f;

    [Header("Ability Cooldowns")]
    public float SmallAttackCooldown = 1f;
    public float HeavyAttackCooldown = 3f;
    public float ShieldBashCooldown = 5f;

    [Header("Ability Trackers")]
    public float SmallAttackLastUsed = 1f;
    public float HeavyAttackLastUsed = 3f;
    public float ShieldBashLastUsed = 5f;

    public GameObject currentAttackBelowInstance;
    public override void OnInitiate(GameObject[] preferbs)
    {
        SmallAttackPrefab = preferbs[0];
        HeavyAttackPrefab = preferbs[1];
        AttackBelowPrefab = preferbs[2];
        ShieldPrefab = preferbs[3];
    }

    public override void ability1()
    {
        if(SmallAttackLastUsed >= SmallAttackCooldown)
        {
            GameObject.Instantiate(SmallAttackPrefab, player.transform.position + new Vector3((player.IsFacingRight ? 1f : -1f) * 1f, 0f, 0f), Quaternion.identity);
            SmallAttackLastUsed = 0f;
        }
    }

    public override void ability2()
    {
        if(HeavyAttackLastUsed >= HeavyAttackCooldown)
        {
            GameObject.Instantiate(HeavyAttackPrefab, player.transform.position + new Vector3((player.IsFacingRight ? 1f : -1f) * 1f, 0f, 0f), Quaternion.identity);
            HeavyAttackLastUsed = 0f;
        }
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
        if (player.isGrounded&&currentAttackBelowInstance != null)
        {
            currentAttackBelowInstance.GetComponent<SelfDestruct>().DestroySelf();
            currentAttackBelowInstance = null;
            player.velocity = new Vector3(0f, 0f, 0f);
        }
    }
    public override void TryDoubleJump()
    {
        if(currentAttackBelowInstance != null)
        {
            return;
        }
        player.velocity = new Vector3(player.velocity.x, -DashDownForce, 0f);
        
        currentAttackBelowInstance = GameObject.Instantiate(AttackBelowPrefab, player.transform.position + new Vector3(0f, 0, 0f), Quaternion.identity);
    }
}
