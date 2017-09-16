using System;
using UnityEngine;
using UnityEngine.UI;
using Redemption.UIAnimation;

namespace Redemption.Story
{
    /// <summary>
    /// * 스토리 캔버스 
    ///     - 스토리 시작시 작동할 UI들을 관리.
    /// </summary>
    public class StoryCanvas : BaseUICanvas
    {
        private enum STORYBTN { NEXT, SKIP }

        [Header("Canvas UI")]
        [SerializeField]
        private Button[] m_Btn;

        [SerializeField]
        private Image[] m_illustrations;

        [SerializeField]
        private Text m_speaker;

        [SerializeField]
        private Text m_text;

        [SerializeField]
        private Image m_fade;

        [Header("Window")]
        [SerializeField]
        private GameObject m_ConversationBox;
        [SerializeField]
        private GameObject m_ConversationPanel;
        
        private Color m_downton = new Color(0.3f, 0.3f, 0.3f, 1);

        #region AnimationUI
        public UIAnimator FadeUI;
        public UIAnimator ConversationBoxUI;
        #endregion // AnimationUI

        public override void Init()
        {
            m_Btn[(int)STORYBTN.NEXT].onClick.AddListener(() => { Play(); });
            m_Btn[(int)STORYBTN.SKIP].onClick.AddListener(() => { Exit(); });

            FadeUI.Init();
            ConversationBoxUI.Init();

            //m_ConversationPanel.SetActive(false);
            //gameObject.SetActive(false);
        }

        //private void SetUIActive(bool _active)
        //{
        //    m_ConversationBox.SetActive(_active);
        //}

        private void ResetIllust()
        {
            for (int i = 0; i < m_illustrations.Length; ++i)
            {
                m_illustrations[i].sprite = null;
                m_illustrations[i].color = Color.white;
                m_illustrations[i].transform.rotation = Quaternion.identity;
            }
        }

        private void SetIllust(Sprite _sprite, int _pos, bool _flip)
        {
            for (int i = 0; i < m_illustrations.Length; ++i)
            {
                if (i == _pos)
                {
                    m_illustrations[_pos].sprite = _sprite;
                    m_illustrations[_pos].color = Color.white;
                }
                else
                {
                    if (m_illustrations[i].sprite != null)
                        m_illustrations[i].color = m_downton;
                    else
                        m_illustrations[i].color = Color.clear;
                }                
            }

            int rot = _flip ? 90 : 0;
            m_illustrations[_pos].transform.rotation = new Quaternion(0, rot, 0, 0);
        }

        private void UpdateUI()
        {
            Conversation conversation = ConversationManager.Instance.GetCurrentConversation();

            if (conversation == null) { Exit(); return; }

            SetIllust(conversation.sprite, (int)conversation.position, conversation.flip);
            m_speaker.text = conversation.speaker;
            m_text.text = conversation.text;
        }

        public override void Enter()
        {
            ResetIllust();
            UpdateUI();
            FadeUI.Enter();
            ConversationBoxUI.Enter();
        }

        public override void Play()
        {
            ConversationManager.Instance.NextConversation();
            UpdateUI();
        }

        public override void Exit()
        {
            FadeUI.Exit();
            ConversationBoxUI.Exit();
            ConversationManager.Instance.Exit();
        }
        
        //void OnEnable()
        //{
        //    m_ConversationPanel.SetActive(true);
        //}
        //void OnDisable()
        //{
        //    Play();
        //    m_ConversationPanel.SetActive(false);
        //}
    }
}