using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// GameScene에 필요한 UI를 관리하는 Canvas Class
/// </summary>
public class GameCanvas : BaseUICanvas
{
    /// <summary>
    /// Game Scene에 필요한 Btn Enum
    /// </summary>
    private enum GAMEBTN { PAUSE=0, BACK, TOWN, RETRY, TOWN_1, TOWN_2}
    /// <summary>
    /// GameScene Btn Array
    /// </summary>
    [SerializeField]
    private Button[] m_Btn;
    /// <summary>
    /// Lose Window UI
    /// </summary>
    [SerializeField]
    private GameObject Lose_Window;
    /// <summary>
    /// Win Window UI
    /// </summary>
    [SerializeField]
    private GameObject Win_Window;
    /// <summary>
    /// UI Trans -> Game UI 위치 고정을 위한 변수
    /// </summary>
    [SerializeField]
    private RectTransform[] m_Trans;

    public override void Init()
    {
        // 각 버튼에 역할에 맡는 함수 등록
        m_Btn[(int)GAMEBTN.PAUSE].onClick.AddListener(() => MonsterManager.Instance.On_Pause());
        m_Btn[(int)GAMEBTN.BACK].onClick.AddListener(() => MonsterManager.Instance.On_BACK());

        m_Btn[(int)GAMEBTN.RETRY].onClick.AddListener(() => PartyManager.Instance.ReStart());
        m_Btn[(int)GAMEBTN.RETRY].onClick.AddListener(() => MonsterManager.Instance.ReStart());

        m_Btn[(int)GAMEBTN.RETRY].onClick.AddListener(() => Exit());
        m_Btn[(int)GAMEBTN.TOWN].onClick.AddListener(() => MonsterManager.Instance.On_TOWN());
        m_Btn[(int)GAMEBTN.TOWN_1].onClick.AddListener(() => MonsterManager.Instance.On_TOWN());
        m_Btn[(int)GAMEBTN.TOWN_2].onClick.AddListener(() => MonsterManager.Instance.On_TOWN());
        
        // Game UI 들 위치 고정
        for (int i = 0; i < m_Trans.Length; i++)
        {
            m_Trans[i].localScale = new Vector3(1, 1, 1);
            m_Trans[i].anchorMax = new Vector2(.5f, .5f);
            m_Trans[i].anchorMin = new Vector2(.5f, .5f);
            m_Trans[i].anchoredPosition = new Vector3(0, 0, 0);
        }
    }

    public override void Enter()
    {
        Lose_Window.SetActive(false);
        Win_Window.SetActive(false);
    }

    public override void Play()
    {
        // 조건에 따라 UI 출현
        if(PartyManager.Instance.All_Clear)
        {
            Win_Window.SetActive(true);
        }
        else if(PartyManager.Instance.All_Dead)
        {
            Lose_Window.SetActive(true);
        }
    }

    public override void Exit()
    {
        Lose_Window.SetActive(false);
        Win_Window.SetActive(false);
    }
}