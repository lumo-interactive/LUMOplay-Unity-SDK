using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LUMOplay/_Utils/Pulse Scale")]
/**
 * Using a SINE wave, will pulse the localScale of the transform
 */
public class PulseScale : MonoBehaviour
{
    [Tooltip("Scale adjustment over time (min/max)")]
    public float scaleAmount;
    [Tooltip("How fast does this pulse")]
    public float speed;

    float phase; // for SINE wave
    Vector3 baseScale;

    void Start()
    {
        // set the starting scale of the transform
        baseScale = transform.localScale;
    }

    void Update()
    {
        // adjust phase of SINE wave
        phase += 1 / speed * Time.deltaTime;

        // adjust localScale of transform based on SINE wave
        transform.localScale = baseScale + scaleAmount * Vector3.one * Mathf.Sin(phase);
    }
}
