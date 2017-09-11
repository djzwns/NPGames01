using UnityEngine;
using System.Collections;
using Spine.Unity;

/// <summary>
/// Hero의 Stat 데이터 및 Hero DB를 받기위한 Class
/// (변수에 대한 자세한 것은 문서참고)
/// </summary>
[SerializeField]
public class HeroStat
{
    public string ID;
    public string IllustID;
    public string CharName;
    public string Title;
    public string Species;
    public string sex;
    public string Class;
    public string SubClass;
    public string Weapon;
    public string organization;
    public float FirstAtt;
    public float FirstDef;
    public float FirstHP;
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
    public string Skill1;
    public string Skill2;
    public string Skill3;
    public float CharLv;
    public float CharExp;
    public float SillingPrice;
    public float RubyPrice;
    public string Model_FileName;
    public bool Has;

    public void Set(HeroStat _Temp)
    {
        ID = _Temp.ID;
        IllustID = _Temp.IllustID;
        CharName = _Temp.CharName;
        Title = _Temp.Title;
        Species = _Temp.Species;
        sex = _Temp.sex;
        Class = _Temp.Class;
        SubClass = _Temp.SubClass;
        Weapon = _Temp.Weapon;
        organization = _Temp.organization;
        FirstAtt = _Temp.FirstAtt;
        FirstDef = _Temp.FirstDef;
        FirstHP = _Temp.FirstHP;
        AttPerLV = _Temp.AttPerLV;
        DefPerLV = _Temp.DefPerLV;
        HPPERLV = _Temp.HPPERLV;
        MoveSpeed = _Temp.MoveSpeed;
        AttSpeed = _Temp.AttSpeed;
        AttRange = _Temp.AttRange;
        NoticeRange = _Temp.NoticeRange;
        Skill1 = _Temp.Skill1;
        Skill2 = _Temp.Skill2;
        Skill3 = _Temp.Skill3;
        CharLv = _Temp.CharLv;
        CharExp = _Temp.CharExp;
        Model_FileName = _Temp.Model_FileName;
        SillingPrice = _Temp.SillingPrice;
        RubyPrice = _Temp.RubyPrice;
        Has = _Temp.Has;
    }
}

/// <summary>
/// 실재 Hero의 Level과 장비에 비례한 전투에 필요한 현재스텟관리를위한 Struct
/// </summary>
public struct HeroAttStat
{
    public int Level;
    public float AttPoint;
    public float DefPoint;
    public float MaxHP;
    public float NowHP;
}

/// <summary>
/// BaseHero Class -> BaseObj_2D를 상속받는 BaseHero 클래스
/// </summary>
public class BaseHero : BaseObj_2D
{
    #region _변수 모음_
    // 영웅 상태 체크를 위한 Enum
    protected enum HEROMODE { IDLE=0, ATTACK, MOVE, ATTACKMOVE,WIN }
    // Spine Animation 관리를 위한 변수
    [SerializeField]
    protected SkeletonAnimation m_HeroAnima;
    // 인게임 내에서의 초기 위치값 설정을 위한 변수
    protected Vector3 Base_Pos;
    // Monster나 Hero가아닌 필드를찍었을때 Position을 받기위한 변수
    protected Vector2 m_TargetPos;
    // 원거리 공격형 유닛일때 생성할 탄환 Obj
    [SerializeField]
    protected GameObject m_Bullet;
    // Hero의 현재 상태 Check를 위한 변수
    [SerializeField]
    protected HEROMODE m_Mode;
    // 영웅이 가져야할 Stat 변수
    protected HeroStat m_Stat = new HeroStat();
    // 영웅의 레벨과 장비에 따라 실제 적용될 Stat 변수
    protected HeroAttStat m_AttStat;
    // 목표 위치 및 목표 Monster 와의 거리를 측정하기위한 변수
    [SerializeField]
    protected float m_Distans;
    // HP UI 변수
    [SerializeField]
    protected UI_Bar m_HpVar;
    // Target이 된 Monster 변수
    protected BaseMonster m_TargetMon;
    // Target이 된 Hero 변수
    protected BaseHero m_TargetMon1;
    // 현재 Hero의 Dead상태 변수
    protected bool is_Active;
    // Attack Time을 체크하기위한 변수
    protected float m_AS_Time;
    public bool Active
    {
        get { return is_Active; }
        set { is_Active = value; }
    }
    // MaxHp  체크를 위한 변수
    public float MaxHp;
    // NowHp  체크를 위한 변수
    public float NowHp;
    // Pause 상태를 체크하기 위한 변수
    protected bool is_Pause;
    // Win 상태를 체크하기 위한 변수
    public bool is_Win;
    // 현재 애니메이션의 상태를 체크하기위한 변수
    protected Spine.TrackEntry m_Complete;
    // Sprite의 레이어 관리를 위한 변수
    protected MeshRenderer m_sortingOrder;
    // 현재 목표의 Filp 방향을 체크하기 위한 변수
    protected bool is_Distan;
#endregion

    public override void Init()
    {
        m_sortingOrder = m_HeroAnima.GetComponent<MeshRenderer>();
        base.Init();
    }

    public override void Enter()
    {
        if (m_Stat == null)
        {
            // Stat 정보가 없을땐 비활성화
            this.gameObject.SetActive(false);
            Active = false;
            Debug.Log("NULL");
        }
        else
        {
            // Stat 정보가 있을때 초기화
            m_Mode = HEROMODE.IDLE;
            m_Distans = Mathf.Infinity;
            m_AS_Time = 0;
            Active = true;
            MaxHp = m_Stat.FirstHP;
            m_HpVar.Set_MaxValue(MaxHp);
            m_HpVar.Set_NowValue(m_Stat.FirstHP);
            m_HpVar.Play();
            this.gameObject.SetActive(true);
            m_HeroAnima.loop = true;
            m_HeroAnima.timeScale = 1;
            is_Pause = false;
            is_Win = false;
        }
        m_HpVar.Play();
    }

    public override void Play()
    {
        // Pause 체크
        if (is_Pause) return;

        // HpUI 관리
        m_HpVar.Set_MaxValue(MaxHp);
        m_HpVar.Set_NowValue(m_Stat.FirstHP);
        m_HpVar.Play();

        // 현재 Hero 상태를 체크하여 
        switch (m_Mode)
        {
            case HEROMODE.IDLE: Search_Target(); break;
            case HEROMODE.ATTACK: Mode_Attack(); break;
            case HEROMODE.MOVE: Mode_Move(); break;
            case HEROMODE.ATTACKMOVE: Mode_AttackMove(); break;
            case HEROMODE.WIN: Mode_Win(); break;
        }
    }

    public override void Exit()
    {
        m_Mode = HEROMODE.IDLE;
        m_Distans = Mathf.Infinity;
        Active = false;
    }

    /// <summary>
    /// Move 상태일때 적용되는 함수
    /// </summary>
    private void Mode_Move()
    {
        m_AS_Time = 0;
        if (m_Distans <= 0.5f)
        {
            m_HeroAnima.AnimationName = "stand_eye";
            m_HeroAnima.loop = true;
            m_Mode = HEROMODE.IDLE;
            m_Distans = Mathf.Infinity;
        }

        Vector3 dir = (Vector3)m_TargetPos - Pos;
        m_Distans = dir.sqrMagnitude;
        Pos += dir.normalized * Time.fixedDeltaTime * m_Stat.MoveSpeed;
    }
    /// <summary>
    /// Attack 상태일때 함수
    /// </summary>
    private void Mode_Attack()
    {
        if (!m_TargetMon.Active)
        {
            m_AS_Time = 0;
            m_TargetMon = null;
            m_Distans = Mathf.Infinity;
            m_Mode = HEROMODE.IDLE;
        }
        m_Complete = m_HeroAnima.AnimationState.GetCurrent(0);
        Debug.Log(m_Complete.AnimationLast);


        if (m_Complete.IsComplete)
        {
            if (m_TargetMon!=null && !m_TargetMon.Active)
            {
                m_HeroAnima.loop = true;
                m_HeroAnima.AnimationName = "stand";
                m_HeroAnima.timeScale = 1;
                if (m_Stat.AttRange <= 1.5f) m_Mode = HEROMODE.ATTACKMOVE;
            }
            else 
            {
                m_HeroAnima.loop = true;
                m_HeroAnima.AnimationName = "stand";
                m_HeroAnima.timeScale = 1;
                if(m_TargetMon!= null)
                {
                    if (m_Stat.AttRange <= 1.5f)
                    {
                        m_TargetMon.Damege(m_AttStat.AttPoint, this);
                        m_Mode = HEROMODE.ATTACKMOVE;
                    }
                    else
                    {
                        Debug.Log(m_Complete.TrackIndex);
                        Create_Bullet();
                    }
                }
            }
            m_AS_Time = 0;
        }
        else
        {
            if(m_TargetMon != null) Filp_X(m_TargetMon.Pos);
            m_HeroAnima.loop = false;
            m_HeroAnima.AnimationName = "attack";
            m_AS_Time += Time.fixedDeltaTime;
        }
    }
    /// <summary>
    /// Attack Move 상태일때 함수
    /// </summary>
    private void Mode_AttackMove()
    {
        m_HeroAnima.loop = true;
        m_HeroAnima.AnimationName = "working";

        #region _ 근접캐릭작업 (주석)_
        //if (m_Stat.AttRange <= 1.5f)
        //{
        //    if (m_Distans <= m_Stat.AttRange &&
        //        m_Distans <= 0.02f &&
        //        Mathf.Abs(m_TargetMon.Side_Pos(is_Distan).y - Pos.y) <= 0.01f)
        //    {
        //        m_Mode = HEROMODE.ATTACK;
        //        m_Distans = Mathf.Infinity;
        //        return;
        //    }

        //    Vector3 dir = m_TargetMon.Pos - Pos;
        //    //Filp_X(m_TargetMon.Side_Pos(is_Distan));
        //    m_Distans = dir.sqrMagnitude;
        //    Pos += dir.normalized * Time.fixedDeltaTime * m_Stat.MoveSpeed;
        //}
        //else
        #endregion

        if (m_Stat.AttRange <= 1.5f)
        {
            Vector3 dir;
            dir = m_TargetMon.Pos - Pos;
            m_Distans = dir.sqrMagnitude;

            if (m_Distans <= m_Stat.AttRange)
            {
                if (Mathf.Abs(m_TargetMon.Pos.y - Pos.y) <= 0.01f)
                {
                    m_Mode = HEROMODE.ATTACK; 
                }
                else if(Mathf.Abs(m_TargetMon.Pos.y - Pos.y) <= 0.04f &&
                    Mathf.Abs(m_TargetMon.Pos.y - Pos.y) >= 0.01f)
                {
                    Pos = new Vector3(Pos.x, m_TargetMon.Pos.y);
                    //Debug.Log("이동");
                    Filp_X(m_TargetMon.Pos);
                }
                else
                {
                    Vector3 temp = new Vector3(0, dir.normalized.y * 2.1f);
                    Pos += temp * Time.fixedDeltaTime * m_Stat.MoveSpeed;
                    Filp_X(m_TargetMon.Pos);
                    //Debug.Log(Mathf.Abs(m_TargetMon.Pos.y - Pos.y));
                }
            }
            else
            {
                Pos += dir.normalized * Time.fixedDeltaTime * m_Stat.MoveSpeed;
                Filp_X(m_TargetMon.Pos);
            }
        }
        else 
        {
            Vector3 dir;
            dir = m_TargetMon.Pos - Pos;
            m_Distans = dir.sqrMagnitude;
            if (m_Distans <= m_Stat.AttRange)
            {
                m_Mode = HEROMODE.ATTACK;
                return;
            }
            else
            {
                Pos += dir.normalized * Time.fixedDeltaTime * m_Stat.MoveSpeed;
                Filp_X(m_TargetMon.Pos);
            }
        }
    }
    /// <summary>
    /// Win 상태일때 함수 -> 추후 승리포즈 때문에 삽입
    /// </summary>
    private void Mode_Win()
    {
        m_Complete = m_HeroAnima.AnimationState.GetCurrent(0);
        if(m_Complete.IsComplete)
        {
            is_Win = true;
        }
    }


    /// <summary>
    /// Hero가 데미지를 받는 함수
    /// </summary>
    /// <param name="_Damege"> 적의 Damege </param>
    public void Damege(float _Damege)
    { 
        if (_Damege > m_AttStat.DefPoint)
        {
            m_Stat.FirstHP -= (_Damege - m_AttStat.DefPoint);
        }
        else m_Stat.FirstHP -= 1;

        if (m_Stat.FirstHP <= 0)
        {
            Active = false;
            this.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Target을 설정하는 함수(Monster)
    /// </summary>
    /// <param name="_TargetMon"> Target Monster </param>
    public void Set_Target(BaseMonster _TargetMon)
    {
        m_TargetMon = _TargetMon;
        m_Mode = HEROMODE.ATTACKMOVE;
        m_Distans = Mathf.Infinity;

        Filp_X(m_TargetMon.Pos);
        m_HeroAnima.loop = true;
        m_HeroAnima.AnimationName = "working";
    }
    /// <summary>
    /// Target을 설정하는 함수 (Hero)
    /// </summary>
    /// <param name="_TargetMon"></param>
    public void Set_Target(BaseHero _TargetMon)
    {
        m_TargetMon1 = _TargetMon;
        m_Mode = HEROMODE.ATTACKMOVE;
        m_Distans = Mathf.Infinity;

        Filp_X(m_TargetMon1.Pos);
        m_HeroAnima.loop = true;
        m_HeroAnima.AnimationName = "working";
    }
    /// <summary>
    /// Point를 지정하는 함수 ( 이동시 )
    /// </summary>
    /// <param name="_Point"></param>
    public void Set_Point(Vector2 _Point)
    {
        m_TargetPos = _Point;
        m_Mode = HEROMODE.MOVE;
        m_Distans = ((Vector3)m_TargetPos - Pos).sqrMagnitude;

        Filp_X(m_TargetPos);
        m_HeroAnima.loop = true;
        m_HeroAnima.AnimationName = "working";
    }
    /// <summary>
    /// 기본 위치로 이동하는 함수 ( 이동완료시 true 반환)
    /// </summary>
    /// <returns></returns>
    public bool Get_Base()
    {
        m_TargetPos = Base_Pos;
        if (((Base_Pos - Pos).sqrMagnitude) <= 0.05f)
        {
            return true;
        }
        else
        {
            Set_Point(m_TargetPos);
            return false;
        }
    }
    /// <summary>
    /// Stat 정보를 받아오는 함수
    /// </summary>
    /// <param name="_stat"></param>
    public void Set_Hero_Stat(HeroStat _stat)
    {
        if (_stat == null)
        {
            m_Stat = null;
        }
        else
        {
            m_Stat.Set(_stat);
            m_AttStat.MaxHP = m_Stat.FirstHP + ((m_AttStat.Level - 1) * m_Stat.HPPERLV);
            m_AttStat.AttPoint = m_Stat.FirstAtt + ((m_AttStat.Level - 1) * m_Stat.AttPerLV);
            m_AttStat.DefPoint = m_Stat.FirstDef + ((m_AttStat.Level - 1) * m_Stat.DefPerLV);
            m_AttStat.NowHP = m_AttStat.MaxHP;
        }
    }
    /// <summary>
    /// 스킬_1을 사용하는 가상 함수
    /// </summary>
    public virtual void  Skill_One()
    {
        if (MonsterManager.Instance.EndStage) return;
        Debug.Log(this.gameObject.name + " Skill_One Active");
    }
    /// <summary>
    /// 스킬_2을 사용하는 가상 함수
    /// </summary>
    public virtual void Skill_Two()
    {
        if (MonsterManager.Instance.EndStage) return;
        Debug.Log(this.gameObject.name + " Skill_Two Active");
    }
    /// <summary>
    /// 자동으로 인식범위내의 몬스터를 찾는 함수
    /// </summary>
    private void Search_Target()
    {
        m_HeroAnima.loop = true;
        m_HeroAnima.AnimationName = "stand";

        m_Distans = Mathf.Infinity;
        float Min = Mathf.Infinity;
        for (int i = 0; i < MonsterManager.Instance.Set_ActivMonsterList().Count; i++)
        {
            if (Min > Vector3.Distance(MonsterManager.Instance.Set_ActivMonsterList()[i].Pos, Pos) 
                && MonsterManager.Instance.Set_ActivMonsterList()[i].Active)
            {
                Min = Vector3.Distance(MonsterManager.Instance.Set_ActivMonsterList()[i].Pos, Pos);
                if(Min <= m_Stat.NoticeRange &&
                    MonsterManager.Instance.Set_ActivMonsterList()[i].Pos.x < 6.25f &&
                    MonsterManager.Instance.Set_ActivMonsterList()[i].Pos.x > -6.25f)
                {
                    if(m_Stat.AttRange<=1.5f)
                    {
                        if(!is_Distan &&
                            (Pos.x - MonsterManager.Instance.Set_ActivMonsterList()[i].Pos.x) <= 0f)
                        {
                            m_TargetMon = MonsterManager.Instance.Set_ActivMonsterList()[i];
                            m_Mode = HEROMODE.ATTACKMOVE;

                        }
                        else if(is_Distan &&
                            (Pos.x - MonsterManager.Instance.Set_ActivMonsterList()[i].Pos.x) >= 0f)
                        {
                            m_TargetMon = MonsterManager.Instance.Set_ActivMonsterList()[i];
                            m_Mode = HEROMODE.ATTACKMOVE;
                        }
                    }
                    else
                    {
                        m_TargetMon = MonsterManager.Instance.Set_ActivMonsterList()[i];
                        m_Mode = HEROMODE.ATTACKMOVE;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Base Pos를 설정하는 함수
    /// </summary>
    /// <param name="_Point"></param>
    public void Set_BasePos(Vector3 _Point)
    {
        Base_Pos = _Point;
    }
    /// <summary>
    /// Target or Point를 확인해 Filp을 바꾸는 함수
    /// </summary>
    /// <param name="_pos"></param>
    public void Filp_X(Vector3 _pos)
    {
        if ((Pos.x < _pos.x))
        {
            //m_HeroAnima.initialFlipX = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            //m_HpVar.Filp(false);
            is_Distan = false;
        }
        else
        {
            //m_HeroAnima.initialFlipX = true;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //m_HpVar.Filp(true);
            is_Distan = true;
        }
        
    }

    /// <summary>
    /// Pause 될때 발동하는 함수
    /// </summary>
    public void On_Pause()
    {
        m_HeroAnima.timeScale = 0;
        is_Pause = true;
    }
    /// <summary>
    /// Pause -> Play 상태일때 발동되는 함수
    /// </summary>
    public void On_Play()
    {
        m_HeroAnima.timeScale = 1;
        is_Pause = false;
    }
    /// <summary>
    /// Win 상태일때 Animation 설정을 하는 함수
    /// </summary>
    public void Win_Pose()
    {
        m_Mode = HEROMODE.WIN;
        m_HeroAnima.timeScale = 1;
        m_HeroAnima.AnimationName = "stand_eye";
        m_HeroAnima.loop = false;
    }
    /// <summary>
    /// Hero의 Win Animation이 종료되었는지 확인하는 함수
    /// </summary>
    /// <returns></returns>
    public bool Get_Win()
    {
        return is_Win;
    }
    /// <summary>
    /// Hero의 laryer을 관리하기 위한 함수
    /// </summary>
    /// <param name="num"></param>
    public void Pick(int num)
    {
        m_sortingOrder.sortingOrder = num;
    }
    /// <summary>
    /// 현재 영웅의 Stat을 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public HeroAttStat Get_Stat()
    {
        return m_AttStat;
    }
    /// <summary>
    /// 원거리 공격시 원거리 오브젝트를 생성하는 함수
    /// </summary>
    private void Create_Bullet()
    {
        GameObject temp = GameObject.Instantiate(m_Bullet) as GameObject;
        temp.transform.parent = this.transform;
        if (is_Distan)
        {
            temp.transform.position = Pos - new Vector3(0.4f, 0, 0);
        }
        else temp.transform.position = Pos + new Vector3(0.4f, 0, 0);

        Bullet _blt = temp.AddComponent<Bullet>();
        _blt.Init(this);
        _blt.Set_Target(m_TargetMon);
    }
}