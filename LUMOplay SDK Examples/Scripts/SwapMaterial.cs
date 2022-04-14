using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LUMOplay/_Examples/Swap Material")]
[RequireComponent(typeof(MeshRenderer))]
public class SwapMaterial : MonoBehaviour
{
    [Tooltip("How long to show the active material (reset by additional movement)")]
    public float hideTime = .5f;
    [Tooltip("Material when there isn't movement")]
    public Material normalMaterial;
    [Tooltip("Material when there is movement")]
    public Material activeMaterial;

    float timer = 0f;

    MeshRenderer meshRenderer;

    // public method that can be called by OnMotionXX callbacks
    public void Swap()
    {
        timer = hideTime;

        // show active material
        meshRenderer.material = activeMaterial;
    }

    private void Update()
    {
        // set MeshRenderer reference
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();

        // checks timer and shows normal material if time runs out
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            meshRenderer.material = normalMaterial;
        }
    }
}
