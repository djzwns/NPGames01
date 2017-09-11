using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Shop Screen을 관리하는 Ui Class
/// </summary>
public class ShopScreen : BaseScreen
{
    private SHOP_TYPE m_Type;
    private enum SHOP_TYPE { CUSTM=0, SHOP }
    public override void Init()
    {
        for (int i = 0; i < m_ObjWindow.Length; i++)
        {
            if(i==0)
            {
                return;
            }
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
    /// 버튼 초기화를 위한 함수
    /// </summary>
    private void Init_Btn()
    {
        //m_BtnList[(int)SHOP_TYPE.CUSTM].onClick.AddListener(() => Window_Open((int)SHOP_TYPE.CUSTM));
        m_BtnList[(int)SHOP_TYPE.SHOP].onClick.AddListener(() => Window_Open((int)SHOP_TYPE.SHOP));
    }
    /// <summary>
    /// 선택한 버튼에 맞는 Window UI를 출력하기 위한 함수
    /// </summary>
    /// <param name="_type"></param>
    protected override void Window_Open(int _type)
    {
        m_Type = (SHOP_TYPE)_type;
        if (m_CrtWindow != null) m_CrtWindow.Exit();
        m_CrtWindow = m_ObjWindow[_type];
        m_CrtWindow.Enter();
        m_Obj_Idle.SetActive(false);
    }
}
