using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUMOplay;

[AddComponentMenu("LUMOplay/Hide On Movement")]
public class HideOnMovement : MonoBehaviour
{
    [Tooltip("SpriteRenderer to hide")]
    public SpriteRenderer sprite;

    [Tooltip("How long with no motion before showing the hidden SpriteRenderer again")]
    public float timeout = 3f;

    [Tooltip("How many seconds it takes to fade in and out")]
    public float fadeSpeed = 1f;

    float baseAlpha;

    float timer = 0f;

    
    void Start()
    {
        // sample the default alpha
        baseAlpha = sprite.color.a;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        var clr = sprite.color; // get current color of sprite

        // if timer is < 0f, fade back in
        if (timer < 0f)
        {
            clr.a += 1 / fadeSpeed * Time.deltaTime;
            if (clr.a > baseAlpha) clr.a = baseAlpha;
        }
        else // if timer is > 0f, fade out
        {
            clr.a -= 1 / fadeSpeed * Time.deltaTime;
            if (clr.a < 0f) clr.a = 0f;
        }

        sprite.color = clr; // set color with new alpha value
    }

    // using FixedUpdate as MotionListener only has new data during FixedUpdate
    private void FixedUpdate()
    {
        // if there's motion, set the timer to the timeout
        if (MotionListener.CurrentMotion.Count > 0)
        {
            timer = timeout;
        }
    }
}
