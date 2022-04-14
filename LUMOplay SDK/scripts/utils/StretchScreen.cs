using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LUMOplay/_Utils/Stretch Screen")]
/**
 * This will stretch all content on the screen rather than adapt
 */
public class StretchScreen : MonoBehaviour
{
    [Tooltip("Native aspect ratio of content")]
    public float aspectRatio = 16f / 9f;
    [Tooltip("Camera to apply stretching to")]
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        cam.aspect = aspectRatio;
    }
}
