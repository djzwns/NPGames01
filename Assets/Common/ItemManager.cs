using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ItemManager -> 아이템 관리를 위한 Manager 클래스
/// (미구현)
/// </summary>
public class ItemManager : MonoBehaviour
{
    public ItemManager Instance
    {
        get { return m_instance; }
        set { m_instance = value; }
    }
    private static ItemManager m_instance;
    private Dictionary<string, Item> m_ItemList;

    void Awake()
    {
        Instance = this;
    }
}
