using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Item Shop Window UI Class
/// </summary>
public class ItemShopWindow : BaseWindow
{
    /// <summary>
    /// 각 제화를 구분해 구매할수있는 Item Data를 저장하는 리스트
    /// </summary>
    private List<ShopBtnData> m_ValueList_1;
    private List<ShopBtnData> m_ValueList_2;
    private List<ShopBtnData> m_ValueList_3;
    private List<ShopBtnData> m_ValueList_4;

    /// <summary>
    /// 실제 Item 버튼을 관리할 Btn List
    /// </summary>
    private List<ShopItemBtn> m_AllBtn;
    /// <summary>
    /// Item Btn 생성을 위한 Prefab
    /// </summary>
    [SerializeField]
    private GameObject m_AllBtn_Obj;
    /// <summary>
    /// 뒤로가기 버튼
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// 버튼들을 정렬하기 위한 공간
    /// </summary>
    [SerializeField]
    private RectTransform m_ShopArea;

    /// <summary>
    /// Item 구매시 출력할 UI Window 변수
    /// </summary>
    [SerializeField]
    private SellWindow m_SellW;
    /// <summary>
    /// Player의 현재 재화를 표시하기 위한 변수
    /// </summary>
    [SerializeField]
    private Value_UI[] m_Value;

    public override void Init()
    {
        m_ValueList_1 = new List<ShopBtnData>(0);
        m_ValueList_2 = new List<ShopBtnData>(0);
        m_ValueList_3 = new List<ShopBtnData>(0);
        m_ValueList_4 = new List<ShopBtnData>(0);

        m_AllBtn = new List<ShopItemBtn>(0);

        m_SellW.Init();
        m_ShopArea.anchorMin = new Vector2(0, .5f);
        m_ShopArea.anchorMax = new Vector2(0, .5f);
        m_ShopArea.pivot = new Vector2(.5f, .5f);

        m_ShopArea.sizeDelta = new Vector2(465, (192+28)* 10);
        m_ShopArea.anchoredPosition = new Vector2((m_ShopArea.sizeDelta.x/2), 0);
        m_Esc.onClick.AddListener(Exit);
        
        // 버튼생성 부분
        // 추후 Item Data를 받아와서 어떤 아이템을 판매할것인지 정보를 넣어줌
        // 카테고리별 버튼 출력을 위해서 Type을 나눌 예정
        for(int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(m_AllBtn_Obj) as GameObject;
            temp.transform.parent = m_ShopArea.transform;
            temp.name = "Btn_" + i;

            ShopItemBtn _temp = temp.GetComponent<ShopItemBtn>();
            _temp.Init(this);
            _temp.Get_RectTransform().anchorMin = new Vector2(0, .5f);
            _temp.Get_RectTransform().anchorMax = new Vector2(0, .5f);
            _temp.Get_RectTransform().pivot = new Vector2(0.5f, 0.5f);
            _temp.Get_RectTransform().localScale = new Vector3(1, 1, 1);
            _temp.Get_RectTransform().anchoredPosition3D = 
                new Vector3(124 + (_temp.Get_RectTransform().sizeDelta.x + 28) * (i),0, 0);

            m_AllBtn.Add(_temp);
        }
    }
}
