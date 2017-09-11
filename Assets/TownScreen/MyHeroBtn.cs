using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Party 관리를 위한 MyHero Btn Class
/// </summary>
public class MyHeroBtn : Button
{
    /// <summary>
    /// AllHero Btn과 비슷함
    /// </summary>
    private int m_Index;
    private Text m_Text;

    /// <summary>
    /// Party를 관리하는 UI 객체 Class 변수
    /// </summary>
    private PartyWindow_New m_Party_W2;
    /// <summary>
    /// 버튼이 표시할 Image
    /// </summary>
    private Image m_Image;
    /// <summary>
    /// 현재 선택한 Btn의 Data
    /// </summary>
    [SerializeField]
    private HeroBtnData m_BtnData;

    /// <summary>
    /// 초기화 하는 함수
    /// </summary>
    /// <param name="_PartyW"></param>
    /// <param name="_index"></param>
    public void Init(PartyWindow_New _PartyW, int _index)
    {
        m_BtnData = new HeroBtnData();
        m_Index = _index;
        m_Text = GetComponentInChildren<Text>();
        m_Party_W2 = _PartyW;
        onClick.AddListener(() => m_Party_W2.Set_Crt_My_Btn(this));
    }

    public void Enter(HeroBtnData _data)
    {
        gameObject.name = _data.HeroCode;

        if (_data.HeroCode == null)
        {
            return;
        }
        else
        {
            m_BtnData = _data;
            //m_Text.text = m_BtnData.HeroCode;
        }
    }
    /// <summary>
    /// 다른 버튼과 Data를 교체하는 함수
    /// </summary>
    /// <param name="_Data"></param>
    public void Chang_Data(HeroBtnData _Data)
    {
        // PlayerData.Instance.Get_Data().PartyMem_Index[m_Index] = m_BtnData.HeroCode;
        
        m_BtnData = _Data;
        //m_Text.text = m_BtnData.HeroCode;
        gameObject.name = _Data.HeroCode;
    }
    public HeroBtnData Get_Data() { return m_BtnData; }
    public void Set_Reset()
    {
        m_BtnData = null;
    }
    public int Get_Index() { return m_Index; }
}
