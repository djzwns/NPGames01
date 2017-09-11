using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill_Upg_Btn : MonoBehaviour
{
    private enum SKILLTYPE { UP, TP }
    private SKILLTYPE m_Type;
    private SkillUpgradWindow m_baseWin;
    private SkillTPWindow m_baseWin2;
    public RectTransform m_Rect;
    public Image[] m_Images;
    public Button[] m_SkillBtn;
    private HeroBtnData m_BtnData;
    private bool is_Active;
    private int m_Index;

    public void Init(SkillUpgradWindow _Win, int m)
    {
        m_Type = SKILLTYPE.UP;
        m_baseWin = _Win;
        m_Index = m;
    }
    public void Init(SkillTPWindow _Win, int m)
    {
        m_Type = SKILLTYPE.UP;
        m_baseWin2 = _Win;
        m_Index = m;
    }

    public RectTransform Get_RectTransform() { return m_Rect; }
    public void Set_Data(HeroBtnData _Data) { m_BtnData = _Data; }
    public HeroBtnData Get_Data() { return m_BtnData; }
    public int Get_Numbe() { return m_Index; }
    public bool Get_Active() { return is_Active; }
}