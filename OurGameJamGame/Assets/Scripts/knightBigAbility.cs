using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightBigAbility : MonoBehaviour
{
    public float FirstStageTime = 2f;
    public float SecondStageTime = 2f;

    /*
    we will first make big rectange, detect all enemies in the rectangle
    at the same time we summon the first VFX 
    and we will stay for the entire diration of the first stage without being able to move or walk
    second stage summon a particale on each enemy detected and wait for the end of the second stage to end
    then we deal damage to all enemies remove all vfx teleport to the furthest enemy 
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
