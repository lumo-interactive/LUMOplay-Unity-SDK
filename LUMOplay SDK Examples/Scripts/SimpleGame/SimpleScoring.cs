using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")] // hide from component menu
[RequireComponent(typeof(TextMesh))]
public class SimpleScoring : MonoBehaviour
{
    // singleton stuff (it is a simple example after all)
    private static List<SimpleScoring> instances;
    private static int score;

    TextMesh text;

    void Start()
    {
        // set instance
        if (instances == null) instances = new List<SimpleScoring>();
        instances.Add(this);

        // set TextMesh reference
        text = GetComponent<TextMesh>();
    }

    // add points and update the text
    public static void AddPoint()
    {
        score++;
        foreach (SimpleScoring instance in instances)
        {
            instance.UpdateText();
        }
    }

    // update the text
    private void UpdateText()
    {
        text.text = score.ToString();
    }

    // remove points and update the text
    public static void RemovePoints(int points)
    {
        score -= points;
        foreach (SimpleScoring instance in instances)
        {
            instance.UpdateText();
        }
    }
}
