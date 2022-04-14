using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LUMOplay;

[AddComponentMenu("LUMOplay/On Motion 3D")]
[RequireComponent(typeof(Collider))]
public class OnMotion3D : MonoBehaviour
{
    [Header("Motion detection is based on the Collider of this object.")]
    [Tooltip("Point checks the center of the transform against motion, Precise will check many points within the motion events against the collider using Raycasts")]
    public MotionType detectionType = MotionType.Precise;

    [Header("Call public methods in your own scripts (Callback passes a LumoMotionEvent)")]
    public LumoMotionEventUnityEvent onMotionEventCallbacks;

    [Tooltip("How far to check Raycasts, lower values can improve performance, but must still reach the collider")]
    public float raycastDistance = Mathf.Infinity;

    private Collider motionCollider;

    // using FixedUpdate as MotionListener only has new data during FixedUpdate
    void FixedUpdate()
    {
        switch(detectionType)
        {
            case MotionType.Point:
                // checks if the center of this objects transform is within any motion events (fast, but quite inaccurate)
                var lst = MotionListener.CheckPointInMotion(MotionListener.WorldToScreen(transform));
                if (lst.Count > 0)
                {
                    foreach (LumoMotionEvent evt in lst)
                    {
                        // invoke all defined callbacks
                        Invoke(evt);
                    }
                }
                break;

            case MotionType.Precise:
                // set the objects Collider object
                if (motionCollider == null) motionCollider = GetComponent<Collider>();

                // get motion data from MotionListener
                List<LumoMotionEvent> events = MotionListener.CurrentMotion;

                // check all events raycasts if they hit this objects Collider
                foreach (LumoMotionEvent evt in events)
                {
                    foreach(Ray ray in evt.Scatter)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, raycastDistance, 1 << motionCollider.gameObject.layer))
                        {
                            // invoke all defined callbacks
                            if (hit.collider == motionCollider) Invoke(evt);
                        }
                    }
                }
                break;
        }
    }

    void Invoke(LumoMotionEvent evt)
    {
        onMotionEventCallbacks.Invoke(evt);
    }
}

// enum for Motion Detection Type
public enum MotionType
{
    Point,
    Precise
}

// UnityEvent for LumoMotionEvent data
[System.Serializable]
public class LumoMotionEventUnityEvent : UnityEvent<LumoMotionEvent>
{

}