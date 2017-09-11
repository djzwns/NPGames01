using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TownScene : BaseScene
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
        m_Canvas.Play();
    }
    public override void Exit()
    {
        m_Canvas.Exit();
    }
}
