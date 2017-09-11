using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Hero Btn의 Data Class
/// </summary>
[SerializeField]
public class HeroBtnData
{
    /// <summary>
    /// Btn에 표시할 Image
    /// </summary>
    public Sprite Image;
    /// <summary>
    /// Btn이 가지고있을 Hero Index Code
    /// </summary>
    public string HeroCode;
}

/// <summary>
/// 보유중인 Hero 교체를 돕기위한 Ui Btn Class
/// </summary>
public class AllHeroBtn : Button
{
    /// <summary>
    /// 버튼의 Tramsform 제어를 위한 변수
    /// </summary>
    private RectTransform m_Rect;
    /// <summary>
    /// 버튼의 확인을 위한 Text(정보확인 나중에 제거필요)
    /// </summary>
    private Text m_Text;
    /// <summary>
    /// Player Party 교체를 위한 객체변수
    /// </summary>
    private PartyWindow_New m_Party_W2;
    /// <summary>
    /// Btn이 표시할 Image 변수
    /// </summary>
    private Image m_Image;
    /// <summary>
    /// 버튼이 가지고있는 HeroData
    /// </summary>
    [SerializeField]
    private HeroBtnData m_BtnData;
    public bool Has { get { return is_Has; } set { is_Has = value; } }
    private bool is_Has;
    private bool is_Press;
    private float m_PressTime;

    void Update()
    {
        // 1초이상 Press 중이면 영웅의 정보가 출력
        if (is_Press)
        {
            m_PressTime += Time.deltaTime;
            if (m_PressTime >= 1.0f)
            {
                m_Party_W2.Show_Hero_Imfo(m_BtnData);
                Debug.Log("m_Party_W2.Show_Hero_Imfo()");
                is_Press = false;
            }
        }
    }
    /// <summary>
    /// Base Init Funtion
    /// </summary>
    public void Init()
    {
        m_Text = GetComponentInChildren<Text>();
    }
    /// <summary>
    /// 현자 사용중인 초기화 함수
    /// </summary>
    /// <param name="_PartyW"></param>
    public void Init(PartyWindow_New _PartyW)
    {
        m_Text = GetComponentInChildren<Text>();
        m_Rect = GetComponent<RectTransform>();
        m_Party_W2 = _PartyW;
        is_Has = true;
    }
    /// <summary>
    /// Btn에 Hero Data를 set하는 함수
    /// </summary>
    /// <param name="_Data"></param>
    public void Set_Data(HeroBtnData _Data) { m_BtnData = _Data; }
    /// <summary>
    /// Btn에 Hero Data를 set하는 함수 (현재 미사용)
    /// </summary>
    /// <param name="_Data"></param>
    public void Set_Data(string _Number)
    {
        m_BtnData.HeroCode = string.Format("{0}", _Number);
        if (Has)
        {
            m_Text.text = m_BtnData.HeroCode;

        }
        else
        {
            m_Text.text = string.Format("{0}", "X");
        }
    }
    public void Has_Btn()
    {
        m_Text.text = m_BtnData.HeroCode;
    }

    public HeroBtnData Get_Data() { return m_BtnData; }
    public RectTransform Get_RectTransform() { return m_Rect; }

    /// <summary>
    /// Press 관련 함수
    /// </summary>
    /// <param name="data"></param>
    public override void OnPointerDown(PointerEventData data)
    {
        m_PressTime = 0;
        is_Press = true;
    }
    public override void OnPointerUp(PointerEventData data)
    {
        
        if (m_PressTime < 1.0f)
        {
            Debug.Log("m_Party_W2.Set_Crt_All_Btn(this)");
            m_Party_W2.Set_Crt_All_Btn(this);
        }
        m_PressTime = 0;
        is_Press = false;
    }
}