using UnityEngine;

/// <summary>
/// 아이템 타입 관리를 위한 Enum
/// </summary>
public enum ITEMTYPE
{
    Equipment = 0,//장비
    Consumption,//소모
    Misc//기타
}
/// <summary>
/// 장비 타입 아이템의 Type을 구분하기 위한 Enum
/// </summary>
public enum EQIPTYPE
{
    NULL = 0,
    WEAPON,
    ARMOR,
    ACCESS
}

/// <summary>
/// 아이템 Class
/// </summary>
[System.Serializable]
public class Item
{
    // Item Index Code 변수
    public string itemIndex;
    // Item Name
    public string itemName; // 이름
    // Item Value -> Game Value
    public int itemValue; // 가치
    // Item Price -> Game Price 
    public int itemPrice; // 가격
    // Item의 설명을 위한 변수
    public string itemDesc; // 설명
    // Item의 타입
    public ITEMTYPE itemType; // 타입
    // Item의 타입이 장비일때 타입
    public EQIPTYPE epipType;
    // Item의 Sprite Image
    public Sprite itemImage; // 이미지
    
    /// <summary>
    /// 생성자 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemName"></param>
    /// <param name="_itemValue"></param>
    /// <param name="_itemPrice"></param>
    /// <param name="_itemDesc"></param>
    /// <param name="_itemType"></param>
    /// <param name="_eqipType"></param>
    /// <param name="_itemImage"></param>
    public Item(string _itemIndex,string _itemName, int _itemValue, int _itemPrice, string _itemDesc, ITEMTYPE _itemType,EQIPTYPE _eqipType, Sprite _itemImage)
    {
        itemName = _itemName;
        itemValue = _itemValue;
        itemPrice = _itemPrice;
        itemDesc = _itemDesc;
        itemType = _itemType;
        itemImage = _itemImage;
    }
    /// <summary>
    /// 기본 생성자
    /// </summary>
    public Item()
    {

    }
}