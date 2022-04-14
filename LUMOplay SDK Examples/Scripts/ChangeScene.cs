using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("LUMOplay/_Examples/Change Scene")]
public class ChangeScene : MonoBehaviour
{
    [Tooltip("Scene name to change to (Must be included in Build)")]
    public string targetSceneName;

    // public method that can be called by OnMotionXX callbacks
    public void SwitchSceneNow()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
