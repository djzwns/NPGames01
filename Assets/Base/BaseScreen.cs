using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseScreen : MonoBehaviour
{
    [SerializeField]
    protected Image m_NPCImage;
    [SerializeField]
    protected Button[] m_BtnList;
    [SerializeField]
    protected Button m_BtnEsc;
    [SerializeField]
    protected Text m_NPCText;
    [SerializeField]
    protected GameObject m_Obj_Idle;
    protected TOWNMODE m_Mode;
    protected bool is_Active;

    [SerializeField]
    protected BaseWindow[] m_ObjWindow;
    protected BaseWindow m_CrtWindow;
    

    public virtual void Init()
    {
        m_BtnEsc.onClick.AddListener(() => Exit());
        is_Active = true;
    }
    public virtual void Enter() { }
    public virtual void Play()
    {
        if (m_CrtWindow == null) return;
        else if (!m_CrtWindow.Get_Active_Window())
        {
            m_CrtWindow = null;
            m_Obj_Idle.SetActive(true);
            return;
        }

        m_CrtWindow.Play();
    }
    public virtual void Exit() { }

    public TOWNMODE Get_Town_Mode() { return m_Mode; }
    public bool Get_Active_Screen() { return is_Active; }
    protected virtual void Window_Open(int _type)
    {
        if (m_CrtWindow != null) m_CrtWindow.Exit();
        m_CrtWindow = m_ObjWindow[_type];
        m_CrtWindow.Enter();
        Debug.Log(m_CrtWindow.name);
    }
}
