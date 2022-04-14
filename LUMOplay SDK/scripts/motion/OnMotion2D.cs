using System.Collections.Generic;
using UnityEngine;
using LUMOplay;

[RequireComponent(typeof(Collider2D))]
[AddComponentMenu("LUMOplay/On Motion 2D")]
public class OnMotion2D : MonoBehaviour
{
    [Header("Motion detection is based on the Collider2D of this object.")]
    [Header("Call public methods in your own scripts (Callback passes a LumoMotionEvent)")]
    public LumoMotionEventUnityEvent onMotionCallbacks;

    private Collider2D motionCollider;

    // using FixedUpdate as MotionListener only has new data during FixedUpdate
    void FixedUpdate()
    {
        // set the objects Collider2D object
        if (motionCollider == null) motionCollider = GetComponent<Collider2D>();

        // get motion data from MotionListener
        List<LumoMotionEvent> motion = MotionListener.CurrentMotion;

        // check all motion for hits on this objects Collider2D
        foreach (LumoMotionEvent evt in motion)
        {
            foreach (Ray ray in evt.Scatter)
            {
                // invoke all defined callbacks
                if (motionCollider.OverlapPoint(ray.origin)) Invoke(evt);
            }
        }
    }

    void Invoke(LumoMotionEvent evt)
    {
        onMotionCallbacks.Invoke(evt);
    }
}
