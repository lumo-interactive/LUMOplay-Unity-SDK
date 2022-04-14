using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUMOplay;

[AddComponentMenu("LUMOplay/Initialize Communication")]
/**
 * This component creates all communication objects needed by LUMOplay.
 * If Motion components are already in use, these objects are created automatically.
 * This is only necessary when Motion detection is not being used.
 * */
public class InitializeCommunication : MonoBehaviour
{
    [Header("This component is used to connect to the", order = 0)]
    [Space(-10, order = 1)]
    [Header("LUMOplay communication systems when there", order = 2)]
    [Space(-10, order = 3)]
    [Header("is no Motion Detection (e.g. mouse or touch games).", order = 4)]
    [Space(-10, order = 5)]
    [Header("Connect once to any object in your starting scene.", order = 6)]
        
    public bool active = true;

    void Start()
    {
        // useless call to MotionListener to create persistant communication objects
        var m = MotionListener.CurrentMotion;
    }
}
