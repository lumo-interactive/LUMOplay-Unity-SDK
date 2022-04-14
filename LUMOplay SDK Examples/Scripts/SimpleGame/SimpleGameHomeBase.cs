using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")] // hide from component menu
public class SimpleGameHomeBase : MonoBehaviour
{
    // when a RigidBody enters the Home Base Collder2D, destroy it and subtract points
    private void OnCollisionEnter2D(Collision2D other)
    {
        SimpleScoring.RemovePoints(10);
        Destroy(other.gameObject);
    }
}
