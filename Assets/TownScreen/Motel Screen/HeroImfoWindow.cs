using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Select Hero Imfomation Output Window UI Class
/// </summary>
public class HeroImfoWindow : BaseWindow
{
    /// <summary>
    /// Image Type Enum
    /// </summary>
    private enum IMAGE_NUM { HERO, SKILL1, SKILL2, SKILL3, EQIP1, EQIP2, EQIP3 }
    /// <summary>
    /// Back Btn
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// Hero Imfomation Image Array
    /// </summary>
    [SerializeField]
    private Image[] m_ImfoImage;
    /// <summary>
    /// Hero Imfomation Text Array
    /// </summary>
    [SerializeField]
    private Text[] m_Text;
    /// <summary>
    /// Hero Stat Bar Array
    /// </summary>
    [SerializeField]
    private UI_Bar[] m_StatBar;
    /// <summary>
    /// Window Output TempStat
    /// </summary>
    private HeroStat m_TempStat;

    public override void Init()
    {
        m_Esc.onClick.AddListener(Exit);

        // Bar Max Value Init
        // 추후 표시할 최대값 조정하여 넣어야함.
        m_StatBar[0].Set_MaxValue(300);
        m_StatBar[1].Set_MaxValue(100);
        m_StatBar[2].Set_MaxValue(2000);
        m_StatBar[3].Set_MaxValue(5);
        m_StatBar[4].Set_MaxValue(5);
    }
    public override void Enter()
    {
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
    /// Hero Data Input Funtion 
    /// </summary>
    /// <param name="_btndata"></param>
    public void Set_HeroData(HeroBtnData _btndata)
    {
        // 선택한 영웅의 Index Code를 이용해 Stat값을 DB에서 가져옴
        m_TempStat = DataTable.Instance.Get_Hero_Data(_btndata.HeroCode);

        // Hero Text 를 수정하는부분
        m_Text[0].text = m_TempStat.CharName;
        // 추후 영웅스토리 등 등의 설명을 넣어야함

        // Hero Image를 수정하는부분
        // 위에 array변수를 사용해 선택된 영웅의 Skill등의 Image를 불러와서 입혀야함

        // 선택된 Hero의 Stat값을 비교하기위한 변수
        m_StatBar[0].Set_NowValue(m_TempStat.FirstAtt);
        m_StatBar[1].Set_NowValue(m_TempStat.FirstDef);
        m_StatBar[2].Set_NowValue(m_TempStat.FirstHP);
        m_StatBar[3].Set_NowValue(m_TempStat.MoveSpeed);
        m_StatBar[4].Set_NowValue(m_TempStat.AttSpeed);

        for (int i = 0; i < m_StatBar.Length; i++)
        {
            m_StatBar[i].Play();
        }
    }
}
