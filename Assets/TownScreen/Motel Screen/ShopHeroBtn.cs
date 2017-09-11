using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// 영웅 구매를 위한 Hero Btn Class
/// </summary>
public class ShopHeroBtn : Button
{
    private RectTransform m_Rect;
    private Text m_Text;
    private ShopWindow m_ShopW;
    private Image m_Image;
    private HeroBtnData m_BtnData;
    public bool Has { get { return is_Has; } set { is_Has = value; } }
    private bool is_Has;
    private bool is_Press;
    private float m_PressTime;

    void Update()
    {
        // 1초이상 누르고있을 시 선택한 버튼의 영웅정보를 출력한다.
        if (is_Press)
        {
            m_PressTime += Time.deltaTime;
            if (m_PressTime >= 1.0f)
            {
                m_ShopW.Set_Hero_Imfo(this);
                is_Press = false;
            }
        }
    }
    /// <summary>
    /// 버튼 초기화
    /// </summary>
    /// <param name="_PartyW"></param>
    public void Init(ShopWindow _PartyW)
    {
        m_Text = GetComponentInChildren<Text>();
        m_Rect = GetComponent<RectTransform>();
        m_ShopW = _PartyW;
    }
    /// <summary>
    /// 버튼이 가지고있는 영웅의 정보(가격 , Index Code 등)을 Set하는 함수
    /// </summary>
    /// <param name="_Data"></param>
    public void Set_Data(HeroBtnData _Data)
    {
        m_BtnData = _Data;
        gameObject.name = m_BtnData.HeroCode;
    }
    /// <summary>
    /// 버튼이 가지고있는 영웅이 구매되었을때 Enable 해주기 위해 체크하는 함수
    /// </summary>
    public void Has_Btn()
    {
        m_Text.text = m_BtnData.HeroCode;
    }

    public HeroBtnData Get_Data() { return m_BtnData; }
    public RectTransform Get_RectTransform() { return m_Rect; }

    /// <summary>
    /// Press 기능을 사용하기 위한 함수들
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
            m_ShopW.Set_Hero_Sell(this);
        }
        m_PressTime = 0;
        is_Press = false;
    }
}
