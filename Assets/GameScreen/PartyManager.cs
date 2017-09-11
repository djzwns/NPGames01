using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Hero들을 관리하는 Party Manager
/// </summary>
public class PartyManager : BaseObjManager
{
    #region _변수모음_
    /// <summary>
    /// Singleton Class Instance를 위한 변수
    /// </summary>
    private static PartyManager m_Instance;
    public static PartyManager Instance
    {
        get { return m_Instance; }
        set { m_Instance = value; }
    }
    /// <summary>
    /// HP BAR에 대한 Enum
    /// </summary>
    private enum BAR { SKILL=0, H1, H2, H3 }
    /// <summary>
    /// Hero Array 변수(사용할 Hero들 정보)
    /// </summary>
    [SerializeField]
    private BaseHero[] m_Heros;
    /// <summary>
    /// Active Hero를 관리하는 변수(살아있는 Hero 관리)
    /// </summary>
    [SerializeField]
    private List<BaseHero> m_ActiveHeros;
    /// <summary>
    /// 선택된 영웅의 Skill 발동 Btn Array
    /// </summary>
    [SerializeField]
    private Button[] m_SkillBtn;
    /// <summary>
    /// 선택한 현재 Hero를 체크하는 변수
    /// </summary>
    [SerializeField]
    private BaseHero m_CurrentHero;
    /// <summary>
    /// 현재 스테이지에서 죽은 Hero의 수
    /// </summary>
    [SerializeField]
    private int m_DeadCount;
    /// <summary>
    /// 모든 영웅이 죽었을 때를 체크하기 위한 변수
    /// </summary>
    private bool is_Dead;
    public bool All_Dead
    {
        get { return is_Dead; }
        set { is_Dead = value; }
    }
    /// <summary>
    /// 스테이지를 클리어 했을 때를 체크하기 위한 변수
    /// </summary>
    private bool is_Clear;
    public bool All_Clear
    {
        get { return is_Clear; }
        set { is_Clear = value; }
    }
    /// <summary>
    /// 모든 Hero가 Base 위치에 도달했는지 체크하기 위한 변수
    /// </summary>
    private bool is_Base;
    /// <summary>
    /// 3마리의 Hero의 기본위치를 저장하는 Array
    /// </summary>
    [SerializeField]
    private Vector3[] m_BasePos;
#endregion

    public override void Init()
    {
        m_ActiveHeros = new List<BaseHero>(0);
        Instance = this;
        //m_Heros = new List<BaseHero>(0);
        // Skill Btn에 스킬 적용하는 함수
        m_SkillBtn[0].onClick.AddListener(() => Skill_Number_1());
        m_SkillBtn[1].onClick.AddListener(() => Skill_Number_2());
    }
    public override void Enter()
    {
        is_Base = false;
        is_Dead = false;
        is_Clear = false;
        // Monster Manager에 Target이 될 Hero의 Array를 보내는 곳
        MonsterManager.Instance.Set_Heros(m_Heros);

        // 플레이어 Data를 가져와서 Hero Obj에 Hero의 Stat Data를 재설정하고, 활성화 하는작업
        for (int i = 0; i < m_Heros.Length; i++)
        {
            if(PlayerData.Instance.Get_Party_Hero(i) == null)
            {
                m_Heros[i].Set_Hero_Stat(null);
            }
            else
            {
                m_Heros[i].Set_Hero_Stat(DataTable.Instance.Get_Hero_Data(PlayerData.Instance.Get_Party_Hero(i)));
                m_ActiveHeros.Add(m_Heros[i]);
            }
        }

        // 활성화된 Hero에게 Base Pos를 전달하는 곳
        for (int i = 0; i < m_ActiveHeros.Count; i++)
        {
            m_ActiveHeros[i].Set_BasePos(m_BasePos[i]);
            m_ActiveHeros[i].Enter();
            m_ActiveHeros[i].Pos = m_BasePos[i] - new Vector3(4.0f, 0, 0);
        }
    }
    public override void Play()
    {
        // Active Hero들을 Update
        for (int i = 0; i < m_ActiveHeros.Count; i++)
        {
            if (!m_ActiveHeros[i].Active)
            {
                m_ActiveHeros[i].Exit();
                m_ActiveHeros.Remove(m_ActiveHeros[i]);
                return;
            }
            else m_ActiveHeros[i].Play();
        }

        // Touch Event 처리
        Touch_Function();

        // Stage End 인지 체크
        StageEnd();

        // 패배 했는지에 대한 체크
        if (m_ActiveHeros.Count == 0)
        {
            Debug.Log("Dead");
            is_Dead = true;
        }
        m_CurrentHero = null;
    }
    public override void Exit()
    {
        m_ActiveHeros.Clear();
    }

    /// <summary>
    /// Touch 이벤트에 대한 처리를 모아놓은 함수
    /// </summary>
    private void Touch_Function()
    {
        // Stage End시 retrun
        if (MonsterManager.Instance.EndStage) return;
        //#if UNITY_EDITOR
        //if (m_CurrentHero == null)
        {
            // 클릭시
            if (Input.GetMouseButtonDown(0))
            {
                // 클릭된 곳의 Point를 가져옴
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Ray를 클릭된곳의 Positon에 쏴서 체크
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                // Ray를 쏜것을 기반으로 Hit된 Obj가 있는지 체크하기 위한 변수
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Hit된 Obj가 Hero 일 때
                if (hit.transform != null && hit.transform.tag.Equals("Hero"))
                {
                    // 선택된 영웅으로 현재 Hero를 변경
                    // 추후 버프 스킬 추가시 추가조각 필요
                    Check_Hero(hit.transform.gameObject);
                }
                // Hit된 Obj가 Monster일 떄
                else if (m_CurrentHero != null && (hit.transform != null && hit.transform.tag.Equals("Mon")))
                {
                    // 선택된 Monster로 타겟 변경
                    m_CurrentHero.Set_Target(hit.transform.GetComponent<BaseMonster>());
                }
                // Hit된 Obj가 없을때
                else if (m_CurrentHero != null && hit.transform == null)
                {
                    // 지정된 위치로 이동 
                    // 일정 위치이상으로 올라가려고하면 그좌표에서 이동할수있는곳으로 이동
                    if (wp.x >= 3.8f && wp.y <= -2.35f) return;
                    else if (wp.y >= 1.6f)
                    {
                        wp.y = 1.6f;
                        m_CurrentHero.Set_Point(wp);
                    }
                    else m_CurrentHero.Set_Point(wp);
                }
            }
        }
//#endif
    }
    
    /// <summary>
    /// 현재 선택한 Hero를 변경하는 함수
    /// </summary>
    /// <param name="_obj"></param>
    private void Check_Hero(GameObject _obj)
    {
        for (int i = 0; i < m_Heros.Length; i++)
        {
            if (_obj == m_Heros[i].gameObject)
            {
                // 현재 선택 Hero 변경 및 Layer 조절
                m_CurrentHero = m_Heros[i];
                m_CurrentHero.Pick(3);
            }
            // 다른 Hero들 Layer 조절
            else m_Heros[i].Pick(1);
        }
    }
    /// <summary>
    /// 현재 Hero의 스킬 1 실행
    /// </summary>
    private void Skill_Number_1()
    {
        if (m_CurrentHero != null)
        {
            m_CurrentHero.Skill_One();
        }
    }
    /// <summary>
    /// 현재 Hero의 스킬 2 실행
    /// </summary>
    private void Skill_Number_2()
    {
        if (m_CurrentHero != null)
        {
            m_CurrentHero.Skill_Two();
        }
    }
    /// <summary>
    /// Hero 들이 Base 위치에 전부 도달했는지 체크하는 함수
    /// </summary>
    /// <returns></returns>
    public bool Base()
    {
        if (is_Base != false) return is_Base;
        int count = 0;
        for (int i = 0; i < m_ActiveHeros.Count; i++)
        {
            if (m_ActiveHeros[i].Get_Base()) { count++;}
        }

        if (count == m_ActiveHeros.Count)
        {
            is_Base = true;
        }

        return is_Base;
    }
    /// <summary>
    /// 현재 Stage가 끝났는지 체크하는 함수
    /// </summary>
    public void StageEnd()
    {
        if (MonsterManager.Instance.EndStage
            && Check_Win())
        {
            is_Clear = true;
        }
    }

    /// <summary>
    /// Pause 상태 일때 발생하는 함수
    /// </summary>
    public void On_Pause()
    {
        for (int i = 0; i < m_ActiveHeros.Count; i++)
        {
            m_ActiveHeros[i].On_Pause();
        }
    }
    /// <summary>
    /// Pause -> Play로 상태변화 할때 실행되는 함수
    /// </summary>
    public void On_BACK()
    {
        for (int i = 0; i < m_ActiveHeros.Count; i++)
        {
            m_ActiveHeros[i].On_Play();
        }
    }
    /// <summary>
    /// 승리 State 일때 발생하는 함수 (Hero들을 Win으로)
    /// </summary>
    public void Win()
    {
        if (MonsterManager.Instance.EndStage) return;
        for(int i = 0;i< m_ActiveHeros.Count; i++)
        {
            m_ActiveHeros[i].Win_Pose();
        }
    }
    /// <summary>
    /// 모든 Hero 들의 Win상태가 True가되면 true 반환
    /// </summary>
    /// <returns></returns>
    private bool Check_Win()
    {
        int count = 0;
        for (int i = 0; i < m_ActiveHeros.Count; i++)
        {
            if(m_ActiveHeros[i].is_Win)
            {
                count++;
            }
        }

        if (count == m_ActiveHeros.Count)
        {
            return true;
        }
        else return false;
    }
    /// <summary>
    /// Scene을 재시작할때 실행되는 함수
    /// </summary>
    public void ReStart()
    {
        Exit();
        Enter();
    }
}