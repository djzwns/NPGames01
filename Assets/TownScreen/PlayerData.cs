using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Party의 정보를 가지고있는 Class
/// </summary>
[Serializable]
public class Party_Index
{
    public string[] m_HeroCode;
    public Party_Index() { m_HeroCode = new string[3]; }
    public Party_Index(string _code1, string _code2, string _code3)
    {
        m_HeroCode = new string[3];
        m_HeroCode[0] = _code1;
        m_HeroCode[1] = _code2;
        m_HeroCode[2] = _code3;
    }
}
/// <summary>
/// Hero의 skill Data중 저장되어야하는 Data Class
/// </summary>
[Serializable]
public class SaveSkillData
{
    public string Index;
    public string Level;
    public string[] TP;

    public SaveSkillData()
    {
        TP = new string[6];
    }
    public SaveSkillData(string _skillDataList)
    {
        string[] temp = _skillDataList.Split('^');
        TP = new string[6];
        Index = temp[0];
        Level = temp[1];
        string[] temp2 = temp[2].Split('-');
        for (int i = 0; i < TP.Length; i++)
        {
            TP[i] = temp2[i];
        }
    }
    public SaveSkillData(string _Index, string _level, string _tp)
    {
        TP = new string[6];
        Index = _Index;
        Level = _level;
        string[] temp = _tp.Split('-');
        for (int i = 0; i < TP.Length; i++)
        {
            TP[i] = temp[i];
        }
    }
    public SaveSkillData(string _Index, string _level,
        string _tp1_1, string _tp1_2, string _tp2_1, string _tp2_2, string _tp3_1, string _tp3_2)
    {
        Index = _Index;
        Level = _level;
        TP = new string[6];
        TP[0] = _tp1_1;
        TP[1] = _tp1_2;
        TP[2] = _tp2_1;
        TP[3] = _tp2_2;
        TP[4] = _tp3_1;
        TP[5] = _tp3_2;
    }
}
/// <summary>
/// SaveHeroData 저장되어야할 영웅의 Data를 저장하는 Data Class
/// </summary>
[Serializable]
public class SaveHeroData
{
    public string Index;
    public string level;
    public string title_ID;
    public SaveSkillData Skill_1;
    public SaveSkillData Skill_2;
    public SaveSkillData Skill_3;

    public SaveHeroData() { }
    public SaveHeroData(string _ListData)
    {
        string[] temp = _ListData.Split(',');
        Index = temp[0];
        level = temp[1];
        title_ID = temp[2];
        Skill_1 = new SaveSkillData(temp[3]);
        Skill_1 = new SaveSkillData(temp[4]);
        Skill_1 = new SaveSkillData(temp[5]);
    }
}

/// <summary>
/// 저장되어야하는 Player Data Class
/// </summary>
[Serializable]
public class Player_Data
{
    /// <summary>
    /// 플레이어의 고유 번호
    /// </summary>
    public int m_PlayerStaticNumber;
    /// <summary>
    /// 플레이어의 닉네임
    /// </summary>
    public string m_Name;
    /// <summary>
    /// 플레이어의 레벨
    /// </summary>
    public float m_PlayerLevel;
    /// <summary>
    /// 플레이어의 현재 경험치
    /// </summary>
    public float m_NowPlayerExperience;
    /// <summary>
    /// 플레이어의 보유 게임머니
    /// </summary>
    public int m_GameMoney;
    /// <summary>
    /// 플레이어의 보유 포인트
    /// </summary>
    public int m_Point;
    /// <summary>
    /// 플레이어가 보유중인 모든 캐쉬
    /// </summary>
    public int m_AllCash;
    /// <summary>
    /// 플레이어가 보유중인 무료 Cash 재화
    /// </summary>
    public int m_FreeCash;
    /// <summary>
    /// 플레이어가 보유중인 구매한 Cash 재화
    /// </summary>
    public int m_UserCash;
    /// <summary>
    /// 플레이어가 가지고있는 영웅의 수
    /// </summary>
    public int m_HasHeroCount;
    /// <summary>
    /// 현재 사용하려고하는 Party의 번호
    /// </summary>
    public int Party_Index;
    /// <summary>
    /// 플레이어의 Party 정보 Array
    /// </summary>
    public Party_Index[] PartyMem_Index;
    /// <summary>
    /// 플레이어가 보유중인 영웅들의 정보(영웅의정보 & 스킬의 강화정보 등등)의 List
    /// </summary>
    public List<SaveHeroData> Char_CodeList;
    /// <summary>
    /// 보유중인 영웅의 Index Code Array
    /// </summary>
    public string[] m_Has_Hero_Code;


    /// <summary>
    /// 생성자  1
    /// </summary>
    public Player_Data() { Char_CodeList = new List<SaveHeroData>(0); }
    /// <summary>
    /// 생성자 2
    /// </summary>
    /// <param name="_num"></param>
    /// <param name="_name"></param>
    /// <param name="_level"></param>
    /// <param name="_expericence"></param>
    /// <param name="_money"></param>
    /// <param name="_point"></param>
    /// <param name="_FreeCash"></param>
    /// <param name="_UserCash"></param>
    public Player_Data(int _num, string _name, float _level, float _expericence,
        int _money, int _point, int _FreeCash, int _UserCash)
    {
        m_PlayerStaticNumber = _num;
        m_Name = _name;
        m_PlayerLevel = _level;
        m_NowPlayerExperience = _expericence;
        m_GameMoney = _money;
        m_Point = _point;
        m_AllCash = _FreeCash + _UserCash;
        m_FreeCash = _FreeCash;
        m_UserCash = _UserCash;

        PartyMem_Index = new global::Party_Index[4];
        Char_CodeList = new List<SaveHeroData>(0);
    }
    /// <summary>
    /// Set 함수
    /// </summary>
    /// <param name="_num"></param>
    /// <param name="_name"></param>
    /// <param name="_level"></param>
    /// <param name="_expericence"></param>
    /// <param name="_money"></param>
    /// <param name="_point"></param>
    /// <param name="_FreeCash"></param>
    /// <param name="_UserCash"></param>
    public void Set(int _num,string _name,float _level, float _expericence,
        int _money, int _point, int _FreeCash, int _UserCash)
    {
        m_PlayerStaticNumber = _num;
        m_Name = _name;
        m_PlayerLevel = _level;
        m_NowPlayerExperience = _expericence;
        m_GameMoney = _money;
        m_Point = _point;
        m_AllCash = _FreeCash + _UserCash;
        m_FreeCash = _FreeCash;
        m_UserCash = _UserCash;
    }
}

/// <summary>
/// PlayerData를 관리하고 불러오는 Class
/// </summary>
public class PlayerData : BaseObjManager
{
    /// <summary>
    /// Singleton Class 생성을 위한 변수들
    /// </summary>
    public static GameObject Obj;
    public static PlayerData m_Instance;
    public static PlayerData Instance
    {
        set { m_Instance = value; }
        get
        {
            if(m_Instance == null)
            {
                Obj = new GameObject("PlayerData");
                m_Instance = Obj.AddComponent<PlayerData>();
                //m_Instance.Init();
            }
            return m_Instance;
        }
    }

    /// <summary>
    /// Player Data 정보
    /// </summary>
    [SerializeField]
    private Player_Data m_Player;

    public override void Init()
    {
        m_Player = new Player_Data();
        Load_Player_Data();
    }
    public override void Enter()
    {
    }
    public override void Play()
    {
    }
    public override void Exit()
    {
    }

    /// <summary>
    /// Player Data 확인을 위한 변수
    /// </summary>
    /// <returns></returns>
    public Player_Data Get_Data() { return m_Player; }
    /// <summary>
    /// 영웅 구매시 호출하여 Player Data 구매한 영웅을 넣어주는 함수
    /// </summary>
    /// <param name="_Key"></param>
    public void Add_Hero(HeroStat _Key)
    {
        SaveHeroData _temp = new SaveHeroData();
        _temp.Index = _Key.ID;

        m_Player.Char_CodeList.Add(_temp);
        string[] temp = new string[m_Player.m_Has_Hero_Code.Length+1];
        for(int i = 0; i < m_Player.m_Has_Hero_Code.Length; i++)
        {
            temp[i] = m_Player.m_Has_Hero_Code[i];
        }
        temp[m_Player.m_Has_Hero_Code.Length] = _Key.ID;
        m_Player.m_Has_Hero_Code = temp;
    } 
    /// <summary>
    /// 현재 Party의 번호를 변경하는 함수
    /// </summary>
    /// <param name="_Party"></param>
    public void Set_Party_Index(int _Party)
    {
        m_Player.Party_Index = _Party;
    }
    /// <summary>
    /// 현재 선택한 파티의 맴버를 변경하는 함수
    /// </summary>
    /// <param name="_Mem"></param>
    public void Set_Party_Mem(Party_Index _Mem)
    {

        Party_Index temp = new Party_Index(_Mem.m_HeroCode[0], _Mem.m_HeroCode[1], _Mem.m_HeroCode[2]);
        m_Player.PartyMem_Index[m_Player.Party_Index] = temp;
    }
    /// <summary>
    /// 현재 선택한 파티의 Hero Index Code를 반환하는 함수(int 값에 따라 반환)
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public string Get_Party_Hero(int i)
    {
        return m_Player.PartyMem_Index[m_Player.Party_Index].m_HeroCode[i];
    }

    /// <summary>
    /// Player Data를 Load하는 함수(추후 GPGS를 사용한 저장을 하였을때 그쪽에서 Data를 불러와야함)
    /// </summary>
    public void Load_Player_Data()
    {
        TextAsset m_Text = Resources.Load("PlayerData", typeof(TextAsset)) as TextAsset;
        string[] Line_arr = m_Text.text.Split('\t');
        
        m_Player.m_PlayerStaticNumber = int.Parse(Line_arr[0]);
        m_Player.m_Name = Line_arr[1];
        m_Player.m_PlayerLevel = int.Parse(Line_arr[2]);
        m_Player.m_NowPlayerExperience = float.Parse(Line_arr[3]);
        m_Player.m_GameMoney = int.Parse(Line_arr[4]);
        m_Player.m_Point = int.Parse(Line_arr[5]);
        m_Player.m_FreeCash = int.Parse(Line_arr[6]);
        m_Player.m_UserCash = int.Parse(Line_arr[7]);
        m_Player.m_AllCash = m_Player.m_FreeCash + m_Player.m_UserCash;
        m_Player.Party_Index = int.Parse(Line_arr[9]);

        string[] Has_Temp = Line_arr[8].Split('`');
        m_Player.m_Has_Hero_Code = new string[Has_Temp.Length];
        for (int i = 0; i < Has_Temp.Length; i++)
        {
            SaveHeroData temp = new SaveHeroData();
            temp.Index = Has_Temp[i];
            m_Player.m_Has_Hero_Code[i] = Has_Temp[i];
            m_Player.Char_CodeList.Add(temp);
        }
        m_Player.m_HasHeroCount = m_Player.Char_CodeList.Count;

        m_Player.PartyMem_Index = new Party_Index[4];
        for (int i = 0; i < 4; i++)
        {
            if(Line_arr[10+i] == "Null")
            {
                m_Player.PartyMem_Index[i] = new Party_Index();
            }
            else
            {
                string[] temp = Line_arr[10 + i].Split('`');
                if(temp.Length == 3)
                {
                    m_Player.PartyMem_Index[i] = new Party_Index(temp[0], temp[1], temp[2]);
                }
                else m_Player.PartyMem_Index[i] = new Party_Index();
            }
        }
    }
    public void Cash_Data()
    {
        m_Player.m_AllCash = m_Player.m_FreeCash + m_Player.m_UserCash;
    }
    /// <summary>
    /// 보유중인 Hero들의 Code Array를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public string[] Get_Has_Hero_Code()
    {
        return m_Player.m_Has_Hero_Code;
    }
}