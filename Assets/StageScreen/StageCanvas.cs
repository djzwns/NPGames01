using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public enum GameMode { NULL=0, STORY, STAGE, RAID}

public class StageCanvas : BaseUICanvas
{
    /// <summary>
    /// Stage Mode 선택을 위한 Btn
    /// </summary>
    [SerializeField]
    private Button[] m_Button;
    /// <summary>
    /// 각 모드별 UI를 호출하기위한 Window Obj
    /// </summary>
    [SerializeField]
    private BaseWindow[] m_GameMode;

    public override void Init()
    {
        ///뒤로 가기 버튼
        m_Button[0].onClick.AddListener(() => ProgramManager.Insatnce.Change_Scene(SceneName.TOWN));
        // 각 모드를 선택
        m_Button[1].onClick.AddListener(() => Show_Mode(GameMode.STORY));
        m_Button[2].onClick.AddListener(() => Show_Mode(GameMode.STAGE));
        m_Button[3].onClick.AddListener(() => Show_Mode(GameMode.RAID));

        //각 모드별 UI 초기화
        for (int i = 0; i < m_GameMode.Length; i++)
        {
            m_GameMode[i].Init();
        }
    }
    public override void Enter()
    {
        this.gameObject.SetActive(true);
        for (int i = 0; i < m_GameMode.Length; i++)
        {
            m_GameMode[i].Exit();
        }
    }
    public override void Play()
    {
    }
    public override void Exit()
    {
        this.gameObject.SetActive(false);
        for (int i = 0; i < m_GameMode.Length; i++)
        {
            m_GameMode[i].Exit();
        }
    }

    /// <summary>
    /// 선택한 Mode의 UI를 출력해주는 함수
    /// </summary>
    /// <param name="_Mode"></param>
    private void Show_Mode(GameMode _Mode)
    {
        for(int i = 0; i < m_GameMode.Length; i++)
        {
            m_GameMode[i].Exit();
        }
        m_GameMode[(int)_Mode - 1].Enter();
        Debug.Log(_Mode);
    }
}
