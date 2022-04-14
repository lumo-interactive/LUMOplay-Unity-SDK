using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("LUMOplay/_Examples/Swap Image Color")]
[RequireComponent(typeof(Image))]
public class SwapImageColor : MonoBehaviour
{
    [Tooltip("How long to show the active color (reset by additional movement)")]
    public float hideTime = .5f;
    [Tooltip("Color when there isn't movement")]
    public Color normalColor;
    [Tooltip("Color when there is movement")]
    public Color activeColor;

    float timer = 0f;

    Image img;

    // public method that can be called by OnMotionXX callbacks
    public void Swap()
    {
        timer = hideTime;

        // show active color
        img.color = activeColor;
    }

    private void Update()
    {
        // set Image reference
        if (img == null) img = GetComponent<Image>();

        // checks timer and shows normal color if time runs out
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            img.color = normalColor;
        }
    }
}
