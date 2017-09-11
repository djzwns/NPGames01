using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Hero Party 관리를 위한 Window Ui Class
/// </summary>
public class PartyWindow_New : BaseWindow
{
    /// <summary>
    /// 현재 내가 선택한 파티의 Hero Btn
    /// </summary>
    [SerializeField]
    private MyHeroBtn[] m_MyHeroBtns;
    /// <summary>
    /// 저장된 4개의 파티를 불러오기위한 Btn
    /// </summary>
    [SerializeField]
    private Button[] m_PartyQuickBtn;
    /// <summary>
    /// 파티 관리를 위한 현재 작업을할 Hero Btn 변수
    /// </summary>
    [SerializeField]
    private MyHeroBtn m_CrtMyHeroBtn;
    /// <summary>
    /// Window Transform 조정을 위한 변수
    /// </summary>
    [SerializeField]
    private RectTransform m_HeroArea;
    /// <summary>
    /// 보유하고있는 모든 영웅을 표시할 Btn을 생성하기위한 Create Obj
    /// </summary>
    [SerializeField]
    private GameObject m_AllBtn_Obj;
    /// <summary>
    /// 파티의 정보를 수정하기 위해 선택한 보유한 영웅버튼 변수
    /// </summary>
    [SerializeField]
    private AllHeroBtn m_CrtAllHeroBtn;
    /// <summary>
    /// 뒤로가기 버튼
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// 파티원 변경시 출력하기 위한 Window UI 변수
    /// </summary>
    [SerializeField]
    private PartyChangeWindow m_PartyChangeWindow;
    /// <summary>
    /// 영웅의 정보를 출력하기 위한 Window UI 변수
    /// </summary>
    [SerializeField]
    private HeroImfoWindow m_HeroImfoWindow;
    /// <summary>
    /// 보유하고있는 Hero Btn들을 관리하기위한 List 
    /// </summary>
    private List<AllHeroBtn> m_AllHeroBtnList;
    /// <summary>
    /// 현재 선택한 Player의 Party Number를 체크하기 위한 변수
    /// </summary>
    private int m_PartyNumberKey;
    /// <summary>
    /// 값 변환을 위한 임시 HeroBtnData 변수
    /// </summary>
    private HeroBtnData m_tempBtn;
    /// <summary>
    /// 값 변환을 위한 임시 Party_Index 변수
    /// </summary>
    private Party_Index m_Temp_Party;
    
    public override void Init()
    {
        m_Temp_Party = new Party_Index("", "", "");
        is_Active = true;
        m_tempBtn = new HeroBtnData();
        m_AllHeroBtnList = new List<AllHeroBtn>(0);
        Init_MyHero();
        Init_AllHero();
        Init_Quick();
        m_Esc.onClick.AddListener(Exit);
        m_HeroImfoWindow.Init();
    }
    public override void Enter()
    {
        m_PartyNumberKey = PlayerData.Instance.Get_Data().Party_Index;
        m_HeroImfoWindow.gameObject.SetActive(false);
        gameObject.SetActive(true);
        Enter_AllHero();
        Quick_Party_Btn(m_PartyNumberKey);
        is_Active = true;
    }
    public override void Play()
    {
        m_HeroImfoWindow.Play();
    }
    public override void Exit()
    {
        gameObject.SetActive(false);
        is_Active = false;
    }

    /// <summary>
    /// Party Hero Btn을 초기화
    /// </summary>
    private void Init_MyHero()
    {
        for(int i = 0; i < m_MyHeroBtns.Length; i++)
        {
            m_MyHeroBtns[i].Init(this, i);
        }
    }
    /// <summary>
    /// Party Quick Btn 초기화
    /// </summary>
    private void Init_Quick()
    {
        m_PartyQuickBtn[0].onClick.AddListener(() => Quick_Party_Btn(0));
        m_PartyQuickBtn[1].onClick.AddListener(() => Quick_Party_Btn(1));
        m_PartyQuickBtn[2].onClick.AddListener(() => Quick_Party_Btn(2));
        m_PartyQuickBtn[3].onClick.AddListener(() => Quick_Party_Btn(3));
    }
    /// <summary>
    /// 보유하고있는 Hero Btn을 생성하고 배치하는 함수
    /// </summary>
    private void Init_AllHero()
    {
        m_HeroArea.sizeDelta = new Vector2(521, 160*4);
        m_HeroArea.anchorMin = new Vector2(0.5f, 1);
        m_HeroArea.anchorMax = new Vector2(0.5f, 1);
        m_HeroArea.pivot = new Vector2(0.5f, 1);
        m_HeroArea.anchoredPosition = new Vector2(0, 0);
        
        //for(int y=0; y < 4; y++)
        {
            for (int x = 0; x < PlayerData.Instance.Get_Data().m_HasHeroCount; x++)
            {
                GameObject temp = Instantiate(m_AllBtn_Obj) as GameObject;
                temp.transform.parent = m_HeroArea.transform;
                temp.name = "Btn_"+ x;

                AllHeroBtn _temp = temp.GetComponent<AllHeroBtn>();
                _temp.Init(this);
                _temp.Get_RectTransform().anchorMin = new Vector2(0, 1);
                _temp.Get_RectTransform().anchorMax = new Vector2(0, 1);
                _temp.Get_RectTransform().pivot = new Vector2(0.5f, 0.5f);
                _temp.Get_RectTransform().localScale = new Vector3(1, 1, 1);
                _temp.Get_RectTransform().anchoredPosition3D = new Vector3(67.5f + (_temp.Get_RectTransform().sizeDelta.x + 20) * (x%4),
                    -90 - (_temp.Get_RectTransform().sizeDelta.y + 25) * (x/4),0);


                HeroBtnData t = new HeroBtnData();
                t.HeroCode = PlayerData.Instance.Get_Data().Char_CodeList[x].Index;
                _temp.Set_Data(t);

                m_AllHeroBtnList.Add(_temp);
            }
        }
    }
    /// <summary>
    /// 보유하고 있는 Hero Btn을 재배치하는 함수(영웅을 새로 구매하였을때 처리를 위한 함수)
    /// </summary>
    private void Enter_AllHero()
    {
        for (int i = 0; i < PlayerData.Instance.Get_Data().Char_CodeList.Count; i++)
        {
            if (Check_Hero_Btn(PlayerData.Instance.Get_Data().Char_CodeList[i].Index))
            {
                GameObject temp = Instantiate(m_AllBtn_Obj) as GameObject;
                temp.transform.parent = m_HeroArea.transform;
                temp.name = "Btn_" + m_AllHeroBtnList.Count;

                AllHeroBtn _temp = temp.GetComponent<AllHeroBtn>();
                _temp.Init(this);
                _temp.Get_RectTransform().sizeDelta = new Vector2(110, 110);
                _temp.Get_RectTransform().anchorMin = new Vector2(0, 1);
                _temp.Get_RectTransform().anchorMax = new Vector2(0, 1);
                _temp.Get_RectTransform().pivot = new Vector2(0.5f, 0.5f);
                _temp.Get_RectTransform().localScale = new Vector3(1, 1, 1);
                HeroBtnData t = new HeroBtnData();
                t.HeroCode = PlayerData.Instance.Get_Data().Char_CodeList[i].Index;
                _temp.Set_Data(t);

                m_AllHeroBtnList.Add(_temp);
            }
        }

        for (int x = 0; x < m_AllHeroBtnList.Count; x++)
        {
            m_AllHeroBtnList[x].Get_RectTransform().anchoredPosition3D =
                new Vector3(67.5f + (m_AllHeroBtnList[x].Get_RectTransform().sizeDelta.x + 25) * (x % 4),
                   -90 - (m_AllHeroBtnList[x].Get_RectTransform().sizeDelta.y + 25) * (x / 4), 0);
        }
    }

    /// <summary>
    /// Quick Party Btn을 눌렀을때 UI 정보를 Update하기 위한 함수
    /// </summary>
    /// <param name="_Num"></param>
    private void Quick_Party_Btn(int _Num)
    {
        m_PartyNumberKey = _Num;
        PlayerData.Instance.Set_Party_Index(m_PartyNumberKey);
        for (int i = 0; i < m_MyHeroBtns.Length; i++)
        {
            m_tempBtn.HeroCode = PlayerData.Instance.Get_Data().PartyMem_Index[m_PartyNumberKey].m_HeroCode[i];
            m_Temp_Party.m_HeroCode[i] = m_tempBtn.HeroCode;
            m_MyHeroBtns[i].Enter(m_tempBtn);
        }
    }
    /// <summary>
    /// 선택한 Hero Index Code가 이미 AllBtn에 생성되어있는지 안되어있는지 체크하는 변수
    /// (버튼 생성을 위한 변수)
    /// </summary>
    /// <param name="_Key"></param>
    /// <returns></returns>
    private bool Check_Hero_Btn(string _Key)
    {
        for (int j = 0; j < m_AllHeroBtnList.Count; j++)
        {
            if (m_AllHeroBtnList[j].Get_Data().HeroCode == _Key)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Party Hero Btn을 조작하는 함수
    /// </summary>
    /// <param name="_CrtMy"></param>
    public void Set_Crt_My_Btn(MyHeroBtn _CrtMy)
    {
        // 선택된 버튼과 같은 버튼을 다시 클릭했을때
        if (m_CrtMyHeroBtn == _CrtMy)
        {
            m_CrtMyHeroBtn.Set_Reset();
            m_CrtMyHeroBtn = null;
            Debug.Log("초기화");
        }
        // 다른 버튼일떄
        else
        {
            m_CrtMyHeroBtn = _CrtMy;
            Debug.Log(m_CrtMyHeroBtn.name);
        }
    }
    /// <summary>
    /// 보유 Hero Btn을 클릭했을때의 조작 함수
    /// </summary>
    /// <param name="_CrtAll"></param>
    public void Set_Crt_All_Btn(AllHeroBtn _CrtAll)
    {
        // 선택한 모든영웅 버튼을 활성화
        m_CrtAllHeroBtn = _CrtAll;
        if (m_CrtMyHeroBtn != null)
        { 
            if (!_CrtAll.Has) return;
            // 영웅을 보유하고 있을때 다음작업으로 넘어감

            // 아래 코드는 같은파티에 같은 영웅이 존재하지않게 하기위한 소스코드(현재 테스트모드이기때문에 제한해놓음)
            //if (Now_ParyMem(m_CrtAllHeroBtn))
            //{
            //    Debug.Log("이미 파티에 합류한 영웅입니다.");
            //    m_CrtAllHeroBtn = null;
            //}
            //else m_PartyChangeWindow.Enter(m_CrtMyHeroBtn, _CrtAll);

            // 선택되어져있는 Party Hero와 선택한 보유중인 Hero를 교대
            m_CrtMyHeroBtn.Chang_Data(m_CrtAllHeroBtn.Get_Data());
            Party_Set(m_CrtMyHeroBtn);
           
            m_CrtMyHeroBtn = null;
            m_CrtAllHeroBtn = null;
        }
    }
    /// <summary>
    /// 선택된 영웅의 정보를 HeroImfomation Window로 출력
    /// </summary>
    /// <param name="_tem"></param>
    public void Show_Hero_Imfo(HeroBtnData _tem)
    {
        m_HeroImfoWindow.Set_HeroData(_tem);
        m_HeroImfoWindow.Enter();
    }
    /// <summary>
    /// Party의 임시값을 넘겨서 실제 파티 정보에 보내는 함수
    /// </summary>
    /// <param name="_Crt"></param>
    private void Party_Set(MyHeroBtn _Crt)
    {
        m_Temp_Party.m_HeroCode[_Crt.Get_Index()] = _Crt.Get_Data().HeroCode;

        PlayerData.Instance.Set_Party_Mem(m_Temp_Party);
    }
}
