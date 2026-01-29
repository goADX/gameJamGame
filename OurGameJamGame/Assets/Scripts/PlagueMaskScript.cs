using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueMaskScript : Genral_Mask
{
    public float doubleJumpForce = 8f;
    public GameObject bombPrefab;
    [Header("CoolDowns")]
    public float doubleJumpCooldown = 1f;
    [Header("Trackers")]
    public float lastDoubleJumpTime = 1f;

    public override void OnInitiate()
    {
        
    }

    public override void ability1()
    {
        if(player.IsFacingRight)
            throwBomb(new Vector3(1f,0f,0f));
        else
            throwBomb(new Vector3(-1f,0f,0f));
        
        if (!player.isGrounded)
        {
            if(player.velocity.y < 0f)
            {
                player.velocity = new Vector3(player.velocity.x, 0.1f, 0f);
            }
        }
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
        lastDoubleJumpTime += Time.deltaTime;
    }

    public override void TryDoubleJump()
    {
        player.velocity += new Vector3(0f, player.jumpForce,0f);
        if(player.velocity.x == 0f)
        {
            throwBomb(new Vector3(0f,-1f,0f));
        }else
        if(player.velocity.x < 0f)
        {
            throwBomb(new Vector3(-0.5f,-1f,0f));
        }else
        {
            throwBomb(new Vector3(0.5f,-1f,0f));
        }
        
    }
    public void throwBomb(Vector3 direction)
    {
        
    }
}
