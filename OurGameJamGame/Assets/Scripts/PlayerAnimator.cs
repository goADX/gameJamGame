using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Material material;
    //idle is from 0 to 3
    //walking from 4 to 7
    //jumping is 8
    //falling from 9 to 12
    public float framesPerSecondIdle = 4.0f;
    public float framesPerSecondRun = 10.0f;
    public float framesPerSecondAir = 12.0f;
    private SpriteRenderer spriteRenderer;
    private MaskManager maskManager;
    int currentFrameIndex = 0;
    public float frameTimer = 0f;
    public masks maskType
    {
        get{ return maskManager.currentMask; }
    }

    public bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        maskManager = GetComponent<MaskManager>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (maskType)
        {
            case masks.none:
                material.SetFloat("_Mask", 0);
                break;
            case masks.PlagueMask:
                material.SetFloat("_Mask", 1);
                break;
            case masks.GoblinMask:
                material.SetFloat("_Mask", 2);
                break;
            case masks.TimeBoyMask:
                material.SetFloat("_Mask", 3);
                //AnimateTimeBoy();
                break;
            default:
                material.SetFloat("_Mask", 0);
                break;
        }
        
        Player player = GetComponent<Player>();
        facingRight = player.IsFacingRight;

        if (!facingRight)
        {
            material.SetFloat("_Flip", 1);
        }
        else
        {
            material.SetFloat("_Flip", 0);
        }

        if (!player.isGrounded)
        {
            if(frameTimer >= 1f / framesPerSecondAir)
            {
                currentFrameIndex++;
                if (currentFrameIndex > 12)
                {
                    currentFrameIndex = 9;
                }
                SetMaterialIndex(currentFrameIndex);
                frameTimer = 0f;
            }
        }else if (Mathf.Abs(player.velocity.x) > 0.1f)
        {
            if(frameTimer >= 1f / framesPerSecondRun)
            {
                currentFrameIndex++;
                if (currentFrameIndex > 7)
                {
                    currentFrameIndex = 4;
                }
                SetMaterialIndex(currentFrameIndex);
                frameTimer = 0f;
            }
        }
        else
        {
            if(frameTimer >= 1f / framesPerSecondIdle)
            {
                currentFrameIndex++;
                if (currentFrameIndex > 3)
                {
                    currentFrameIndex = 0;
                }
                SetMaterialIndex(currentFrameIndex);
                frameTimer = 0f;
            }
        }


        frameTimer += Time.deltaTime;
    }



    public void SetMaterialIndex(int index)
    {
        material.SetFloat("_State", index);
    }
}
