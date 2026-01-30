using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum masks
{
    none,
    PlagueMask,
    GoblinMask,
    TimeBoyMask
    


}
public class MaskManager : MonoBehaviour
{
    [SerializeField]
    public masks currentMask = masks.none;

    public Genral_Mask currentMaskScript;
    [SerializeField]
    [Header("put in the same order as the enum masks")]
    public Genral_Mask[] masksScripts = new Genral_Mask[3]{new PlagueMaskScript(),new KnightMaskAbilities(),null};


    [Header("Put here the prefabs of the masks")]
    public GameObject bombPrefab;
    public GameObject smallAttackPrefab;
    public GameObject heavyAttackPrefab;
    public GameObject attackBelowPrefab;
    public GameObject shieldPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Player playerRef = GetComponent<Player>();
        foreach (var mask in masksScripts)
        {
            if (mask != null)
            {
                mask.Initialize(playerRef, this);
            }
        }
        masksScripts[0].OnInitiate(new GameObject[1]{bombPrefab});
        masksScripts[1].OnInitiate(new GameObject[4]{smallAttackPrefab, heavyAttackPrefab, attackBelowPrefab, shieldPrefab});

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipMask(masks.none);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipMask(masks.PlagueMask);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipMask(masks.GoblinMask);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipMask(masks.TimeBoyMask);
        }
        if(currentMask != masks.none)
        {
            currentMaskScript.passiveUpdate();
        }

        if(Input.GetKeyDown(KeyCode.E)|| Input.GetKeyDown(KeyCode.Mouse1))
        {
            ability1();
        }
        if(Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.Mouse0))
        {
            ability2();
        }
        
        foreach (var mask in masksScripts)
        {
            if (mask != null)
            {
                mask.GlobalUpdate();
            }
        }
    }
    public void EquipMask(masks maskToEquip)
    {
        if(currentMask != masks.none)
        {
            currentMaskScript.onUnequip();
        }
        currentMask = maskToEquip;
        if(currentMask != masks.none)
        {
            currentMaskScript = masksScripts[(int)currentMask - 1];
            currentMaskScript.onEquip();
        }
    }

    public void UnequipCurrentMask()
    {
        EquipMask(masks.none);
    }

    public void ability1()
    {
        if(currentMask != masks.none)
        {
            currentMaskScript.ability1();
        }
    }
    public void ability2()
    {
        if(currentMask != masks.none)
        {
            currentMaskScript.ability2();
        }
    }
    public void TryDoubleJump()
    {
        if(currentMask != masks.none)
        {
            currentMaskScript.TryDoubleJump();
        }
    }
}
