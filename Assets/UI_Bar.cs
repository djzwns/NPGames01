using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Hp 등의 UI를 표기하기 위한 UI Class
/// </summary>
public class UI_Bar : MonoBehaviour
{
    [SerializeField]
    private float m_fill;
    [SerializeField]
    private Image m_valueImage;
    [SerializeField]
    private float m_Max;
    [SerializeField]
    private float m_Now;
    private RectTransform m_Rect;
    
    void Update()
    {
        Play();
    }

    public void Play()
    {
        Bar_Update();
    }

    private void Bar_Update()
    {
        m_fill = Map(m_Now, 0, m_Max, 0, 1);
        m_valueImage.fillAmount = m_fill;
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin);
    }
    /// <summary>
    /// Max값을 설정
    /// </summary>
    /// <param name="_value"></param>
    public void Set_MaxValue(float _value)
    {
        m_Rect = GetComponent<RectTransform>();
        m_Max = _value;
    }
    /// <summary>
    /// 현재 값을 설정
    /// </summary>
    /// <param name="_value"></param>
    public void Set_NowValue(float _value) { m_Now = _value; }
    /// <summary>
    /// 좌우 반전(캐릭터에 종속되어있을떄)
    /// </summary>
    /// <param name="_is"></param>
    public void Filp(bool _is)
    {
        if(!_is)
        {
            m_Rect.localEulerAngles = new Vector3(0, 0, 0);
        }
        else m_Rect.localEulerAngles = new Vector3(0, 180, 0);
    }
}
