using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인벤토리 Class
/// ( 미구현 )
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Slot 생성을 위한 Object
    /// </summary>
    [SerializeField]
    private GameObject m_Slot;
    /// <summary>
    /// Inventory Rect 관리를 위한 변수
    /// </summary>
    private RectTransform m_InventoryRect;
    /// <summary>
    /// ItemSlot 관리를 위한 List
    /// </summary>
    private List<ItemSlot> m_AllSlot;
    /// <summary>
    /// 임시 이미지를 가지고있는 Object
    /// ( Test용 )
    /// </summary>
    [SerializeField]
    private GameObject m_TempImg;
    /// <summary>
    /// Inventory의 Width와 Hight관리를 위한 float 변수
    /// </summary>
    private float m_Inventory_Width;
    private float m_Inventory_Hight;
    /// <summary>
    /// ItemSlot의 갯수를 관리하기위한 int 형 변수
    /// </summary>
    private int Width_Count = 6;
    private int Hight_Count = 3;
    /// <summary>
    /// Slot사이의 간격을 설정하기위한 변수
    /// </summary>
    private float m_Gap = 25;
    /// <summary>
    /// Item Slot의 좌우 크기를 설정하기위한 변수
    /// </summary>
    private int SlotSize = 50;
    /// <summary>
    /// 비어있는 Slot의 갯수를 파악하기위한 변수
    /// </summary>
    private int m_EmptySlotCount;

    void Awake()
    {
        Init();
    }
    public void Init()
    {
        m_AllSlot = new List<ItemSlot>(0);
        m_InventoryRect = GetComponent<RectTransform>();

        // Temp Object의 Rect를 설정
        RectTransform RectTemp = m_TempImg.GetComponent<RectTransform>();
        RectTemp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
        RectTemp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);

        m_Inventory_Width = (SlotSize * Width_Count) + (m_Gap * Width_Count) + m_Gap;
        m_Inventory_Hight = (SlotSize * Hight_Count) + (m_Gap * Hight_Count) + m_Gap;

        // InventoryRect를 설정
        m_InventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_Inventory_Width);
        m_InventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_Inventory_Hight);

        // Item Slot을 생성하는 부분
        for (int y = 0; y < Hight_Count; y++)
        {
            for (int x = 0; x < Width_Count; x++)
            {
                GameObject Temp_Slot = Instantiate(m_Slot) as GameObject;
                RectTransform SlotRect = Temp_Slot.GetComponent<RectTransform>();
                ItemSlot _Slot = Temp_Slot.GetComponent<ItemSlot>();

                SlotRect.anchorMax = new Vector2(0, 1);
                SlotRect.anchorMin = new Vector2(0, 1);
                
                Temp_Slot.name = "Sloat_" + y + "_" + x;
                Temp_Slot.transform.parent = transform;

                SlotRect.anchoredPosition3D = new Vector3(SlotSize / 2, -SlotSize / 2, 0)
                    + new Vector3((SlotSize * x)+(m_Gap*(x+1)),
                    -(SlotSize * y+1) - (m_Gap * (y + 1)), 0);
                
                SlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
                SlotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);
                SlotRect.localScale = Vector3.one;

                m_AllSlot.Add(_Slot);
            }
        }

        m_EmptySlotCount = m_AllSlot.Count;
    }
    public void Enter()
    {
    }
    public void Exit()
    {
    }
}
