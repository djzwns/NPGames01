using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackToIdle : CutSceneAnima
{
    void OnEnable()
    {
        SetAnimation("attack", false);
        AddAimation("stand", true);
    }

    void OnDisable()
    {
        SetAnimation("stand", true);
    }
}
