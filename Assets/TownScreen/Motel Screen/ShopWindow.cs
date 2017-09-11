using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 영웅을 구매하기 위한 Shop Window Ui Class
/// </summary>
public class ShopWindow : BaseWindow
{
    /// <summary>
    /// 보유하지 못한 영웅의 정보를 가지고있는 Btn들의 List
    /// </summary>
    private List<ShopHeroBtn> m_AllBtn;
    /// <summary>
    /// Btn을 생성하기 위한 Prefab
    /// </summary>
    [SerializeField]
    private GameObject m_AllBtn_Obj;
    /// <summary>
    /// 현재 선택된 구매 Hero Btn 체크
    /// </summary>
    [SerializeField]
    private ShopHeroBtn m_CrtAllHeroBtn;
    /// <summary>
    /// 뒤로가기
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// 버튼 배치를 위한 Area 설정을 위한 변수
    /// </summary>
    [SerializeField]
    private RectTransform m_HeroArea;

    /// <summary>
    /// 구매를 선택했을시 표시할 Window 변수
    /// </summary>
    [SerializeField]
    private SellWindow m_SellW;
    /// <summary>
    /// 영웅의 정보를 출력하기위한 Window 변수
    /// </summary>
    [SerializeField]
    private HeroImfoWindow m_HeroImfo;
    /// <summary>
    /// 현재 플레이어의 재화를 출력하기위한 변수
    /// </summary>
    [SerializeField]
    private Value_UI[] m_Value;


    public override void Init()
    {
        // Area 초기설정
        m_AllBtn = new List<ShopHeroBtn>(0);
        m_SellW.Init();
        m_HeroArea.sizeDelta = new Vector2(922, 160 * 4);
        m_HeroArea.anchorMin = new Vector2(0.5f, 1);
        m_HeroArea.anchorMax = new Vector2(0.5f, 1);
        m_HeroArea.pivot = new Vector2(0.5f, 1);
        m_HeroArea.anchoredPosition = new Vector2(0, 0);
        m_Esc.onClick.AddListener(Exit);
        

        DataTable.Instance.Check_Hero(PlayerData.Instance.Get_Has_Hero_Code());
        int real_x = 0;
        //가지고있지않은 영웅의 수를 체크하여 그만큼 버튼을 생성한다.
        for (int x = 0; x < DataTable.Instance.Get_HeroData_Count(); x++)
        {
            if (DataTable.Instance.Get_Hero_Data_List()[x].Has) { }
            else
            {
                GameObject temp = Instantiate(m_AllBtn_Obj) as GameObject;
                temp.transform.parent = m_HeroArea.transform;
                temp.name = "Btn_" + x;

                // 가지고있지 않은 영웅의 index Code를 임시 정보에 넣어준다.
                HeroBtnData t = new HeroBtnData();
                t.HeroCode = DataTable.Instance.Get_Hero_Data_List()[x].ID;
                

                ShopHeroBtn _temp = temp.GetComponent<ShopHeroBtn>();
                _temp.Init(this);
                //임시 정보를 넣어준다.
                _temp.Set_Data(t);
                // 버튼 배치
                _temp.Get_RectTransform().sizeDelta = new Vector2(102, 102);
                _temp.Get_RectTransform().anchorMin = new Vector2(0, 1);
                _temp.Get_RectTransform().anchorMax = new Vector2(0, 1);
                _temp.Get_RectTransform().pivot = new Vector2(0.5f, 0.5f);
                _temp.Get_RectTransform().localScale = new Vector3(1, 1, 1);
                _temp.Get_RectTransform().anchoredPosition3D = new Vector3(80 + (_temp.Get_RectTransform().sizeDelta.x + 25) * (real_x % 7),
                    -90 - (_temp.Get_RectTransform().sizeDelta.y + 25) * (real_x / 7), 0);
                real_x++;

                //버튼 리스트에 넣어줌
                m_AllBtn.Add(_temp);
            }
        }
    }
    public override void Enter()
    {
        gameObject.SetActive(true);
        m_HeroImfo.gameObject.SetActive(false);
        m_SellW.gameObject.SetActive(false);
        is_Active = true;
    }
    public override void Play()
    {
        for (int i = 0; i < m_Value.Length; i++)
        {
            m_Value[i].Play();
        }

        Re_Position_Btn();
    }
    public override void Exit()
    {
        gameObject.SetActive(false);
        is_Active = false;
    }

    /// <summary>
    /// 구매하려는 영웅을 눌렀을때 발생하는 함수(구매 Window Ui 출력)
    /// </summary>
    /// <param name="_Btn"></param>
    public void Set_Hero_Sell(ShopHeroBtn _Btn)
    {
        m_SellW.Enter(_Btn);
    }
    /// <summary>
    /// 선택한 버튼의 영웅 정보를 노출하려고 하는함수(영웅 정보 Window Ui 출력)
    /// </summary>
    /// <param name="_Btn"></param>
    public void Set_Hero_Imfo(ShopHeroBtn _Btn)
    {
        m_HeroImfo.Set_HeroData(_Btn.Get_Data());
        m_HeroImfo.Enter();
    }
    /// <summary>
    /// 영웅이 구매됬을때 그 영웅을 제외한 버튼의 위치를 재조정하는 함수
    /// </summary>
    public void Re_Position_Btn()
    {
        int real_x = 0;
        for(int i = 0; i < m_AllBtn.Count; i++)
        {
            if (m_AllBtn[i].Has)
            {
                m_AllBtn[i].gameObject.SetActive(false);
            }
            else
            {
                m_AllBtn[i].Get_RectTransform().anchoredPosition3D =
                new Vector3(90 + (m_AllBtn[i].Get_RectTransform().sizeDelta.x + 25) * (real_x % 7),
                    -90 - (m_AllBtn[i].Get_RectTransform().sizeDelta.y + 25) * (real_x / 7), 0);
                real_x++;
            }
        }
    }
}
