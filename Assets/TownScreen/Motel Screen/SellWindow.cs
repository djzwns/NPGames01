using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 영웅, 장비 등을 구매하기위해 필요한 Window UI Class
/// </summary>
public class SellWindow : BaseWindow
{
    /// <summary>
    /// 구매하려고는 Hero Index Code 저장 변수
    /// </summary>
    private string m_Hero_Index;
    /// <summary>
    /// 뒤로가기
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// Hero 가격 관리를 위한 Text 변수
    /// </summary>
    [SerializeField]
    private Text[] m_Char_Text;
    /// <summary>
    /// 구매하려는 Hero의 이미지를 출력하기 위한 변수
    /// </summary>
    [SerializeField]
    private Image m_Image;
    /// <summary>
    /// 구매하려는 Hero의 Btn Data를 가져오기위한 임시 변수
    /// </summary>
    private ShopHeroBtn m_Btn;
    /// <summary>
    /// 구매하려는 Hero의 Stat Data를 가지고있는 변수
    /// </summary>
    private HeroStat m_HeroData;
    /// <summary>
    /// 구매의사 버튼 ( 예 or 아니오)
    /// </summary>
    [SerializeField]
    private Button[] m_SellBtn;

    public override void Init()
    {
        // 초기화작업
        //m_SellBtn = new Button[2];
        //m_Char_Text = new Text[3];

        //m_Esc = gameObject.transform.FindChild("Back").GetComponent<Button>();
        //m_Image = gameObject.transform.FindChild("Hero_Image").GetComponent<Image>();
        //m_SellBtn[0] = gameObject.transform.FindChild("Cashe").GetComponent<Button>();
        //m_SellBtn[1] = gameObject.transform.FindChild("Gold").GetComponent<Button>();
        //m_Char_Text[0] = gameObject.transform.FindChild("Hero_Name").GetComponent<Text>();
        //m_Char_Text[1] = gameObject.transform.FindChild("Gold_Text").GetComponent<Text>();
        //m_Char_Text[2] = gameObject.transform.FindChild("Cashe_Text").GetComponent<Text>();

        //m_Esc.onClick.AddListener(Exit);
        //m_SellBtn[0].onClick.AddListener(Sell_Cash);
        //m_SellBtn[1].onClick.AddListener(Sell_Gold);
        gameObject.SetActive(false);
    }
    public void Enter(ShopHeroBtn _btn)
    {
        m_Btn = _btn;
        // 택스트를 제외한 나머지것들(Image)도 추후 Update 필요함.
        m_HeroData = DataTable.Instance.Get_Hero_Data(m_Btn.Get_Data().HeroCode);
        m_Char_Text[0].text = m_HeroData.CharName;
        m_Char_Text[1].text = string.Format("{0}", m_HeroData.SillingPrice);
        m_Char_Text[2].text = string.Format("{0}", m_HeroData.RubyPrice);
        
        gameObject.SetActive(true);
    }
    public override void Play()
    {
    }
    public override void Exit()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 유료재화를 사용하여 구매하려고 하였을때 호출
    /// </summary>
    public void Sell_Cash()
    {
        int temp_Sell_Cash = 0;
        if (PlayerData.Instance.Get_Data().m_AllCash - (int)m_HeroData.RubyPrice >= 0)
        {
            PlayerData.Instance.Get_Data().m_FreeCash -= (int)m_HeroData.RubyPrice;
            if(PlayerData.Instance.Get_Data().m_FreeCash < 0)
            {
                temp_Sell_Cash = PlayerData.Instance.Get_Data().m_FreeCash;
                PlayerData.Instance.Get_Data().m_FreeCash = 0;
                PlayerData.Instance.Get_Data().m_UserCash -= Mathf.Abs(temp_Sell_Cash);
            }
            PlayerData.Instance.Add_Hero(m_HeroData);
            m_Btn.Has = true;
            Exit();
        }
        else Debug.Log("캐시 재화 부족");
    }
    /// <summary>
    /// 게임 재화를 사용하려 구매하려고하였을때 호출
    /// </summary>
    public void Sell_Gold()
    {
        if (PlayerData.Instance.Get_Data().m_GameMoney - (int)m_HeroData.SillingPrice >= 0)
        {
            PlayerData.Instance.Get_Data().m_GameMoney -= (int)m_HeroData.SillingPrice;
            PlayerData.Instance.Add_Hero(m_HeroData);
            m_Btn.Has = true;
            Exit();
        }
        else Debug.Log("골드 재화 부족");
    }
}