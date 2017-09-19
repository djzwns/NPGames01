using UnityEngine;
using System.Collections;

public enum SceneName { MAIN = 0, TOWN ,STAGE , GAME, STORY, EXIT }

/// <summary>
///  Program Manager Class
/// </summary>
public class ProgramManager : MonoBehaviour
{
    /// <summary>
    /// Singleton Class instance
    /// </summary>
    private static ProgramManager m_Instance;
    public static ProgramManager Insatnce
    {
        set { m_Instance = value; }
        get { return m_Instance; }
    }

    /// <summary>
    /// Program Scene 들을 관리하는 Array
    /// </summary>
    [SerializeField]
    private BaseScene[] m_SceneList;
    /// <summary>
    /// 현재 구동중인 Scene을 관리하기 위한 변수
    /// </summary>
    [SerializeField]
    private BaseScene m_CurrentScene;

    void Awake()
    {
        DataTable.Instance.Init();
        PlayerData.Instance.Init();
        Insatnce = this;
        Screen.SetResolution(1280,720, false);
        for (int i = 0; i < m_SceneList.Length; i++)
        {
            m_SceneList[i].Init();
            m_SceneList[i].gameObject.SetActive(false);
        }
        m_CurrentScene = m_SceneList[(int)SceneName.MAIN];
        Change_Scene(SceneName.MAIN);
    }

    void Update()
    {
        PlayerData.Instance.Cash_Data();
        if (m_CurrentScene == null) return;

        m_CurrentScene.Play();
    }

    /// <summary>
    /// Scene Change를 위한 함수
    /// </summary>
    /// <param name="_temp"></param>
    public void Change_Scene(SceneName _temp)
    {
        if (m_CurrentScene == null) return;
        
        m_CurrentScene.gameObject.SetActive(false);
        m_CurrentScene.Exit();
        m_CurrentScene = m_SceneList[(int)_temp];
        m_CurrentScene.gameObject.SetActive(true);
        m_CurrentScene.Enter();
    }
}
