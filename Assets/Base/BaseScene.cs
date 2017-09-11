using UnityEngine;
using System.Collections;

/// <summary>
/// Base Scene Class
/// Scene을 Object화 하여서 관리하기 위한 Class
/// </summary>
public abstract class BaseScene : MonoBehaviour
{
    // 초기화 되었는지 확인하기 위한 bool 변수
    protected bool is_Init;
    // Scene의 UI를 관리하기위한 변수
    [SerializeField]
    protected BaseUICanvas m_Canvas;
    // Object들을 관리하는 매니저들을 관리하기 위한 Array
    [SerializeField]
    protected BaseObjManager[] m_ObjManager;
    
    public abstract void Init();
    public abstract void Enter();
    public abstract void Play();
    public abstract void Exit();

    // 초기화 되었는지 확인하기위한 Get 함수
    public bool Get_Init() { return is_Init; }
}
