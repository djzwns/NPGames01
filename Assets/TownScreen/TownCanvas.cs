using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// UI Controll Eum
/// </summary>
public enum TOWNMODE { INVENTORY = 1, QUEST,PARTY, SHOP, SKILL, LIB }
/// <summary>
/// Town Screen UI Canvas Class
/// </summary>
public class TownCanvas : BaseUICanvas
{
    public enum TOWNBTN { START=0 ,INVENTORY,QUEST,PARTY, SHOP, SKILL, LIB }
    public enum TOWNTEXT { NAME = 0, LEVEL, MONEY, POINT, CASH }
    /// <summary>
    /// Btn List
    /// </summary>
    [SerializeField]
    private Button[] m_BtnList;
    /// <summary>
    /// Player Exp Var 
    /// </summary>
    [SerializeField]
    private UI_Bar m_Var;
    /// <summary>
    /// Text ( 추후 제거 예정 )
    /// </summary>
    [SerializeField]
    private Text[] m_Texts;
    /// <summary>
    /// 프로필 Image 
    /// </summary>
    [SerializeField]
    private Image m_LeaderImage;
    /// <summary>
    /// 접속한 Player의 Data
    /// </summary>
    private Player_Data m_Data;
    
    /// <summary>
    /// Ui Screen Array
    /// </summary>
    [SerializeField]
    private BaseScreen[] m_ScreenList;
    /// <summary>
    /// 현재 실행중인 Screen 객체
    /// </summary>
    private BaseScreen m_CrtScreen;


    public override void Init()
    {
        Init_Btn();
        for (int i = 0; i < m_ScreenList.Length; i++)
        {
            m_ScreenList[i].Init();
            m_ScreenList[i].gameObject.SetActive(false);
        }
    }
    public override void Enter()
    {
        Player_Data_Load();
    }
    public override void Play()
    {
        Player_Data_Load();

        if (m_CrtScreen == null)
        {
            return;
        }
        else if (!m_CrtScreen.Get_Active_Screen())
        {
            m_CrtScreen = null;
            return;
        }
        m_CrtScreen.Play();
    }
    public override void Exit()
    {
        for (int i = 0; i < m_ScreenList.Length; i++) { m_ScreenList[i].Exit(); }
    }

    /// <summary>
    /// Btn 초기화 -> 함수 지정
    /// </summary>
    private void Init_Btn()
    {
        m_BtnList[(int)TOWNBTN.START].onClick.AddListener(() => Btn((TOWNBTN.START)));
        m_BtnList[(int)TOWNBTN.SKILL].onClick.AddListener(() => Btn((TOWNBTN.SKILL)));
        //m_BtnList[(int)TOWNBTN.QUEST].onClick.AddListener(() => Btn((TOWNBTN.QUEST)));
        m_BtnList[(int)TOWNBTN.PARTY].onClick.AddListener(() => Btn((TOWNBTN.PARTY)));
        m_BtnList[(int)TOWNBTN.SHOP].onClick.AddListener(() => Btn((TOWNBTN.SHOP)));
        //m_BtnList[(int)TOWNBTN.INVENTORY].onClick.AddListener(() => Btn((TOWNBTN.INVENTORY)));
        m_BtnList[(int)TOWNBTN.LIB].onClick.AddListener(() => Btn((TOWNBTN.LIB)));

    }
    /// <summary>
    /// 각 버튼의 Mode 별로 함수 지정
    /// </summary>
    /// <param name="_Mode"></param>
    private void Btn(TOWNBTN _Mode)
    {
        switch (_Mode)
        {
            case TOWNBTN.START: ProgramManager.Insatnce.Change_Scene(SceneName.STAGE); break;
            //case TOWNBTN.INVENTORY: break;
            //case TOWNBTN.QUEST: break;
            //case TOWNBTN.PARTY: m_PartyWin.Enter(); break;
            //case TOWNBTN.SKILL: break;
            //case TOWNBTN.SHOP: break;
            //case TOWNBTN.LIB: break;
            default: Select_Crt_Screen(_Mode); break;
        }
    }

    /// <summary>
    /// Player Data를 가져오고 재화나 Player정보를 업데이트
    /// </summary>
    private void Player_Data_Load()
    {
        m_Data = PlayerData.Instance.Get_Data();

        m_Texts[(int)TOWNTEXT.NAME].text = m_Data.m_Name;
        m_Texts[(int)TOWNTEXT.LEVEL].text = string.Format("{0}", m_Data.m_PlayerLevel);
        m_Texts[(int)TOWNTEXT.MONEY].text = string.Format("{0}", m_Data.m_GameMoney);
        m_Texts[(int)TOWNTEXT.POINT].text = string.Format("{0}", m_Data.m_Point);
        m_Texts[(int)TOWNTEXT.CASH].text = string.Format("{0}", m_Data.m_AllCash);

        m_Var.Set_MaxValue(100);
        m_Var.Set_NowValue(m_Data.m_NowPlayerExperience);
        m_Var.Play();
    }
    /// <summary>
    /// 선택된 Screen 객체를 Enter 함수로 호출해주는 함수
    /// </summary>
    /// <param name="_Mode"></param>
    private void Select_Crt_Screen(TOWNBTN _Mode)
    {
        if (m_CrtScreen!=null && ((int)m_CrtScreen.Get_Town_Mode() != (int)_Mode))
        {
            m_CrtScreen.Exit();
            m_CrtScreen = m_ScreenList[(int)_Mode-1];
            m_CrtScreen.Enter();
        }

        if(m_CrtScreen == null)
        {
            m_CrtScreen = m_ScreenList[(int)_Mode - 1];
            m_CrtScreen.Enter();
        }
    }
}
