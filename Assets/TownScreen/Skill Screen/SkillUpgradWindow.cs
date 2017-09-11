using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Skill Data를 표시하기위한 Class(미완)
/// </summary>
public class SkillData
{
    public string SkillCode;
    public int Level;
}

/// <summary>
/// Skill 강화를 담당하는 Window UI Class
/// </summary>
public class SkillUpgradWindow : BaseWindow
{
    /// <summary>
    /// 버튼 생성을 위한 Prefab
    /// </summary>
    [SerializeField]
    private GameObject m_AllBtn_Obj;
    /// <summary>
    /// 뒤로가기 버튼
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// 버튼 배치를 위한 Area 객체
    /// </summary>
    [SerializeField]
    private RectTransform m_BtnArea;
    /// <summary>
    /// 보유중인 Hero의 스킬정보가 담긴 Btn을 관리하는 List
    /// </summary>
    private List<Skill_Upg_Btn> m_SkillBtnList;
    /// <summary>
    /// Player의 현재 재화를 나타내는 UI 객체
    /// </summary>
    [SerializeField]
    private Value_UI[] m_Value;
    /// <summary>
    /// 선택된 Hero의 스킬정보를 표시하기위한 Btn (이미지로 대체될 가능성 o)
    /// </summary>
    [SerializeField]
    private Button[] m_SkillBtn;
    /// <summary>
    /// 해당 스을 어떤 형태로 구매할것인지 선택하는 Btn UI Array (일반강화 초월강화 선택)
    /// </summary>
    [SerializeField]
    private Button[] m_SkillSellBtn;
    /// <summary>
    /// 해당 스킬이 현재 몇 LV인지 표시하기위한 Text UI
    /// </summary>
    [SerializeField]
    private Text[] m_Skill_Level_Text;
    /// <summary>
    /// 현재 선택한 Skill의 Data를 확인하기 위한 변수
    /// </summary>
    private SkillData m_CrtSkill;
    /// <summary>
    /// 스킬 강화 구매를 다시한번 확인하기 위한 확인 Window UI 객체
    /// </summary>
    [SerializeField]
    private SkillSellWindow m_SellWin;


    public override void Init()
    {
        m_SkillBtnList = new List<Skill_Upg_Btn>(0);

        is_Active = true;
        m_BtnArea.anchorMin = new Vector2(.5f, 1);
        m_BtnArea.anchorMax = new Vector2(.5f, 1);
        m_BtnArea.pivot = new Vector2(.5f, .5f);

        m_BtnArea.sizeDelta = new Vector2(300, 100 * PlayerData.Instance.Get_Data().m_HasHeroCount);
        m_BtnArea.anchoredPosition = new Vector2(0, -(m_BtnArea.sizeDelta.y / 2));
        m_Esc.onClick.AddListener(Exit);
        Init_Skil_Btn();
    }
    public override void Enter()
    {
        is_Active = true;
        gameObject.SetActive(true);
    }
    public override void Play()
    {
        for (int i = 0; i < m_Value.Length; i++)
        {
            m_Value[i].Play();
        }
    }
    public override void Exit()
    {
        is_Active = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 영웅(스킬정보)를 가진 Btn을 생성하는 함수
    /// </summary>
    private void Init_Skil_Btn()
    {
        for (int x = 0; x < PlayerData.Instance.Get_Data().m_HasHeroCount; x++)
        {
            GameObject temp = Instantiate(m_AllBtn_Obj) as GameObject;
            temp.transform.parent = m_BtnArea.transform;
            temp.name = "Btn_" + x;

            Skill_Upg_Btn _temp = temp.GetComponent<Skill_Upg_Btn>();
            _temp.Init(this, m_SkillBtnList.Count);
            _temp.Get_RectTransform().anchorMin = new Vector2(0, 1);
            _temp.Get_RectTransform().anchorMax = new Vector2(0, 1);
            _temp.Get_RectTransform().pivot = new Vector2(0.5f, 0.5f);
            _temp.Get_RectTransform().localScale = new Vector3(1, 1, 1);
            _temp.Get_RectTransform().anchoredPosition3D = new Vector3(0,
                    (-50) - (100 * x), 0);


            HeroBtnData t = new HeroBtnData();
            t.HeroCode = PlayerData.Instance.Get_Data().Char_CodeList[x].Index;
            _temp.Set_Data(t);

            m_SkillBtnList.Add(_temp);
        }
    }
    /// <summary>
    /// Btn들을 재배치하는 함수 (영웅 추가 구매시 버튼 생성 기능)
    /// </summary>
    private void Enter_Btn()
    {
        m_BtnArea.sizeDelta = new Vector2(300, 100 * PlayerData.Instance.Get_Data().m_HasHeroCount);

        for (int i = 0; i < PlayerData.Instance.Get_Data().Char_CodeList.Count; i++)
        {
            if (Check_Hero_Btn(PlayerData.Instance.Get_Data().Char_CodeList[i].Index))
            {
                GameObject temp = Instantiate(m_AllBtn_Obj) as GameObject;
                temp.transform.parent = m_BtnArea.transform;
                temp.name = "Btn_" + i;

                Skill_Upg_Btn _temp = temp.GetComponent<Skill_Upg_Btn>();
                _temp.Init(this, m_SkillBtnList.Count);
                _temp.Get_RectTransform().anchorMin = new Vector2(0, 1);
                _temp.Get_RectTransform().anchorMax = new Vector2(0, 1);
                _temp.Get_RectTransform().pivot = new Vector2(0.5f, 0.5f);
                _temp.Get_RectTransform().localScale = new Vector3(1, 1, 1);
                _temp.Get_RectTransform().anchoredPosition3D = new Vector3(0,
                    (-50) - (100 * i), 0);

                HeroBtnData t = new HeroBtnData();
                t.HeroCode = PlayerData.Instance.Get_Data().Char_CodeList[i].Index;
                _temp.Set_Data(t);

                m_SkillBtnList.Add(_temp);
            }
        }

        for (int x = 0; x < m_SkillBtnList.Count; x++)
        {
            m_SkillBtnList[x].Get_RectTransform().anchoredPosition3D =
                new Vector3(0, (-50) - (100 * x), 0);
        }
    }
    /// <summary>
    /// 버튼이 생성되었는지 아닌지 확인하고 생성을 허가하기 위해 확인하는 함수
    /// </summary>
    /// <param name="_Key"></param>
    /// <returns></returns>
    private bool Check_Hero_Btn(string _Key)
    {
        for (int j = 0; j < m_SkillBtnList.Count; j++)
        {
            if (m_SkillBtnList[j].Get_Data().HeroCode == _Key)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 선택 된 Btn의 정보에 따라 UI를 업데이트 해주는 함수 (Update 필요)
    /// </summary>
    /// <param name="_crt"></param>
    private void Click_Hero_Btn(Skill_Upg_Btn _crt)
    {
        // 선택된 버튼에대한
        // UI 업데이트
        // m_SkillBtn[0].image
        // m_SkillBtn[1].image
        // m_Skill_Level_Text[0].text
        // m_Skill_Level_Text[1].text
        // m_Skill_Level_Text[2].text
        // m_Skill_Level_Text[3].text
    }
}
