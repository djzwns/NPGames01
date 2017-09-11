using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 스킬 강화 등의 구매 Window를 표시하기위한 Class
/// </summary>
public class SkillSellWindow : BaseWindow
{
    /// <summary>
    /// 선택한 스킬의 Image 표기를 위한 변수
    /// </summary>
    [SerializeField]
    private Image m_SkillImage;
    /// <summary>
    /// 선택한 스킬의 정보를 표시
    /// </summary>
    [SerializeField]
    private Text m_Text;
    /// <summary>
    /// 살것인지 아닌지 버튼( Yes or No)
    /// </summary>
    [SerializeField]
    private Button[] m_Btn;

    public override void Init()
    {
        m_Btn[0].onClick.AddListener(Exit);
        m_Btn[1].onClick.AddListener(Sell_Btn);
    }
    public override void Enter()
    {
        gameObject.SetActive(true);
        is_Active = true;
    }
    public override void Play()
    {

    }
    public override void Exit()
    {
        is_Active = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Yes Btn
    /// </summary>
    private void Sell_Btn()
    {
        // 돈부족시 Window 출력

    }
}
