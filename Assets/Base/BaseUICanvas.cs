using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class BaseUICanvas : MonoBehaviour
{
    /// <summary>
    /// Scene이 스타트되어야하는지 컨트롤하는 bool 변수 
    /// ex) Text 후 Scene Update 시작 같은 상황 연출 관리를 위한 변수
    /// </summary>
    protected bool is_StartScene = true;

    public abstract void Init();
    public abstract void Enter();
    public abstract void Play();
    public abstract void Exit();

    /// <summary>
    /// StartScene 변수 Get 함수 
    /// </summary>
    public bool Get_StartScene() { return is_StartScene; }
}
