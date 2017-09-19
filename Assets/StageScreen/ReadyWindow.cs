using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Redemption.Story;

/// <summary>
/// Ready Window -> 스테이지를 선택한 후 퀵파티를 조정하고 Stage의 정보를 간략하게 보여주는 Window Class
/// </summary>
public class ReadyWindow : BaseWindow
{
    /// <summary>
    /// 스테이지의 Title을 출력하는 Text UI
    /// </summary>
    [SerializeField]
    private Text m_TitleText;
    /// <summary>
    /// 뒤로 가기 버튼
    /// </summary>
    [SerializeField]
    private Button[] m_SysBtn;
    /// <summary>
    /// 현재 파티의 영웅들의 초상화를 출력하기위한 이미지 Obj
    /// </summary>
    [SerializeField]
    private Image[] m_HeroImage;
    /// <summary>
    /// 게임제화로 게임에서 Buff를 사기위한 Btn
    /// </summary>
    [SerializeField]
    private Button[] m_BuffBtn;
    /// <summary>
    /// Player가 저장한 Party 4개의 퀵 Btn
    /// </summary>
    [SerializeField]
    private Button[] m_QuickBtn;
    /// <summary>
    /// 현재 스테이지의 Boss의 초상화 Image Obj
    /// </summary>
    [SerializeField]
    private Image m_BossIcon;
    /// <summary>
    /// 현재 선택된 Party의 퀵넘버를 저장하는 int형 변수
    /// </summary>
    private int m_PartyNumberKey;
    /// <summary>
    /// 선택된 Stage의 Data를 load하여 정보를 출력하기위한 변수
    /// </summary>
    private StageData m_CrtData;

    [SerializeField]
    private GameObject m_attachWindow;
    
    public override void Init()
    {
        // 퀵파티 설정 버튼
        m_QuickBtn[0].onClick.AddListener(() => Quick_Party_Btn(0));
        m_QuickBtn[1].onClick.AddListener(() => Quick_Party_Btn(1));
        m_QuickBtn[2].onClick.AddListener(() => Quick_Party_Btn(2));
        m_QuickBtn[3].onClick.AddListener(() => Quick_Party_Btn(3));
        // 뒤로가기 버튼
        m_SysBtn[0].onClick.AddListener(() => Exit());
        // 게임시작 버튼
        m_SysBtn[1].onClick.AddListener(() => Game_Start());

        gameObject.SetActive(false);
    }
    public override void Enter()
    {
        //Title Text 수정
        m_TitleText.text = m_CrtData.StageName;

        // 보스아이콘 불러오기

        //캐릭터 아이콘 및 퀵슬롯
        Quick_Party_Btn(PlayerData.Instance.Get_Data().Party_Index);

        //창을 띄워준다.
        gameObject.SetActive(true);
    }
    public override void Play()
    {

    }
    public override void Exit()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 파티 퀵버튼 선택시(int -> 파티의 번호)
    /// </summary>
    /// <param name="_Num"></param>
    private void Quick_Party_Btn(int _Num)
    {
        m_PartyNumberKey = _Num;
        // Player의 파티번호를 바꿔준다.
        PlayerData.Instance.Set_Party_Index(m_PartyNumberKey);
        for (int i = 0; i < m_HeroImage.Length; i++)
        {
            // 이미지 교체
            Debug.Log(PlayerData.Instance.Get_Data().PartyMem_Index[m_PartyNumberKey].m_HeroCode[i]);
        }
    }
    /// <summary>
    /// Stage Data를 출력하기위한 Data를 받아오는 변수
    /// </summary>
    /// <param name="_data"></param>
    public void Set_StageData(StageData _data)
    {
        Debug.Log(_data.m_Wave.Length);
        m_CrtData = _data;
    }
    /// <summary>
    /// Stage Data를 넘겨서 Game을 시작하는 함수
    /// </summary>
    public void Game_Start()
    {
        MonsterManager.Instance.Set_StageData(m_CrtData);
        Debug.Log(m_CrtData.m_Wave.Length);
        string window = m_attachWindow.name;
        switch (window)
        {
            case "Stage_Window":
                ProgramManager.Insatnce.Change_Scene(SceneName.GAME);
                break;
            case "Story_Window":
                ConversationManager.Instance.LoadStory("story1");
                ProgramManager.Insatnce.Change_Scene(SceneName.STORY);
                break;
            default:
                break;
        }
    }

    public void SetWindow(GameObject _window)
    {
        m_attachWindow = _window;
    }
}
