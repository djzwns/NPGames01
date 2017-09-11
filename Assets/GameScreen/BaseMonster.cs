using UnityEngine;
using System.Collections;
using Spine.Unity;

/// <summary>
/// Monster의 Stat Struct & DB 
/// </summary>
public struct MonsterStat
{
    public string MonID;
    public string IllustID;
    public string MonsterName;
    public float Race;
    public float SubClass;
    public float Grade;
    public float Position;
    public float Frenz;
    public float MinAttPower;
    public float MinDefendPower;
    public float MinHP;
    public float AttPerLV;
    public float DefPerLV;
    public float HPPERLV;
    public float FurtherDmg;
    public float DmgOffset;
    public float Cooldown;
    public float MoveSpeed;
    public float AttSpeed;
    public float AttRange;
    public float NoticeRange;
    public string pattern;
    public string Scale;
    public float RewardExp;
    public float Exp;
    public float RewardMoney;
    public float Money;

    public void Set(MonsterStat _Temp)
    {
        MonID = _Temp.MonID;
        IllustID = _Temp.IllustID;
        MonsterName = _Temp.MonsterName; 
        Race = _Temp.Race;
        SubClass = _Temp.SubClass; 
        Grade = _Temp.Grade; 
        Position = _Temp.Position; 
        Frenz = _Temp.Frenz; 
        MinAttPower = _Temp.MinAttPower; 
        MinDefendPower = _Temp.MinDefendPower; 
        MinHP = _Temp.MinHP; 
        AttPerLV = _Temp.AttPerLV; 
        DefPerLV = _Temp.DefPerLV; 
        HPPERLV = _Temp.HPPERLV; 
        FurtherDmg = _Temp.FurtherDmg; 
        DmgOffset = _Temp.DmgOffset; 
        Cooldown = _Temp.Cooldown; 
        MoveSpeed = _Temp.MoveSpeed; 
        AttSpeed = _Temp.AttSpeed; 
        AttRange = _Temp.AttRange; 
        NoticeRange = _Temp.NoticeRange;
        pattern = _Temp.pattern; 
        Scale = _Temp.Scale; 
        RewardExp = _Temp.RewardExp; 
        Exp = _Temp.Exp; 
        RewardMoney = _Temp.RewardMoney;
        Money = _Temp.Money;
    }
}
/// <summary>
/// 몬스터의 레벨에 따른 실제 Stat
/// </summary>
public struct MonAttStat
{
    public int Level;
    public float AttPoint;
    public float DefPoint;
    public float MaxHP;
    public float NowHP;
}

/// <summary>
/// Base Monster Class
/// </summary>
public class BaseMonster : BaseObj_2D
{
    #region _변수 모음_
    // Monster의 상태 Enum
    protected enum MONSTERMODE { IDLE = 0, ATTACK, ATTACKMOVE }
    // Monster의 Spine Animation 변수
    [SerializeField]
    protected SkeletonAnimation m_MonAnima;
    // Monster의 현재 상태 변수
    [SerializeField]
    protected MONSTERMODE m_Mode;
    // Monster의 DB Stat 변수
    protected MonsterStat m_Stat;
    // Monster의 현재(레벨) Stat 변수
    protected MonAttStat m_AttStat;
    // Monster Active 변수(활성화,비활성화) 
    [SerializeField]
    protected bool is_Active;
    public bool Active
    {
        get { return is_Active; }
        set { is_Active = value; }
    }
    // Target과의 거리를 체크하기위한 변수
    protected float m_Distans;
    // Target이 될 영웅 Array 변수
    protected BaseHero[] m_Heros;
    // 현재 Target이 된 영웅 변수
    protected BaseHero m_Target;
    // 어그로 관리를 위한 Hero 변수
    protected BaseHero m_FirstAttHero;
    // Target과의 거리를 체크하기위한 Vector3 변수
    protected Vector3 dir;
    // Attact Speed를 체크하기위한 Float 변수
    protected float m_AS_Time;
    // Target을 재설정하기위한 시간을 설정하는 변수
    [SerializeField]
    protected float m_searchTime;
    // HP UI 변수
    [SerializeField]
    protected UI_Bar m_HpVar;
    // Pause 체크 변수
    protected bool is_Pause;
    // 어그로 관리를 위한 Bool 변수_1
    [SerializeField]
    protected bool is_Damage;
    // 어그로 관리를 위한 Bool 변수_2 ( 첫히트인지 체크)
    [SerializeField]
    protected bool is_FirHit;
    // 어그로 타임을 체크하기위한 변수
    [SerializeField]
    protected float m_firattTime;
    #endregion

    public override void Init()
    {
        base.Init();
        m_Stat = new MonsterStat();
        Exit();
    }
    public override void Enter()
    {
        m_FirstAttHero = null;
        m_firattTime = 0;
        is_Damage = false;
        is_FirHit = false;
        is_Pause = false;
        m_HpVar.Set_MaxValue(m_AttStat.MaxHP);
        m_HpVar.Set_NowValue(m_AttStat.NowHP);
        m_HpVar.gameObject.SetActive(true);
        Active = true;
        m_MonAnima.timeScale = 1;
        this.gameObject.SetActive(true);
        m_Mode = MONSTERMODE.IDLE;
        m_Distans = Mathf.Infinity;
        m_searchTime = 3.0f;
    }
    public override void Play()
    {
        if(is_Pause) return;

        m_HpVar.Set_MaxValue(m_AttStat.MaxHP);
        m_HpVar.Set_NowValue(m_AttStat.NowHP);
        m_HpVar.Play();
        
        if (m_AttStat.NowHP <= 0) Active = false;

        //처음 맞았을때부터 얼마나 지났는지에 대한 시간 추가
        m_firattTime += Time.fixedDeltaTime;

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
        this.gameObject.SetActive(false);
        Active = false;
        m_Mode = MONSTERMODE.IDLE;
        m_Distans = Mathf.Infinity;
        m_Heros = null;
        MonsterManager.Instance.Remove_Mon(this);
    }

    /// <summary>
    /// Monster Stat을 가져오는 함수
    /// </summary>
    /// <param name="_stat"></param>
    public void Set_Monster_Stat(MonsterStat _stat)
    {
        m_Stat.Set(_stat);
        m_AttStat.MaxHP = m_Stat.MinHP + (m_AttStat.Level * m_Stat.HPPERLV);
        m_AttStat.AttPoint = m_Stat.MinAttPower + (m_AttStat.Level * m_Stat.AttPerLV);
        m_AttStat.DefPoint = m_Stat.MinDefendPower + (m_AttStat.Level * m_Stat.DefPerLV);
        m_AttStat.NowHP = m_AttStat.MaxHP;
    }
    /// <summary>
    /// Monster Stat을 가져오고, 실제 Stat을 설정 (Lv 비례)
    /// </summary>
    /// <param name="_stat"></param>
    /// <param name="_lv"></param>
    public void Set_Monster_Stat(MonsterStat _stat, float _lv)
    {
        m_AttStat.Level = (int)_lv;
        m_Stat.Set(_stat);
        m_AttStat.MaxHP = m_Stat.MinHP + ((m_AttStat.Level - 1) * m_Stat.HPPERLV);
        m_AttStat.AttPoint = m_Stat.MinAttPower + ((m_AttStat.Level - 1) * m_Stat.AttPerLV);
        m_AttStat.DefPoint = m_Stat.MinDefendPower + ((m_AttStat.Level - 1) * m_Stat.DefPerLV);
        m_AttStat.NowHP = m_AttStat.MaxHP;
    }

    /// <summary>
    /// Attack Move 상태일때 함수
    /// </summary>
    protected void Mode_AttackMove()
    {
        //if (m_searchTime <= 0) { Search_Target(); m_searchTime = 3.0f; }
        //else
        {
            if(m_Target!=null && m_Target.Active)
            {
                if (!is_Damage && m_firattTime > 3.0f && is_FirHit)
                {
                    Search_Target();
                }
                Filp_X(m_Target.Pos);
                if (m_Distans <= m_Stat.AttRange) { m_Mode = MONSTERMODE.ATTACK; m_Distans = Mathf.Infinity; }
                m_MonAnima.loop = true;
                m_MonAnima.AnimationName = "working";
                dir = m_Target.Pos - Pos;
                m_Distans = dir.sqrMagnitude;
                Pos += dir.normalized * Time.fixedDeltaTime * m_Stat.MoveSpeed;
                m_searchTime -= Time.fixedDeltaTime;
            }
        }
    }
    /// <summary>
    /// Attack 상태의 함수
    /// </summary>
    protected void Mode_Attack()
    {
        if (!m_Target.Active)
        {
            m_firattTime = 0;
            m_FirstAttHero = null;
            m_Target = null;
            is_Damage = false;
            is_FirHit = false;
            Search_Target();
            m_Mode = MONSTERMODE.IDLE;
            return;
        }

        if (!is_Damage && m_firattTime > 3.0f && is_FirHit)
        {
            Search_Target();
            m_Mode = MONSTERMODE.ATTACKMOVE;
        }

        Filp_X(m_Target.Pos);
        dir = m_Target.Pos - Pos;
        m_Distans = dir.sqrMagnitude;
        
        if (m_Distans >= m_Stat.AttRange)
        {
            m_MonAnima.loop = true;
            m_MonAnima.AnimationName = "stand";
            m_MonAnima.timeScale = 1;
            m_Mode = MONSTERMODE.IDLE;
        }
        //if (m_Stat.NR >= m_Stat.AR)
        //{
        //    m_Mode = MONSTERMODE.IDLE;
        //}
        else
        {
            if (m_AS_Time >= m_Stat.AttSpeed)
            {
                if (!m_Target.Active && m_Target != null)
                {
                    m_MonAnima.loop = true;
                    m_MonAnima.AnimationName = "stand";
                    m_MonAnima.timeScale = 1;
                }
                else
                {
                    m_MonAnima.loop = true;
                    m_MonAnima.AnimationName = "stand";
                    m_MonAnima.timeScale = 1;
                    m_Target.Damege(m_AttStat.AttPoint);
                }
                m_AS_Time = 0;
            }
            else
            {
                if (m_Target != null) Filp_X(m_Target.Pos);
                m_MonAnima.loop = false;
                m_MonAnima.AnimationName = "attack";
                m_AS_Time += Time.fixedDeltaTime;
            }
        }
    }
    /// <summary>
    /// Idle 상태의 함수
    /// </summary>
    protected void Mode_Idle()
    {
        m_MonAnima.loop = true;
        m_MonAnima.AnimationName = "stand";
        m_MonAnima.timeScale = 1;
        Search_Target();
        m_Mode = MONSTERMODE.ATTACKMOVE;
    }
    
    /// <summary>
    /// 영웅을 검색하여 Target을 설정하는 함수
    /// </summary>
    private void Search_Target()
    {
        int Ran = Random.Range(0, m_Heros.Length);
        if (m_Heros[Ran].Active)
        {
            m_FirstAttHero = null;
            m_Target = m_Heros[Ran];
            m_firattTime = 0;
            is_FirHit = false;
            is_Damage = false;
        }
        //float Min = Mathf.Infinity;
        //for (int i = 0; i < m_Heros.Length; i++)
        //{
        //    if (Min > Vector3.Distance(m_Heros[i].Pos, Pos) && m_Heros[i].Active)
        //    {
        //        Min = Vector3.Distance(m_Heros[i].Pos, Pos);
        //        m_Target = m_Heros[i];
        //    }
        //}
    }
    /// <summary>
    /// Target이 될 Hero Array를 받아오는 함수
    /// </summary>
    /// <param name="_Heros"></param>
    public void Set_Heros(BaseHero[] _Heros) { m_Heros = _Heros; }
    /// <summary>
    /// Damege를 받는 함수
    /// </summary>
    /// <param name="_Damege"></param>
    /// <param name="_HitHero"></param>
    public void Damege(float _Damege, BaseHero _HitHero)
    {
        if(m_FirstAttHero==null &&!is_Damage)
        {
            m_firattTime = 0;
            m_FirstAttHero = _HitHero;
            m_Target = m_FirstAttHero;
            is_Damage = true;
            is_FirHit = true;
        }
        else if(m_FirstAttHero!=null && m_FirstAttHero==_HitHero)
        {
            m_firattTime = 0;
        }

        if (_Damege > m_AttStat.DefPoint)
        {
            m_AttStat.NowHP -= (_Damege - m_AttStat.DefPoint);
        }
        else m_AttStat.NowHP -= 1;
    }
    /// <summary>
    /// Target or Point를 확인해 Filp을 바꾸는 함수
    /// </summary>
    /// <param name="_pos"></param>
    public virtual void Filp_X(Vector3 _pos)
    {
        if ((Pos.x - _pos.x) <= 0.1f)
        {
            //m_HeroAnima.initialFlipX = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            m_HpVar.transform.localEulerAngles = new Vector3(0, 0, 0);
            //m_HpVar.Filp(false);
        }
        else
        {
            //m_HeroAnima.initialFlipX = true;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            m_HpVar.transform.localEulerAngles = new Vector3(0, 180, 0);
            //m_HpVar.Filp(true);
        }

    }

    /// <summary>
    /// Pause 될때 발동하는 함수
    /// </summary>
    public void On_Pause()
    {
        m_MonAnima.timeScale = 0;
        is_Pause = true;
    }
    /// <summary>
    /// Pause -> Play 상태일때 발동되는 함수
    /// </summary>
    public void On_Play()
    {
        m_MonAnima.timeScale = 1;
        is_Pause = false;
    }
    /// <summary>
    /// 근접 캐릭 공격을 구현하기위한 Side 포지션 
    /// </summary>
    /// <param name="_dic"></param>
    /// <returns></returns>
    public Vector3 Side_Pos(bool _dic)
    {
        Vector3 size = new Vector3(0.75f,0,0);

        if(_dic)
        {
            return Pos + size;
        }
        else
        {
            return Pos - size;
        }
    }
}