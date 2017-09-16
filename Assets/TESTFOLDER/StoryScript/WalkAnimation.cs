using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimation : CutSceneAnima
{
    void OnEnable()
    {
        SetAnimation("working", true);
    }

    void OnDisable()
    {
        SetAnimation("stand", true);
    }
}
