using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// StageScreen를 관리하기위한 ScreenC lass
/// </summary>
public class StageScene : BaseScene
{
    public override void Init()
    {
        m_Canvas.Init();
    }
    public override void Enter()
    {
        m_Canvas.Enter();
    }
    public override void Play()
    {
    }
    public override void Exit()
    {
        m_Canvas.Exit();
    }
}