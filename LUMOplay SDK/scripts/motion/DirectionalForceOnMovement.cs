using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUMOplay;

[AddComponentMenu("LUMOplay/Directional Force On Movement")]
public class DirectionalForceOnMovement : MonoBehaviour
{
    [Tooltip("Layer to check for motion on (typically a floor surface)")]
    public LayerMask triggerLayer;

    [Tooltip("Radius from motion hit to check for objects")]
    public float hitRadius = 2f;

    [Tooltip("Multiplier to apply to valid RigidBodies within radius")]
    public float forceMult = 3f;

    [Tooltip("What object tag to be affected by this force (with attached RigidBody and Collider)")]
    public string affectedTag;

    [Tooltip("How far to check Raycasts, lower values can improve performance, but must still reach the collider")]
    public float raycastDistance = Mathf.Infinity;

    // using FixedUpdate as MotionListener only has new data during FixedUpdate
    void FixedUpdate()
    {
        // get motion data from MotionListener
        List<LumoMotionEvent> events = MotionListener.CurrentMotion;

        foreach (LumoMotionEvent evt in events)
        {
            // use the centroid
            Ray ray = evt.Centroid;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance, triggerLayer))
            {
                // get list of colliders within radius of hit
                Collider[] colliders = Physics.OverlapSphere(hit.point, hitRadius);

                foreach (Collider coll in colliders)
                {
                    // apply force vector from LumoMotionEvent to rigidbodies with matching tag
                    if (coll.tag == affectedTag)
                    {
                        Rigidbody rb = coll.GetComponent<Rigidbody>();
                        rb?.AddForce(evt.MotionDirection3D * forceMult, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
