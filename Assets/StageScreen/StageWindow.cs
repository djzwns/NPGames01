using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Stage Mode 선택시 표시할 Window UI Class
/// </summary>
public class StageWindow : BaseWindow
{
    /// <summary>
    /// Stage Btn Create를 위한 Obj
    /// </summary>
    [SerializeField]
    private GameObject m_StageBtn;
    /// <summary>
    /// Esc -> 뒤로가기 버튼
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
        Create_Stage_Btn();
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
    /// <summary>
    /// Stage Data를 가지고있는 Stage Btn을 생성하는 함수
    /// </summary>
    private void Create_Stage_Btn()
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
            }
        }
    }
}
