using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimation : CutSceneAnima
{
    public GameObject bubble;

    void OnEnable()
    {
        bubble.SetActive(true);
    }

    void OnDisable()
    {
        bubble.SetActive(false);
    }
}
