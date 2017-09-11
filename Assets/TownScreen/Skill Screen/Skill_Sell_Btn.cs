using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill_Sell_Btn : MonoBehaviour
{
    [SerializeField]
    private Button[] m_SellBtn;
    private float[] m_Value;

    public void Init()
    {
        m_Value = new float[2];
        //m_SellBtn[0].onClick.AddListener(() =>);
        //m_SellBtn[1].onClick.AddListener(() =>);
    }
    public void Enter(float _V1, float _V2)
    {
        m_Value[0] = _V1;
        m_Value[0] = _V2;
    }
    public void Sell_Value(bool _is)
    {
        if(_is)
        {

        }
    }
}
