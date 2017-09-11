using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stage의 Wave Data Class
/// </summary>
[SelectionBase]
public class WaveData
{
    /// <summary>
    /// Wave Monster Index ID
    /// </summary>
    public string[] MonID;
    /// <summary>
    /// Wave Monster Count
    /// </summary>
    public int[] Count;
    /// <summary>
    /// Wave Monster Lv
    /// </summary>
    public float[] Lv;
    
    /// <summary>
    /// 초기화
    /// </summary>
    public void Enter()
    {
        MonID = new string[0]; Count = new int[0]; Lv = new float[0];
    }
    /// <summary>
    /// Wave Data에 Monster를 넣어주는 함수
    /// </summary>
    /// <param name="_Id"></param>
    /// <param name="_Count"></param>
    /// <param name="_Lv"></param>
    public void Add_Mon(string _Id, string _Count, string _Lv)
    {
        // 오류값이 있을때 넣지 않음
        if(_Id == "" || _Id == "N/A"  ||
            _Count == "" || _Count == "N/A" ||
            _Lv == "" || _Lv == "N/A" )
        {
            return;
        }

        // Wave 몬스터개체수를 추가하기위해 배열 크기 재정의
        string[] _tempID = new string[MonID.Length + 1];
        int[] _tempCount = new int[Count.Length + 1];
        float[] _tempLv = new float[Lv.Length + 1];
        
        // 기존 Monster 정보를 다시 받아옴
        for (int i = 0; i < MonID.Length; i++)
        {
            _tempID[i] = MonID[i];
            _tempCount[i] = Count[i];
            _tempLv[i] = Lv[i];
        }

        // 추가된 Monster 정보를 넣어주는 곳
        _tempID[_tempID.Length - 1] = _Id;
        _tempCount[_tempCount.Length - 1] = int.Parse(_Count);
        _tempLv[_tempCount.Length - 1] = float.Parse(_Lv);
        
        // 임시 Array들을 Wave Data에 넣는 곳
        MonID = _tempID;
        Count = _tempCount;
        Lv = _tempLv;
    }
}

/// <summary>
/// Stage Data Class
/// </summary>
[SelectionBase]
public class StageData
{
    /// <summary>
    /// Stage Index ID
    /// </summary>
    public string StageID;
    /// <summary>
    /// Stage Number 
    /// </summary>
    public int StageNumber;
    /// <summary>
    /// Stage Name
    /// </summary>
    public string StageName;
    /// <summary>
    /// Stage WaveData Array
    /// </summary>
    public WaveData[] m_Wave;
    /// <summary>
    /// Stage Clear Exp 
    /// </summary>
    public float RewardExp;
    /// <summary>
    /// Stage Clear Silling
    /// </summary>
    public float RewardSilling;
    /// <summary>
    /// Stage Clear Ruby
    /// </summary>
    public float RewardRuby;
    /// <summary>
    /// Stage Clear CP
    /// </summary>
    public float RewardCP;
    /// <summary>
    /// Stage Clear Char(영웅 보상)
    /// </summary>
    public string RewardChar;

    /// <summary>
    /// Wave Data를 추가하는 함수
    /// </summary>
    /// <param name="_data"></param>
    public void Add_Wave(WaveData _data)
    {
        //Wave Data를 추가하기위해 임시 배열을 생성
        WaveData[] _temp = new WaveData[m_Wave.Length+1];

        // 기존 Wave Data를 temp 값으로 가져옴
        for (int i = 0;i < m_Wave.Length; i++)
        {
            _temp[i] = m_Wave[i];
        }
        // 임시 배열에 새로운 WaveData를 추가
        _temp[m_Wave.Length] = _data;

        // 임시 배열을 StageData에 넣어줌
        m_Wave = _temp;
    }
}

public class StageDataManager : MonoBehaviour
{
    /// <summary>
    /// Singleton Class 생성을 위한 Instance
    /// </summary>
    [SerializeField]
    private static StageDataManager m_Instance;
    [SerializeField]
    public static StageDataManager Instance
    {
        get { return m_Instance; }
        set { m_Instance = value; }
    }
    /// <summary>
    /// Stage Monster Data (구)
    /// </summary>
    [SerializeField]
    private List<string[]> m_StageMonsterData;
    /// <summary>
    /// Stage Data List
    /// </summary>
    [SerializeField]
    private Dictionary<string, StageData> m_StageDataList;
    /// <summary>
    /// Wave Data를 수정하기 위한 변수
    /// </summary>
    private WaveData _Wave = new WaveData();

    void Awake()
    {
        Instance = this;
        m_StageMonsterData = new List<string[]>(0);
        m_StageDataList = new Dictionary<string, StageData>(0);
        // Stage Data를 가져오는 함수(구)
        Instance.Load_Stage_Data();
        // Stage Data를 가져오는 함수
        New_Load_Stage_Data();
    }
    /// <summary>
    /// Stage Data를 가져오는 함수(구)
    /// </summary>
    private void Load_Stage_Data()
    {
        TextAsset m_Text = Resources.Load("StageData") as TextAsset;
        string[] Line_arr = m_Text.text.Split('\n');

        for (int i = 0; i < (int)Line_arr.Length; i++)
        {
            string[] Stage_arr = Line_arr[i].Split('\t');
            m_StageMonsterData.Add(Stage_arr);
        }
    }
    /// <summary>
    /// Stage Data를 반환하는 함수(구)
    /// </summary>
    /// <param name="_stageNum"></param>
    /// <returns></returns>
    public string[] Set_Stage_Data(int _stageNum)
    {
        return m_StageMonsterData[_stageNum];
    }

    /// <summary>
    /// Stage Data를 반환하는 함수
    /// </summary>
    /// <param name="_StageID"></param>
    /// <returns></returns>
    public StageData Get_Stage_Data(string _StageID)
    {
        StageData value;
        // Stage Index ID를 사용하여 있는지 Check 후 반환
        if (m_StageDataList.TryGetValue(_StageID, out value))
        {
            return value;
        }
        else
        {
            Debug.Log("스테이지데이터가 존재하지 않습니다.");
            return null;
        }
    }
    /// <summary>
    /// StageData Class를 사용한 Stage Data Load 함수
    /// </summary>
    private void New_Load_Stage_Data()
    {
        // 저장된 Stage Data를 가져옴
        TextAsset m_Text = Resources.Load("NewStageData") as TextAsset;
        // Line 별로 Array
        string[] Line_arr = m_Text.text.Split('\n');

        // Line 별 StageData 정리
        for (int i = 0; i < (int)Line_arr.Length; i++)
        {
            // 임시 StageData
            StageData Temp = new StageData();
            Temp.m_Wave = new WaveData[0];

            string[] Stage_arr = Line_arr[i].Split('\t');
            
            Temp.StageID = Stage_arr[0];  // Stage Index ID 
            Temp.StageNumber = int.Parse(Stage_arr[1]); // Stage Number 
            Temp.StageName = Stage_arr[2];  // Stage Name
            // 1웨이브
            _Wave.Enter(); // WaveData 초기화 
            for (int count=0; count < 3; count++)
            {
                // Wave Monster 추가
                _Wave.Add_Mon(Stage_arr[4 + (count * 4)], Stage_arr[5 + (count * 4)], Stage_arr[6 + (count * 4)]);
            }
            Temp.Add_Wave(_Wave);
            _Wave = new WaveData();
            // 2웨이브
            _Wave.Enter();
            for (int count = 0; count < 3; count++)
            {
                _Wave.Add_Mon(Stage_arr[17 + (count * 4)], Stage_arr[18 + (count * 4)], Stage_arr[19 + (count * 4)]);
            }
            Temp.Add_Wave(_Wave);
            _Wave = new WaveData();
            // 3웨이브
            _Wave.Enter();
            for (int count = 0; count < 3; count++)
            {
                _Wave.Add_Mon(Stage_arr[30 + (count * 4)], Stage_arr[31 + (count * 4)], Stage_arr[32 + (count * 4)]);
            }
            Temp.Add_Wave(_Wave);
            _Wave = new WaveData();
            // 보스 웨이브
            _Wave.Enter();
            _Wave.Add_Mon(Stage_arr[43], Stage_arr[44], Stage_arr[45]); // Boss Monster Data 추가
            Temp.Add_Wave(_Wave);
            _Wave = new WaveData();

            Temp.RewardExp = float.Parse(Stage_arr[47]);
            Temp.RewardSilling = float.Parse(Stage_arr[48]);
            Temp.RewardRuby = float.Parse(Stage_arr[49]);
            Temp.RewardCP = float.Parse(Stage_arr[50]);
            Temp.RewardChar = Stage_arr[51];

            // Dictionary에 저장
            m_StageDataList.Add(Temp.StageID,Temp);
        }
    }

    /// <summary>
    /// 저장된 StageData가 올바르게 저장되었는지 출력하기 위해 만든 함수
    /// </summary>
    /// <param name="_data"></param>
    private void Show_StageData(StageData _data)
    {
        Debug.Log(string.Format("ID : {0}\nName : {1}\nNumber : {2}", _data.StageID, _data.StageName, _data.StageNumber));
        for (int j = 0; j < _data.m_Wave.Length; j++)
        {
            Debug.Log((j + 1) + "웨이브");
            for (int k = 0; k < _data.m_Wave[j].Count.Length; k++)
            {
                Debug.Log(string.Format("MonID : {0}\nCount : {1}\nLevel : {2}",
                    _data.m_Wave[j].MonID[k],
                    _data.m_Wave[j].Count[k],
                    _data.m_Wave[j].Lv[k]));
            }
        }
    }
}