using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")] // hide from component menu
/**
 * Captures and displays Debug.Log messages on an in-game TextMesh object, extremely useful when testing published games within LUMOplay
 * Toggle key is configurable, canvas is hidden by default
 */
public class DebugCapture : MonoBehaviour
{
    public KeyCode toggleKey;
    //public TextMeshProUGUI text;
    public Text text;
    public GameObject popup;
    public ScrollRect scroller;
    public bool IncludeStackTrace;

    string log = "";

    void Awake()
    {
        // subscribe to logging callback
        Application.logMessageReceivedThreaded += Application_logMessageReceived;
        // start message (version pulled from publish settings)
        Debug.Log("Application Started: Version " + Application.version);
    }

    private void Start()
    {
        // hide canvas to start
        popup.SetActive(false);
    }

    // callback to capture Log messages, adds them to the log string
    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (IncludeStackTrace) log += stackTrace;
        log += condition + "\n\n";
    }

    void Update()
    {
        // toggles visiblity of canvas
        if (Input.GetKeyDown(toggleKey))
        {
            popup.SetActive(!popup.activeInHierarchy);
            scroller.verticalNormalizedPosition = 0f;
        }

        // updates TextMesh if there's change to the log string
        if (text.text != log) text.text = log;
    }
}
