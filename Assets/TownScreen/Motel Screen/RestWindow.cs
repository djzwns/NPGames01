using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 휴식보상 Window Ui Class
/// (추후 접속 시간에 대한 UI를 표시하고 보상을 받을수있게 관리)
/// </summary>
public class RestWindow : BaseWindow
{
    private enum TIMEITEM { One, Two, Tree, Four }

    [SerializeField]
    private Button m_Ecs;
    [SerializeField]
    private Button[] m_Item;
    [SerializeField]
    private Button[] m_OkBtn;
    
    public override void Init()
    {
    }
    public override void Enter()
    {
    }
    public override void Play()
    {

    }
    public override void Exit()
    {
    }
}
