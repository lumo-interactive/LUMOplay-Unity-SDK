using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUMOplay;

[AddComponentMenu("LUMOplay/Explosion Force On Movement")]
public class ExplosionForceOnMovement : MonoBehaviour
{
    [Tooltip("Layer to check for motion on (typically a floor surface)")]
    public LayerMask triggerLayer;

    [Tooltip("Radius from motion hit to check for objects")]
    public float hitRadius = 2f;

    [Tooltip("Explosion Force to apply to valid RigidBodies within radius")]
    public float explosionForce = 3f;

    [Tooltip("What object tag to be affected by this force (with attached RigidBody and Collider)")]
    public string tagToApplyExplosionTo;

    [Tooltip("How far to check Raycasts, lower values can improve performance, but must still reach the collider")]
    public float raycastDistance = Mathf.Infinity;

    // using FixedUpdate as MotionListener only has new data during FixedUpdate
    void FixedUpdate()
    {
        // get motion data from MotionListener
        List<LumoMotionEvent> events = MotionListener.CurrentMotion;

        foreach(LumoMotionEvent evt in events)
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
                    // apply explosion force at RaycastHit hit point to rigidbodies with matching tag
                    if (tagToApplyExplosionTo.Length <= 0 || coll.tag == tagToApplyExplosionTo)
                    {
                        Rigidbody rb = coll.GetComponent<Rigidbody>();
                        rb?.AddExplosionForce(explosionForce, hit.point, hitRadius, 0f, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
