using UnityEngine;
using System.Collections;

/// <summary>
/// Game Scene을 구동하는 Class
/// </summary>
public class GameScene : BaseScene
{
    public override void Init()
    {
        m_Canvas.Init();
        for (int i = 0; i < m_ObjManager.Length; i++)
        {
            m_ObjManager[i].Init();
        }
    }
    public override void Enter()
    {
        m_Canvas.Enter();
        for (int i = 0; i < m_ObjManager.Length; i++)
        {
            m_ObjManager[i].Enter();
        }
    }
    public override void Play()
    {
        m_Canvas.Play();
        // Pause 되면 매니저들 update를 금지함
        if (MonsterManager.Instance.Pause) { return; }

        for(int i = 0; i< m_ObjManager.Length; i++)
        {
            m_ObjManager[i].Play();
        }
    }
    public override void Exit()
    {
        m_Canvas.Exit();
        for (int i = 0; i < m_ObjManager.Length; i++)
        {
            m_ObjManager[i].Exit();
        }
    }
}