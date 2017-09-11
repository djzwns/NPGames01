using UnityEngine;
using System.Collections;


/// <summary>
/// Unity Sprite 2D Base Object Class
/// Unity Sprite 2D Object들을 상속해주는 클래스
/// </summary>
public class BaseObj_2D : MonoBehaviour 
{
    // 2D Object SpriteRenderer
    protected SpriteRenderer m_Renderer2D;
    // 2D Object BoxCollider2D
    protected BoxCollider2D m_Collider2D;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public virtual void Init()
    {
        m_Renderer2D = GetComponent<SpriteRenderer>();
        m_Collider2D = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Enter 함수 -> Object Manager에서 Scene진입시 1회 호출(재 초기화)
    /// </summary>
    public virtual void Enter() { }

    /// <summary>
    /// Update 함수
    /// </summary>
    public virtual void Play() { }

    /// <summary>
    /// Exit 함수 -> Object의 Active 상태가 flase 일때 호출
    /// </summary>
    public virtual void Exit() { }

    /// <summary>
    /// Object의 Position을 Get,Set하는 파라미터
    /// </summary>
    public Vector3 Pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    } 
}
