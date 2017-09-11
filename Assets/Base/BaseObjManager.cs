using UnityEngine;
using System.Collections;

/// <summary>
/// Object들을 컨트롤하기위한 Base Object Manager
/// </summary>
public abstract class BaseObjManager : MonoBehaviour
{
    public abstract void Init();
    public abstract void Enter();
    public abstract void Play();
    public abstract void Exit();
}
