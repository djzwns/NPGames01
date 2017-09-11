using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Game DB class
/// </summary>
public class DataTable : MonoBehaviour
{
    /// <summary>
    /// Data Table Class Singleton Class Instance
    /// </summary>
    public static GameObject Obj;
    public static DataTable m_Instance;
    public static DataTable Instance
    {
        set { m_Instance = value; }
        get
        {
            if (m_Instance == null)
            {
                Obj = new GameObject("DataTable");
                m_Instance = Obj.AddComponent<DataTable>();
                //m_Instance.Init();
            }
            return m_Instance;
        }
    }


    /// <summary>
    /// Hero DB Table 
    /// </summary>
    [SerializeField]
    private Dictionary<string, HeroStat> m_HeroTable;
    /// <summary>
    /// Tem Hero Db Table
    /// </summary>
    private List<HeroStat> m_HeroTable2;
    /// <summary>
    /// Monster Db Table
    /// </summary>
    private Dictionary<string, MonsterStat> m_MonsterTable;

    /// <summary>
    /// Temp Data 
    /// </summary>
    private HeroStat m_tempData;
    private MonsterStat m_tempData_Mon;

    public void Init()
    {
        Instance = this;
        m_HeroTable = new Dictionary<string, HeroStat>(0);
        m_MonsterTable = new Dictionary<string, MonsterStat>(0);
        m_HeroTable2 = new List<HeroStat>(0);
        Instance.Load_Hero_Data();
        Instance.Load_Monster_Data();
    }
    /// <summary>
    /// Db Load
    /// </summary>
    private void Load_Hero_Data()
    {
        TextAsset m_Text = Resources.Load("HeroDataTable",typeof(TextAsset)) as TextAsset;
        string[] Line_arr = m_Text.text.Split('\n');

        for (int i = 0; i < (int)Line_arr.Length; i++)
        {
            string[] Herodata_arr = Line_arr[i].Split('\t');
            HeroStat _data = new HeroStat();
            _data.ID = Herodata_arr[0].ToString();
            _data.IllustID = Herodata_arr[1].ToString();
            _data.CharName = Herodata_arr[2].ToString();
            _data.Title = Herodata_arr[3].ToString();
            _data.Species = Herodata_arr[4].ToString();
            _data.sex = Herodata_arr[5].ToString();
            _data.Class = Herodata_arr[6].ToString();
            _data.SubClass = Herodata_arr[7].ToString();
            _data.Weapon = Herodata_arr[8].ToString();
            _data.organization = Herodata_arr[9].ToString();
            _data.FirstAtt = float.Parse(Herodata_arr[10].ToString());
            _data.FirstDef = float.Parse(Herodata_arr[11].ToString());
            _data.FirstHP = float.Parse(Herodata_arr[12].ToString());
            _data.AttPerLV = float.Parse(Herodata_arr[13].ToString());
            _data.DefPerLV = float.Parse(Herodata_arr[14].ToString());
            _data.HPPERLV = float.Parse(Herodata_arr[15].ToString());
            _data.FurtherDmg = float.Parse(Herodata_arr[16].ToString());
            _data.DmgOffset = float.Parse(Herodata_arr[17].ToString());
            _data.Cooldown = float.Parse(Herodata_arr[18].ToString());
            _data.MoveSpeed = float.Parse(Herodata_arr[19].ToString());
            _data.AttSpeed = float.Parse(Herodata_arr[20].ToString());
            _data.AttRange = float.Parse(Herodata_arr[21].ToString());
            _data.NoticeRange = float.Parse(Herodata_arr[22].ToString());
            _data.Skill1 = Herodata_arr[23].ToString();
            _data.Skill2 = Herodata_arr[24].ToString();
            _data.Skill3 = Herodata_arr[25].ToString();
            _data.CharLv = float.Parse(Herodata_arr[26].ToString());
            _data.CharExp = float.Parse(Herodata_arr[27].ToString());
            _data.SillingPrice = float.Parse(Herodata_arr[28].ToString());
            _data.RubyPrice = float.Parse(Herodata_arr[29].ToString());

            m_HeroTable.Add(_data.ID, _data);
            m_HeroTable2.Add(_data);
        }
    }
    private void Load_Monster_Data()
    {
        TextAsset m_Text = Resources.Load("MonsterDataTable", typeof(TextAsset)) as TextAsset;
        string[] Line_arr = m_Text.text.Split('\n');

        for (int i = 0; i < (int)Line_arr.Length; i++)
        {
            string[] Mondata_arr = Line_arr[i].Split('\t');
            MonsterStat _data = new MonsterStat();
            _data.MonID = Mondata_arr[0];
            _data.IllustID = Mondata_arr[1];
            _data.MonsterName = Mondata_arr[2];
            _data.Race = float.Parse(Mondata_arr[3]);
            _data.SubClass = float.Parse(Mondata_arr[4]);
            _data.Grade = float.Parse(Mondata_arr[5]);
            _data.Position = float.Parse(Mondata_arr[6]);
            _data.Frenz = float.Parse(Mondata_arr[7]);
            _data.MinAttPower = float.Parse(Mondata_arr[8]);
            _data.MinDefendPower = float.Parse(Mondata_arr[9]);
            _data.MinHP = float.Parse(Mondata_arr[10]);
            _data.AttPerLV = float.Parse(Mondata_arr[11]);
            _data.DefPerLV = float.Parse(Mondata_arr[12]);
            _data.HPPERLV = float.Parse(Mondata_arr[13]);
            _data.FurtherDmg = float.Parse(Mondata_arr[14]);
            _data.DmgOffset = float.Parse(Mondata_arr[15]);
            _data.Cooldown = float.Parse(Mondata_arr[16]);
            _data.MoveSpeed = float.Parse(Mondata_arr[17]);
            _data.AttSpeed = float.Parse(Mondata_arr[18]);
            _data.AttRange = float.Parse(Mondata_arr[19]);
            _data.NoticeRange = float.Parse(Mondata_arr[20]);
            _data.pattern = Mondata_arr[21];
            _data.Scale = Mondata_arr[22];
            _data.RewardExp = float.Parse(Mondata_arr[23]);
            _data.Exp = float.Parse(Mondata_arr[24]);
            _data.RewardMoney = float.Parse(Mondata_arr[25]);
            _data.Money = float.Parse(Mondata_arr[26]);
            m_MonsterTable.Add(_data.MonID, _data);
        }
    }

    /// <summary>
    /// Get Hero Data_(Index Code)로 반환
    /// </summary>
    /// <param name="_Key"></param>
    /// <returns></returns>
    public HeroStat Get_Hero_Data(string _Key)
    {
        m_tempData = null;
        if (_Key == "")
        {
            return m_tempData;
        }
        else if(m_HeroTable.ContainsKey(_Key))
        {
            m_HeroTable.TryGetValue(_Key, out m_tempData);
            return m_tempData;
        }
        else return m_tempData;
    }
    /// <summary>
    /// Monster Data return ( Monster Index Code를 사용하여 반환)
    /// </summary>
    /// <param name="_Key"></param>
    /// <returns></returns>
    public MonsterStat Get_Monster_Data(string _Key)
    {
        if (m_MonsterTable.ContainsKey(_Key))
        {
            m_MonsterTable.TryGetValue(_Key, out m_tempData_Mon);
            return m_tempData_Mon;
        }
        else return m_tempData_Mon;
    }

    /// <summary>
    /// 전체 영웅의 Data Table이 몇개인지 체크
    /// </summary>
    /// <returns></returns>
    public int Get_HeroData_Count() { return m_HeroTable.Count; }
    /// <summary>
    /// 해당 영웅을 가지고있는지 체크하기위한 함수
    /// </summary>
    /// <param name="_Key"></param>
    /// <returns></returns>
    public bool Has_Hero(string _Key)
    {
        if (m_HeroTable.ContainsKey(_Key))
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 위와 같은 역할을 하는 확인용 함수
    /// </summary>
    /// <returns></returns>
    public List<HeroStat> Get_Hero_Data_List()
    {
        return m_HeroTable2;
    }
    public void Check_Hero(string[] List)
    {
        for (int i = 0; i < m_HeroTable2.Count; i++)
        {
            for (int j = 0; j < List.Length; j++)
            {
                if (m_HeroTable2[i].ID == List[j])
                {
                    m_HeroTable2[i].Has = true;
                }
            }
        }
    }
}
