using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// item Data
/// </summary>
public class ShopBtnData
{
    public int ItemType;
    public string ItemCode;
}

/// <summary>
/// Item 구매를 위한 Btn
/// </summary>
public class ShopItemBtn : Button
{
    /// <summary>
    /// tranform 수정을 위한 변수
    /// </summary>
    private RectTransform m_Rect;
    private Text m_Text;
    /// <summary>
    /// Ui Controll을 위한 변수
    /// </summary>
    private ItemShopWindow m_ShopW;
    private Image m_Image;


    /// <summary>
    /// 현재 버튼이 가지고있는 Item Data의 정보
    /// </summary>
    private ShopBtnData m_BtnData;

    public void Init(ItemShopWindow _PartyW)
    {
        m_Text = GetComponentInChildren<Text>();
        m_Rect = GetComponent<RectTransform>();
        m_ShopW = _PartyW;
    }
    public void Set_Data(ShopBtnData _Data)
    {
        m_BtnData = _Data;
        gameObject.name = m_BtnData.ItemCode;
    }
    public void Set_Data(string _Number)
    {
        m_BtnData.ItemCode = string.Format("{0}", _Number);
    }
    public void Has_Btn()
    {
        m_Text.text = m_BtnData.ItemCode;
    }

    public ShopBtnData Get_Data() { return m_BtnData; }
    public RectTransform Get_RectTransform() { return m_Rect; }
}
