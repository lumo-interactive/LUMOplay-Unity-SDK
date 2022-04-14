using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")] // hide from component menu
public class SimpleGameSpawner : MonoBehaviour
{
    [Tooltip("Prefab to spawn")]
    public GameObject badGuyPrefab;

    [Tooltip("How often to spawn prefab")]
    public float spawnInterval;

    [Tooltip("X-distance to vary spawning (based on a SIN wave)")]
    public float spawnerMoveDistance;

    [Tooltip("How erratic the spawning is (based on a SIN wave)")]
    public float spawnerMoveSpeed;

    float phase; // for SIN wave
    float timer = 0f; // spawn timer
    Vector3 home;

    void Start()
    {
        // set the phase start
        phase = Random.Range(0f, Mathf.PI * 2);
        // set the home position to current position
        home = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // advance the phase and timer
        phase += spawnerMoveSpeed * Time.deltaTime;
        timer -= Time.deltaTime;

        // move the spawner
        transform.localPosition = home + Vector3.right * Mathf.Sin(phase) * spawnerMoveDistance;

        // if timer reaches 0, reset and spawn prefab
        if (timer <= 0f)
        {
            timer = spawnInterval;
            Spawn();
        }
    }

    // spawn a prefab at spawners current position
    void Spawn()
    {
        Instantiate(badGuyPrefab, transform.position, Quaternion.identity, null);
    }
}
