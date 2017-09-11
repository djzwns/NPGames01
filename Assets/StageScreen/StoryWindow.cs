using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Story Mode를 선택시 표시하기 위한 Window UI Class
/// </summary>
public class StoryWindow : BaseWindow
{
    /// <summary>
    /// 뒤로가기 버튼
    /// </summary>
    [SerializeField]
    private Button m_Esc;
    /// <summary>
    /// Ready Window 호출을 위한 변수
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
