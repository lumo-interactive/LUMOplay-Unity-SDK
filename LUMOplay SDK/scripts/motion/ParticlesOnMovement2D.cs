using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUMOplay;

[AddComponentMenu("LUMOplay/Particles On Movement 2D")]
[RequireComponent(typeof(Collider2D))]
public class ParticlesOnMovement2D : MonoBehaviour
{
    [Header("Particle Systems to spawn particles on movement")]
    public ParticleSystem[] particleSystems;

    [Header("Number of particles to spawn each second")]
    public int particlesPerSecond = 10;

    [Header("Axis to lock based on the transform of this object")]
    [Tooltip("This is necessary for sprite-sorting errors with multiple ParticleSystems")]
    public Axis lockedAxis = Axis.Z;

    float particlesToSpawn;

    Collider2D motionCollider;

    // using FixedUpdate as MotionListener only has new data during FixedUpdate
    void FixedUpdate()
    {
        // set the objects Collider2D object
        if (motionCollider == null) motionCollider = GetComponent<Collider2D>();

        // get motion data from MotionListener
        List<LumoMotionEvent> events = MotionListener.CurrentMotion;

        // add to particles to spawn only if under per second value to prevent a buildup
        if (particlesToSpawn < particlesPerSecond) particlesToSpawn += Time.fixedDeltaTime * particlesPerSecond;

        // reset particles to spawn if there is no movement to prevent smaller buildup
        if (events.Count <= 0) particlesToSpawn = Time.fixedDeltaTime;

        // collect all hits on Collider2D
        List<Ray> rays = new List<Ray>();
        foreach (LumoMotionEvent evt in events)
        {
            List<Ray> tmp = evt.Scatter;
            foreach (Ray ray in tmp)
            {
                if (motionCollider.OverlapPoint(ray.origin))
                {
                    rays.Add(ray);
                }
            }
        }

        // randomly select rays to spawn particles at (based on number of particles waiting to spawn)
        // cannot spawn more than one particle per ray
        while (particlesToSpawn > 1f)
        {
            if (rays.Count <= 0) break;
            int i = Random.Range(0, rays.Count);
            Ray ray = rays[i];
            rays.RemoveAt(i);
            SpawnParticles(ray.origin);
            particlesToSpawn -= 1f;
        }
    }

    // spawn a particle at the hit location randomly picked from available ParticleSystems
    // (an axis position is locked to the parent objects transform, slightly offset for each particleSystem to avoid sprite sorting errors)
    public void SpawnParticles(Vector3 point)
    {
        var i = Random.Range(0, particleSystems.Length);
        var ps = particleSystems[i];

        switch (lockedAxis)
        {
            case Axis.X:
                point.x = transform.position.x + i * .1f;
                break;

            case Axis.Y:
                point.y = transform.position.y + i * .1f;
                break;

            case Axis.Z:
                point.z = transform.position.z + i * .1f;
                break;
        }

        ps.transform.position = point;
        ps.Emit(1);
    }
}
