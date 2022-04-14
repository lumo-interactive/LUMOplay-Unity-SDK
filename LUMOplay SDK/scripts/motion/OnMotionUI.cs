using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUMOplay;

[AddComponentMenu("LUMOplay/On Motion UI")]
[RequireComponent(typeof(RectTransform))]
public class OnMotionUI : MonoBehaviour
{
    [Header("Motion detection is based on the RectTransform of this object.")]

    [Header("Call public methods in your own scripts (Callback passes a LumoMotionEvent)")]
    public LumoMotionEventUnityEvent onMotionEventCallbacks;

    private RectTransform rt;

    Vector3[] WorldCorners = new Vector3[4];

    private void Start()
    {
        // set this objects RectTransform
        rt = GetComponent<RectTransform>();
    }

    // using FixedUpdate as MotionListener only has new data during FixedUpdate
    void FixedUpdate()
    {
        // checks if the Rect of the RectTransform overlaps any motion on the screen
        List<LumoMotionEvent> events = MotionListener.CheckRectOverlapsBounds(RectTransformToScreenSpace(rt));
        foreach(LumoMotionEvent evt in events)
        {
            // invoke all defined callbacks
            Invoke(evt);
        }
    }

    // converts a RectTransform to screen(pixel) space
    Rect RectTransformToScreenSpace(RectTransform transform)
    {
        transform.GetWorldCorners(WorldCorners);
        Bounds bounds = new Bounds(WorldCorners[0], Vector3.zero);
        for (int i = 1; i < 4; ++i)
        {
            bounds.Encapsulate(WorldCorners[i]);
        }
        Rect rect = new Rect(bounds.min, bounds.size);
        return rect;
    }

    void Invoke(LumoMotionEvent evt)
    {
        onMotionEventCallbacks.Invoke(evt);
    }
}
