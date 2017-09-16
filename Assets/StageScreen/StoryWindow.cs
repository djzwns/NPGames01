using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Redemption.Story;

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

    [SerializeField]
    private GameObject m_StageBtn;

    public override void Init()
    {
        m_Esc.onClick.AddListener(Exit);
        Create_Story_Btn();
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

    private void Create_Story_Btn()
    {
        // UI의 x와 y에 따라 값 조절
        for (int i = 0; i < 1; i++)
        {
            // UI의 x와 y에 따라 값 조절
            for (int j = 0; j < 1; j++)
            {
                GameObject temp = GameObject.Instantiate(m_StageBtn) as GameObject;
                temp.transform.parent = this.transform;

                StageBtn _Btn = temp.GetComponent<StageBtn>();
                _Btn.Init();
                // 추후 Stage Mode Data의 Index값을 Btn에 넘겨줘야함.
                _Btn.Set_Ready_Win(m_ReadyW);
                _Btn.Stage_Number((i * 5) + j + 1);

                Vector2 _pos = new Vector2(0 + (j * 0), 0 - (i * 0));
                _Btn.Set_Rect_Pos(_pos);

                // "수정필요"
                //_Btn.onClick.AddListener(StoryStart);
            }
        }
    }
}
