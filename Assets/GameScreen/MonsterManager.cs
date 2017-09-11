using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class MonsterManager : BaseObjManager
{
    /// <summary>
    /// Singleton Class로 사용하기위한 Instance
    /// </summary>
    private static MonsterManager m_Instance;
    public static MonsterManager Instance
    {
        get { return m_Instance; }
        set { m_Instance = value; }
    }

    #region _변수 모음_
    /// <summary>
    /// Base Monster들을 생성하기위한 Prefab
    /// </summary>
    [SerializeField]
    private GameObject m_Prefab;
    /// <summary>
    /// 생성된 모든 Base Monster를 관리하기 위한 List
    /// </summary>
    private List<BaseMonster> m_MonsterList;
    /// <summary>
    /// 현재 사용중인 Base Monster를 관리하기 위한 List
    /// </summary>
    private List<BaseMonster> m_ActiveMonster;
    /// <summary>
    /// Stage Boss Monster Object
    /// </summary>
    [SerializeField]
    private BaseBossMonster m_BossMonster;
    /// <summary>
    /// Monster들이 Target을 인식하기 위한 Hero Array
    /// </summary>
    private BaseHero[] m_Heros;
    /// <summary>
    /// 현재 스테이지의 Wave상태를 알기위한 int 형 변수
    /// </summary>
    [SerializeField]
    private int m_CrtWave;
    /// <summary>
    /// Wave 대기 시간을 체크하기위한 float 형 변수
    /// </summary>
    private float m_reTime;
    /// <summary>
    /// 스테이지의 종료 여부를 체크하기위한 변수
    /// </summary>
    public bool EndStage
    {
        set { is_EndStage = value; }
        get { return is_EndStage; }
    }
    private bool is_EndStage;
    /// <summary>
    /// MonsterStat을 전달하기 위한 임시 변수
    /// </summary>
    private MonsterStat m_Stat;
    /// <summary>
    /// Puase UI를 나타내기 위한 Obj
    /// </summary>
    [SerializeField]
    private GameObject m_PauseOBj;
    /// <summary>
    /// WaveCount를 화면에 보여주기위한 Text
    /// </summary>
    [SerializeField]
    private Text m_WaveCount;
    /// <summary>
    /// Puase 상태를 체크하기위한 변수
    /// </summary>
    public bool Pause
    {
        set { is_Pause = value; }
        get { return is_Pause; }
    }
    private bool is_Pause;
    /// <summary>
    /// Monster가 생성되는 위치들을 받는 Array
    /// </summary>
    [SerializeField]
    public SponPoint[] m_SponPoint;
    #endregion

    /// <summary>
    /// Stage Monster 정보(구)
    /// </summary>
    [SerializeField]
    private string[] m_StageWave;
    /// <summary>
    /// Stage Monster And Wave 정보(StageData) 변수
    /// </summary>
    [SerializeField]
    private StageData m_StageData;

    public override void Init()
    {
        Instance = this;
        m_MonsterList = new List<BaseMonster>(0);
        m_ActiveMonster = new List<BaseMonster>(0);
        Create_Monster();
        m_BossMonster.Init();
    }
    public override void Enter()
    {
        m_WaveCount.gameObject.SetActive(false);
        m_CrtWave = 0;
        m_reTime = 0;
        is_EndStage = false;
        is_Pause = false;
        m_PauseOBj.SetActive(false);
    }
    public override void Play()
    {
        Pause_Check();

        if (Pause || !PartyManager.Instance.Base()) return;

        #region _String Wave Mode(구)_
        //if (m_ActiveMonster.Count <= 0 && m_CrtWave > m_StageWave.Length
        //    && m_BossMonster.Active == false
        //    )
        //{
        //    PartyManager.Instance.Win();
        //    is_EndStage = true;
        //}
        //if (m_ActiveMonster.Count <= 0 && m_reTime >= 2.0f)
        //{
        //    m_WaveCount.gameObject.SetActive(false);
        //    if (m_CrtWave == m_StageWave.Length)
        //    {
        //        On_BossMon(new Vector2(4, 0));
        //        m_CrtWave++;
        //    }
        //    else if (m_CrtWave < m_StageWave.Length)
        //    {
        //        for (int i = 0; i < int.Parse(m_StageWave[m_CrtWave]); i++)
        //        {
        //            SponMonster();
        //        }
        //        m_CrtWave++;
        //    }
        //    m_reTime = 0;
        //}
        //else if(m_ActiveMonster.Count <= 0  && is_EndStage == false)
        //{
        //    WaveNumber();
        //    m_reTime += Time.deltaTime;
        //}
        #endregion

        #region _Stage Data Mode_
        // 모든웨이브 종료 & 보스몬스터가 죽었을때
        if (m_ActiveMonster.Count <= 0 && m_CrtWave >= m_StageData.m_Wave.Length
            && m_BossMonster.Active == false
            )
        {
            PartyManager.Instance.Win();
            is_EndStage = true;
        }
        // 모든몬스터가 죽고, Wave 타임이 완료되었을 때
        else if (m_ActiveMonster.Count <= 0 && m_reTime >= 2.0f)
        {
            m_WaveCount.gameObject.SetActive(false);
            // 진행 될 웨이브가 보스몬스터 Wave일 때
            if (m_CrtWave == m_StageData.m_Wave.Length-1)
            {
                for (int i = 0; i < m_StageData.m_Wave[m_CrtWave].MonID.Length; i++)
                {
                    for (int j = 0; j < m_StageData.m_Wave[m_CrtWave].Count[i]; j++)
                    {
                        On_BossMon(new Vector2(4, 0),
                               m_StageData.m_Wave[m_CrtWave].MonID[i],
                               m_StageData.m_Wave[m_CrtWave].Lv[i]);
                    }
                }
                m_CrtWave++;
            }
            // 진행 될 웨이브가 일반 Wave일 때
            else if (m_CrtWave < m_StageData.m_Wave.Length-1)
            {
                for (int i = 0; i < m_StageData.m_Wave[m_CrtWave].MonID.Length; i++)
                {
                    for(int j=0; j< m_StageData.m_Wave[m_CrtWave].Count[i]; j++)
                    {
                        SponMonster(m_StageData.m_Wave[m_CrtWave].MonID[i],
                            m_StageData.m_Wave[m_CrtWave].Lv[i]);
                    }
                }
                m_CrtWave++;
            }
            m_reTime = 0;
        }
        // 모든 몬스터가 죽고, Wave 타임이 완료되지 않았을 때
        else if (m_ActiveMonster.Count <= 0 && is_EndStage == false)
        {
            WaveNumber();
            m_reTime += Time.deltaTime;
        }
        #endregion

        // 활성화된 몬스터가 없으면 return
        if (m_ActiveMonster.Count <= 0) return;
        // 활성화 된 몬스터가 있을 시, Active Monster들을 Update 해줌
        for (int i = 0; i < m_ActiveMonster.Count; i++)
        {
            if(!m_ActiveMonster[i].Active)
            {
                m_ActiveMonster[i].Exit();
                return;
            }
            else m_ActiveMonster[i].Play();
        }
    }
    public override void Exit()
    {
        m_CrtWave = 0;
        m_reTime = 0;
        is_EndStage = false;
        for (int i = 0; i < m_MonsterList.Count; i++)
        {
            m_MonsterList[i].Exit();
        }
        is_Pause = false;
        m_PauseOBj.SetActive(false);
    }

    /// <summary>
    /// 해당 몬스터를 Active List에서 지우는 함수
    /// </summary>
    /// <param name="_mon"></param>
    public void Remove_Mon(BaseMonster _mon)
    {
        if (m_ActiveMonster.Contains(_mon))
        {
            m_ActiveMonster.Remove(_mon);
        }
    }
    /// <summary>
    /// Base Monster들을 생성하는 함수
    /// </summary>
    private void Create_Monster()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject temp = Instantiate(m_Prefab) as GameObject;
            temp.name = "Monster";
            temp.transform.parent = this.transform;

            BaseMonster _Mon = temp.GetComponent<BaseMonster>();
            _Mon.Init();
            _Mon.gameObject.SetActive(false);
            m_MonsterList.Add(_Mon);
        }
    }
    /// <summary>
    /// Monster를 해당 오브젝트에 배치하는 함수 (비활성화된 몬스터들 중 하나를 Active한다)
    /// </summary>
    /// <param name="_tile"></param>
    public void On_Monster(GameObject _tile)
    {
        for (int i = 0; i < m_MonsterList.Count; i++)
        {
            if (!m_MonsterList[i].Active)
            {
                m_MonsterList[i].Pos = _tile.transform.position;
                m_ActiveMonster.Add(m_MonsterList[i]);
                m_MonsterList[i].Enter();
                m_MonsterList[i].Set_Heros(m_Heros);
                m_MonsterList[i].Set_Monster_Stat(DataTable.Instance.Get_Monster_Data("mon001"));
                return;
            }
        }
    }
    /// <summary>
    /// 위 함수와 역할 같음 -> Monster ID를 이용해 해당 몬스터의 Stat을 가져오고 Lv를 비례하여 스텟적용
    /// </summary>
    /// <param name="_tile"></param>
    public void On_Monster(GameObject _tile,string _id, float _lv)
    {
        for (int i = 0; i < m_MonsterList.Count; i++)
        {
            if (!m_MonsterList[i].Active)
            {
                m_MonsterList[i].Pos = _tile.transform.position;
                m_ActiveMonster.Add(m_MonsterList[i]);
                m_MonsterList[i].Enter();
                m_MonsterList[i].Set_Heros(m_Heros);
                m_MonsterList[i].Set_Monster_Stat(DataTable.Instance.Get_Monster_Data(_id),_lv);
                return;
            }
        }
    }
    /// <summary>
    /// 맨위 함수와 역할 같음-> Obj 대신 Point로 대체
    /// </summary>
    /// <param name="_pos"></param>
    public void On_Monster(Vector2 _pos)
    {
        for (int i = 0; i < m_MonsterList.Count; i++)
        {
            if (!m_MonsterList[i].Active)
            {
                m_MonsterList[i].Pos = _pos;
                m_ActiveMonster.Add(m_MonsterList[i]);
                m_MonsterList[i].Enter();
                m_MonsterList[i].Set_Heros(m_Heros);
                m_MonsterList[i].Set_Monster_Stat(DataTable.Instance.Get_Monster_Data("mon001"));
                return;
            }
        }
    }
    /// <summary>
    /// Boss Monster 배치를 위한 함수
    /// </summary>
    /// <param name="_pos"></param>
    public void On_BossMon(Vector2 _pos)
    {
        if (!m_BossMonster.Active)
        {
            m_BossMonster.Pos = _pos;
            m_ActiveMonster.Add(m_BossMonster);
            m_BossMonster.Enter();
            m_BossMonster.Set_Heros(m_Heros);
            m_BossMonster.Set_Monster_Stat(DataTable.Instance.Get_Monster_Data("mon001"));
            return;
        }
    }
    /// <summary>
    /// 위 함수와 역할 같음 -> Monster ID를 이용해 해당 몬스터의 Stat을 가져오고 Lv를 비례하여 스텟적용
    /// </summary>
    /// <param name="_tile"></param>
    public void On_BossMon(Vector2 _pos, string _id, float _lv)
    {
        if (!m_BossMonster.Active)
        {
            m_BossMonster.Pos = _pos;
            m_ActiveMonster.Add(m_BossMonster);
            m_BossMonster.Enter();
            m_BossMonster.Set_Heros(m_Heros);
            m_BossMonster.Set_Monster_Stat(DataTable.Instance.Get_Monster_Data(_id), _lv);
            return;
        }
    }
    /// <summary>
    /// Wave 정보를 가져오는 함수 (구)
    /// </summary>
    /// <param name="_wave"></param>
    public void Set_Wave(string[] _wave) { m_StageWave = _wave; }
    /// <summary>
    /// Stage Data(Wave Data 등등)을 가져오는 함수
    /// </summary>
    /// <param name="_stage"></param>
    public void Set_StageData(StageData _stage)
    {
        m_StageData = _stage;
    }
    /// <summary>
    /// Target이 될 영웅들의 Array를 받아오는 함수
    /// </summary>
    /// <param name="_Heros"></param>
    public void Set_Heros(BaseHero[] _Heros) { m_Heros = _Heros; }

    /// <summary>
    /// Pause 상태일때 발동할 함수
    /// </summary>
    public void On_Pause()
    {
        if (is_EndStage) return;

        for (int i = 0; i < m_ActiveMonster.Count; i++)
        {
            m_ActiveMonster[i].On_Pause();
        }
        PartyManager.Instance.On_Pause();
        is_Pause = true;
        m_PauseOBj.SetActive(is_Pause);
    }
    /// <summary>
    /// Pause -> Play 상태에서 한번 호출하는 함수
    /// </summary>
    public void On_BACK()
    {
        if (is_EndStage) return;

        for (int i = 0; i < m_ActiveMonster.Count; i++)
        {
            m_ActiveMonster[i].On_Play();
        }
        PartyManager.Instance.On_BACK();
        is_Pause = false;
        m_PauseOBj.SetActive(is_Pause);
    }
    /// <summary>
    /// Puase 상태에서 마을로 돌아가면 호출될 Scene 전환 함수
    /// </summary>
    public void On_TOWN()
    {
        ProgramManager.Insatnce.Change_Scene(SceneName.TOWN);
    }

    /// <summary>
    /// 활성화된 몬스터를 반환하는 함수 (영웅들의 자동공격에 사용)
    /// </summary>
    /// <returns></returns>
    public List<BaseMonster> Set_ActivMonsterList()
    {
        return m_ActiveMonster;
    }

    /// <summary>
    /// App Pause State 시 발동되는 함수
    /// </summary>
    /// <param name="_pause"></param>
    void OnApplicationPause(bool _pause)
    {
        if (_pause)
        {
            On_Pause();
        }
        else
        {
        }
    }
    /// <summary>
    /// Pause 상태인지 Check 하는 함수(Android의 Pause Key Check)
    /// </summary>
    private void Pause_Check()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Home) ||
                Input.GetKeyDown(KeyCode.Escape) ||
                Input.GetKeyDown(KeyCode.Menu))
            {
                On_Pause();
            }
        }
    }
    /// <summary>
    /// Wave Text를 수정하는 함수
    /// </summary>
    private void WaveNumber()
    {
        for (int i = 0; i < m_SponPoint.Length; i++)
        {
            m_SponPoint[i].is_Mon = false;
        }
        m_WaveCount.gameObject.SetActive(true);

        for(int i = 0; i < m_MonsterList.Count; i++)
        {
            m_MonsterList[i].Exit();
            m_MonsterList[i].gameObject.SetActive(false);
        }
        //if (m_CrtWave == m_StageWave.Length) m_WaveCount.text = string.Format("Boss Wave");
        //else m_WaveCount.text = string.Format("Wave {0}/{1}", m_CrtWave + 1, m_StageWave.Length + 1);

        if (m_CrtWave == m_StageData.m_Wave.Length) m_WaveCount.text = string.Format("Boss Wave");
        else m_WaveCount.text = string.Format("Wave {0}/{1}", m_CrtWave + 1, m_StageData.m_Wave.Length);


    }
    /// <summary>
    /// Monster Spon 위치를 랜덤으로 설정하는 함수
    /// </summary>
    private void SponMonster()
    {
        bool _spon = false;
        int Ran;
        while (!_spon)
        {
            Ran = UnityEngine.Random.Range(0, 8);
            if(!m_SponPoint[Ran].is_Mon)
            {
                On_Monster(m_SponPoint[Ran].gameObject);
                m_SponPoint[Ran].is_Mon = true;
                _spon = true;
            }
        }
    }
    /// <summary>
    /// Monster Spon 위치를 랜덤으로 설정하는 함수(Monster Stat과 Lv 설정)
    /// </summary>
    /// <param name="_Id"></param>
    /// <param name="_lv"></param>
    private void SponMonster(string _Id, float _lv)
    {
        bool _spon = false;
        int Ran;
        while (!_spon)
        {
            Ran = UnityEngine.Random.Range(0, 8);
            if (!m_SponPoint[Ran].is_Mon)
            {
                On_Monster(m_SponPoint[Ran].gameObject);
                m_SponPoint[Ran].is_Mon = true;
                _spon = true;
            }
        }
    }
    /// <summary>
    /// 전투 재시작 시 현재 오브젝트의 초기화 함수
    /// </summary>
    public void ReStart()
    {
        Exit();
        Enter();
    }
}