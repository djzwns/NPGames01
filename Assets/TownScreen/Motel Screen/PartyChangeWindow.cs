using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Party의 맴버가 교체될때 표시할 Window UI Class
/// (추후 PartyWindow_New Class에서 사용될 예정)
/// </summary>
public class PartyChangeWindow : MonoBehaviour
{
    [SerializeField]
    private Button[] m_Btn;
    [SerializeField]
    private Text m_MyText;

    public void Enter(MyHeroBtn _My, AllHeroBtn _All)
    {
        this.gameObject.SetActive(true);
        m_MyText.text = string.Format("[{0}]을 [{1}] 대신 파티에 참가시키겠습니까?", _All,_My);
    }
    public void Enter(HeroBtnData _data,bool cash)
    {
        this.gameObject.SetActive(true);
        string c;
        if (cash) c = "Cash";
        else c = "Coin";
        m_MyText.text = string.Format("[{0}]을 구매하시겠습니까?\n 현재 {3}보유량 : {1}\n 구매 후 {3}보유량: {2}", "선택영웅",1,0, c);
    }
    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
