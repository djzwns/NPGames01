using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Stage Select Window 중 하나인 Raid Mode Window Class
/// 레이드 모드의 스테이지를 선택하는 Window를 출력하고 UI의 역할을 부여하고 실행
/// </summary>
public class RaidWindow : BaseWindow
{
    /// <summary>
    /// 뒤로가기 버튼
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// 추후 다른 화면으로 넘어갈때 이 화면을 컨트롤하기위한 변수
    /// </summary>
    [SerializeField]
    private ReadyWindow m_ReadyW;

    public override void Init()
    {
        m_Esc.onClick.AddListener(Exit);
    }
    public override void Enter()
    {
        this.gameObject.SetActive(true);
    }
    public override void Play()
    {
    }
    public override void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
