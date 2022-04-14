using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")] // hide from component menu
public class SimpleGameAI : MonoBehaviour
{
    public Vector3 target;

    public float speed = 1f;

    // Moves transform towards target
    void Update()
    {
        var dir = target - transform.position;
        dir = dir.normalized * speed * Time.deltaTime;
        transform.Translate(dir);
    }

    // public method that can be called by OnMotionXX callbacks
    public void Die()
    {
        SimpleScoring.AddPoint();
        Destroy(gameObject);
    }
}
