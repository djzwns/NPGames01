using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Base Boss Monster Class
/// Base Monster를 상속받아 기존의 함수를 사용
/// </summary>
public class BaseBossMonster : BaseMonster
{
    /// <summary>
    /// Boss의 이미지를 저장 -> 초상화
    /// </summary>
    [SerializeField]
    private Image m_BossImage;

    public override void Init()
    {
        base.Init();
        // Hp_UI 관리
        m_HpVar.gameObject.SetActive(false);
        // Boss 초상화 UI 관리
        m_BossImage.gameObject.SetActive(false);
    }
    public override void Enter()
    {
        // UI 표시
        m_HpVar.gameObject.SetActive(true);
        m_BossImage.gameObject.SetActive(true);

        // Object를 활성화
        Active = true;
        this.gameObject.SetActive(true);

        // Monster의 현재 상태 설정
        m_Mode = MONSTERMODE.IDLE;
        // 탐색거리 설정 초기화 
        m_Distans = Mathf.Infinity;
        // 적 탐색 기본시간 설정
        m_searchTime = 3.0f;
    }
    public override void Play()
    {
        // Hp UI업데이트 
        m_HpVar.Set_MaxValue(m_AttStat.MaxHP);
        m_HpVar.Set_NowValue(m_AttStat.NowHP);
        m_HpVar.Play();

        // Active -> Monster Dead 관리
        if (m_AttStat.NowHP <= 0) Active = false;

        // Monster 상태에 따른 함수 호출
        switch (m_Mode)
        {
            case MONSTERMODE.IDLE: Mode_Idle(); break;
            case MONSTERMODE.ATTACK: Mode_Attack(); break;
            case MONSTERMODE.ATTACKMOVE: Mode_AttackMove(); break;
        }
    }
    public override void Exit()
    {
        m_HpVar.gameObject.SetActive(false);
        m_BossImage.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Active = false;
        m_Mode = MONSTERMODE.IDLE;
        m_Distans = Mathf.Infinity;
        m_Heros = null;
        MonsterManager.Instance.Remove_Mon(this);
    }

    /// <summary>
    /// Target의 위치에따른 Filp 설정
    /// </summary>
    /// <param name="_pos"></param>
    public override void Filp_X(Vector3 _pos)
    {
        if ((Pos.x - _pos.x) <= 0.1f)
        {
            //m_HeroAnima.initialFlipX = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            //m_HpVar.Filp(false);
        }
        else
        {
            //m_HeroAnima.initialFlipX = true;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //m_HpVar.Filp(true);
        }
    }
}
