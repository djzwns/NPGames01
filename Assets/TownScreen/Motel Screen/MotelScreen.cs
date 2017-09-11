using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Motel Screen UI Class
/// </summary>
public class MotelScreen : BaseScreen
{
    /// <summary>
    /// Motel Screen의 상태를 체크하기 위한 변수
    /// </summary>
    private MOTEL_TYPE m_Type;
    /// <summary>
    /// Model Screen의 상태를 표시하기위한 Enum 변수
    /// </summary>
    private enum MOTEL_TYPE { PARTY=0, SHOP, ECT }
    
    public override void Init()
    {
        m_Mode = TOWNMODE.PARTY;
        Init_Btn();
        for (int i = 0; i < m_ObjWindow.Length; i++)
        {
            m_ObjWindow[i].Init();
            m_ObjWindow[i].gameObject.SetActive(false);
        }
        base.Init();
    }
    public override void Enter()
    {
        gameObject.SetActive(true);
        is_Active = true;
        m_Obj_Idle.SetActive(true);
        for (int i = 0; i < m_ObjWindow.Length; i++)
        {
            m_ObjWindow[i].gameObject.SetActive(false);
            m_ObjWindow[i].Exit();
        }
    }
    public override void Play()
    {
        base.Play();
        if (m_CrtWindow == null) return;
        else if (m_CrtWindow != null) m_CrtWindow.Play();
        if (!m_CrtWindow.Get_Active_Window())
        {
            m_Obj_Idle.SetActive(true);
            m_CrtWindow = null;
        }
    }
    public override void Exit()
    {
        gameObject.SetActive(false);
        m_Obj_Idle.SetActive(false);
        for (int i = 0; i < m_ObjWindow.Length; i++)
        {
            m_ObjWindow[i].Exit();
        }
        is_Active = false;
    }

    /// <summary>
    /// 각 버튼의 역할을 지정하는 함수
    /// </summary>
    private void Init_Btn()
    {
     
        m_BtnList[(int)MOTEL_TYPE.PARTY].onClick.AddListener(() => Window_Open((int)MOTEL_TYPE.PARTY));
        m_BtnList[(int)MOTEL_TYPE.SHOP].onClick.AddListener(() => Window_Open((int)MOTEL_TYPE.SHOP));
        m_BtnList[(int)MOTEL_TYPE.ECT].onClick.AddListener(() => Window_Open((int)MOTEL_TYPE.ECT));
    }
    /// <summary>
    /// 모드별로 Window UI를 출력해주는 함수
    /// </summary>
    /// <param name="_type"></param>
    protected override void Window_Open(int _type)
    {
        m_Type = (MOTEL_TYPE)_type;
        if (m_CrtWindow != null) m_CrtWindow.Exit();
        m_CrtWindow = m_ObjWindow[_type];
        m_CrtWindow.Enter();
        m_Obj_Idle.SetActive(false);
    }
}