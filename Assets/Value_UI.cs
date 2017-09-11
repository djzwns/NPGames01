using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 재화 표기 UI
/// </summary>
public class Value_UI : MonoBehaviour
{
    private enum TYPE { Gold, Cash, Point }

    /// <summary>
    /// 부족한 재화 구매를 위한 나중을 위한 UI Btn
    /// </summary>
    [SerializeField]
    private Button m_Btn;
    /// <summary>
    /// 현재 재화량 표기 Text
    /// </summary>
    [SerializeField]
    private Text m_Text;
    /// <summary>
    /// 재화의 Image
    /// </summary>
    [SerializeField]
    private Image m_Icon;
    /// <summary>
    /// 현재 표시하고있는 재화의 Type
    /// </summary>
    [SerializeField]
    private TYPE m_ValueType;

    public void Init()
    {
        m_Btn.onClick.AddListener(Btn_SetUp);
    }

    /// <summary>
    /// 타입에 맞는 재화를 표시
    /// </summary>
    public void Play()
    {
        switch (m_ValueType)
        {
            case TYPE.Gold:
                m_Text.text = string.Format("{0}", PlayerData.Instance.Get_Data().m_GameMoney);
                break;
            case TYPE.Point:
                m_Text.text = string.Format("{0}", PlayerData.Instance.Get_Data().m_Point);
                break;
            case TYPE.Cash:
                m_Text.text = string.Format("{0}", PlayerData.Instance.Get_Data().m_AllCash);
                break;
        }
    }

    /// <summary>
    /// 재화 추가 구입 함수
    /// </summary>
    private void Btn_SetUp()
    {

    }
}
