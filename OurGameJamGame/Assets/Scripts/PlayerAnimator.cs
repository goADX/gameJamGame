using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Sprite[] frames;
    public float framesPerSecondIdle = 4.0f;
    public float framesPerSecondRun = 10.0f;
    public float framesPerSecondAir = 12.0f;
    private SpriteRenderer spriteRenderer;
    private MaskManager maskManager;
    public masks maskType
    {
        get{ return maskManager.currentMask; }
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        maskManager = GetComponent<MaskManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(frames.Length == 0) return;
        

    }
}
