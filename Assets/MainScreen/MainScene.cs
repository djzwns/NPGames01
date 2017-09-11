using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 맨 처음 App 게임화면을 관리하는 Screen Class
/// 추후 GPGS Logine + User Data Load 예정
/// </summary>
public class MainScene : BaseScene
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
