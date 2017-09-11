using UnityEngine;
using System.Collections;

public class BaseWindow : MonoBehaviour
{
    protected bool is_Active;
    public virtual void Init() { }
    public virtual void Enter() { }
    public virtual void Play() { }
    public virtual void Exit() { }
    public bool Get_Active_Window() { return is_Active; }
}
