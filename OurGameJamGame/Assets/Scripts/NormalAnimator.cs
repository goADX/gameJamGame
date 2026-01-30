using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 10.0f;
    private SpriteRenderer spriteRenderer;
    public bool loop = true;
    public bool Played = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!loop && Played) return;
        if(frames.Length == 0) return;
        int index = (int)(Time.time * framesPerSecond) % frames.Length;
        spriteRenderer.sprite = frames[index];
        if(!loop && index == frames.Length - 1) Played = true;
    }
}
