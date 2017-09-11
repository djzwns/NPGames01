using UnityEngine;
using System.Collections;

public class Bullet : BaseObj_2D
{
    /// <summary>
    /// Target Monster 변수
    /// </summary>
    [SerializeField]
    private BaseMonster m_Target;
    /// <summary>
    /// Attack Hero 변수
    /// </summary>
    [SerializeField]
    private BaseHero m_Hero;
    /// <summary>
    /// Target과의 거리를 체크하기 위한 변수
    /// </summary>
    private float m_Distans;
    /// <summary>
    /// Active(활성화 체크를 위한 변수)
    /// </summary>
    private bool is_Active;
    
    /// <summary>
    /// Update 함수
    /// </summary>
    void Update()
    {
        Vector3 dir;
        dir = m_Target.Pos - Pos;
        m_Distans = dir.sqrMagnitude;

        if (m_Distans < 0.25f)
        {
            Hit_Monster();
            is_Active = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, Get_Angle(m_Target.Pos) + 180.0f);
            Pos += dir.normalized * Time.fixedDeltaTime * 5f;
        }
    }

    /// <summary>
    /// Target Monster와 Hit 했을때 발동하는함수
    /// (Monster에게 Damege를 주고 Object Destroy)
    /// </summary>
    public void Hit_Monster()
    {
        if (is_Active)
        {
            m_Target.Damege(m_Hero.Get_Stat().AttPoint, m_Hero);
        }
        else Destroy(this.gameObject);
    }
    /// <summary>
    /// Bullet 초기화 함수(주체 영웅 설정)
    /// </summary>
    /// <param name="_Hero"></param>
    public void Init(BaseHero _Hero){ m_Hero = _Hero; is_Active = true; }

    /// <summary>
    /// Target Monster 설정하는 함수()
    /// </summary>
    /// <param name="_Target"></param>
    public void Set_Target(BaseMonster _Target)
    {
        m_Target = _Target;
        transform.eulerAngles = new Vector3(0, 0, -Get_Angle(m_Target.Pos) + 180.0f);
    }

    /// <summary>
    /// Angle을 설정하는 함수
    /// </summary>
    private float Get_Angle(float x1, float y1, float x2, float y2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;
        float rad = Mathf.Atan2(dx, dy);
        float degree = rad * Mathf.Rad2Deg;
        return degree;
    }
    private float Get_Angle(Vector3 _pos)
    {
        float dx = _pos.x - Pos.x;
        float dy = _pos.y - Pos.y;
        float rad = Mathf.Atan2(dx, dy);
        float degree = rad * Mathf.Rad2Deg;
        return degree;
    }
}