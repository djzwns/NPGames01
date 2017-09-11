using UnityEngine;
using System.Collections;


/// <summary>
/// Inventory Ui에서 생성하여 관리하기위한 Object Class
/// </summary>
public class ItemSlot : MonoBehaviour
{
    /// <summary>
    /// Slot이 가지고있는 ItemCode (Get,Set)
    /// </summary>
    public string ItemCode
    {
        get { return m_ItemCode; }
        set { m_ItemCode = value; }
    }
    private string m_ItemCode;
    /// <summary>
    /// ItemSlot에 있는 Item 정보
    /// </summary>
    private Item m_Item;
    /// <summary>
    /// Slot이 가지고있는 Item 정보 Get 함수
    /// </summary>
    /// <returns></returns>
    public Item Get_Item()
    {
        return m_Item;
    }
}
