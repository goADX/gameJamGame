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
    public LayerMask HurtMeMask;
    public LayerMask EnemyMask;
    public LayerMask EnemyAttacksMask;

    public GameObject[] PlagueEquipments;
    public GameObject[] KnightEquipments;
    public GameObject[] TimeBoyEquipments;



    [SerializeField]
    public masks currentMask = masks.none;

    public Genral_Mask currentMaskScript;
    [SerializeField]
    [Header("put in the same order as the enum masks")]
    public Genral_Mask[] masksScripts = new Genral_Mask[3]{new PlagueMaskScript(),new KnightMaskAbilities(),new TimeBoyMaskScript()};


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
            //disable its equipments in a for lopp
            if(currentMask == masks.PlagueMask)
            {
                foreach (var item in PlagueEquipments)
                {
                    item.SetActive(false);
                }
            }
            else if(currentMask == masks.GoblinMask)
            {
                foreach (var item in KnightEquipments)
                {
                    item.SetActive(false);
                }
            }
            else if(currentMask == masks.TimeBoyMask)
            {
                foreach (var item in TimeBoyEquipments)
                {
                    item.SetActive(false);
                }
            }
            
        }
        currentMask = maskToEquip;
        if(currentMask != masks.none)
        {
            
            currentMaskScript = masksScripts[(int)currentMask - 1];
            currentMaskScript.onEquip();

            //enable its equipments in a for loop where you go through the array and enable each one in the game editor

            if(maskToEquip == masks.PlagueMask)
            {
                foreach (var item in PlagueEquipments)
                {
                    item.SetActive(true);
                }
            }
            else if(maskToEquip == masks.GoblinMask)
            {
                foreach (var item in KnightEquipments)
                {
                    item.SetActive(true);
                }
            }
            else if(maskToEquip == masks.TimeBoyMask)
            {
                foreach (var item in TimeBoyEquipments)
                {
                    item.SetActive(true);
                }
            }
            
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
