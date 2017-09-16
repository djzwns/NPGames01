using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Stage Select Btn Class
/// </summary>
public class StageBtn : Button
{
    /// <summary>
    /// 버튼 배치를 위한 Transform 정보
    /// </summary>
    private RectTransform m_RectTrans;
    /// <summary>
    /// Text -> 어떤 스테이지인지 확인 (현재 필요)
    /// </summary>
    private Text m_Text;
    /// <summary>
    /// Stage의 번호를 알기위한 변수
    /// </summary>
    private int m_StageNum;
    /// <summary>
    /// Ready창 호출을 위한 변수
    /// </summary>
    private ReadyWindow m_Ready;

    public void Init()
    {
        onClick.AddListener(() => onClick_Btn());
        m_Text = transform.GetComponentInChildren<Text>();
        m_RectTrans = GetComponent<RectTransform>();
        m_RectTrans.anchorMax = new Vector2(0, 1);
        m_RectTrans.anchorMin = new Vector2(0, 1);
    }
    /// <summary>
    /// 제대로 들어오는지 확인하기위한 함수(추후 필요 X)
    /// </summary>
    /// <param name="_num"></param>
    public void Stage_Number(int _num)
    {
        m_StageNum = _num;
        m_Text.text = string.Format("{0}", m_StageNum);
    }
    /// <summary>
    /// Btn의 위치를 조정하기위한 함수
    /// </summary>
    /// <param name="_pos"></param>
    public void Set_Rect_Pos(Vector2 _pos)
    {
        m_RectTrans.anchorMax = new Vector2(.5f, .5f);
        m_RectTrans.anchorMin = new Vector2(.5f, .5f);
        m_RectTrans.localScale = new Vector3(1, 1, 1);
        m_RectTrans.anchoredPosition = _pos;
    }
    /// <summary>
    /// 클릭시 호출될 변수(현재 "Story0101"의 Stage 정보만을 호출중, 추후 선택한 Stage Data 정보의 Index Key값을 이용해 호출 예정)
    /// </summary>
    private void onClick_Btn()
    {
        m_Ready.Set_StageData(StageDataManager.Instance.Get_Stage_Data("Story0101"));
        m_Ready.SetWindow(this.transform.parent.gameObject);
        m_Ready.Enter();
    }
    /// <summary>
    /// Ready창 호출을 위한 Set 함수
    /// </summary>
    /// <param name="_win"></param>
    public void Set_Ready_Win(ReadyWindow _win) { m_Ready = _win; }
}
