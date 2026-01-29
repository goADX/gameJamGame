using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Genral_Mask : MonoBehaviour
{
    object mask;
    
    public abstract void ability1();
    public abstract void ability2();
    public abstract void TryDoubleJump();
}
public enum masks
{
    defult,
    PlagueMask,
    GoblinMask,
    TimeBoyMask
    


}
