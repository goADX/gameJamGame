using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Genral_Mask 
{
    public Player player;
    public MaskManager maskManager;

    public void Initialize(Player playerRef, MaskManager maskManagerRef)
    {
        player = playerRef;
        maskManager = maskManagerRef;
        
    }


    public abstract void OnInitiate(GameObject[] preferbs);
    public abstract void passiveUpdate();
    public abstract void GlobalUpdate();
    public abstract void onEquip();
    public abstract void onUnequip();
    public abstract void ability1();
    public abstract void ability2();
    public abstract void TryDoubleJump();
}
