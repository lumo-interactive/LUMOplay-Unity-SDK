using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LUMOplay/_Examples/Swap Sprite")]
[RequireComponent(typeof(SpriteRenderer))]
public class SwapSprite : MonoBehaviour
{
    [Tooltip("How long to show the active sprite (reset by additional movement)")]
    public float hideTime = .5f;
    [Tooltip("Sprite when there isn't movement")]
    public Sprite normalSprite;
    [Tooltip("Sprite when there is movement")]
    public Sprite activeSprite;

    float timer = 0f;

    SpriteRenderer spRenderer;

    // public method that can be called by OnMotionXX callbacks
    public void Swap()
    {
        timer = hideTime;

        // show active sprite
        spRenderer.sprite = activeSprite;
    }

    private void Update()
    {
        // set SpriteRenderer reference
        if (spRenderer == null) spRenderer = GetComponent<SpriteRenderer>();

        // checks timer and shows normal sprite if time runs out
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            spRenderer.sprite = normalSprite;
        }
    }
}
