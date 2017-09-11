using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MainCanvas : BaseUICanvas
{
    private enum BTN_NAME { START = 0, EXIT }
    [SerializeField]
    private Button[] m_Button;

    public override void Init()
    {
        m_Button[(int)BTN_NAME.START].onClick.AddListener(() => ProgramManager.Insatnce.Change_Scene(SceneName.TOWN));
        m_Button[(int)BTN_NAME.EXIT].onClick.AddListener(() => Application.Quit() );
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