using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Redemption.Story
{
    public class ConversationManager : BaseObjManager
    {
        #region * 싱글톤
        /// <summary>
        /// Singleton script
        /// </summary>
        private static ConversationManager m_Instance;
        public static ConversationManager Instance
        {
            get
            {
                if (m_Instance != null) return m_Instance;
                else
                {
                    m_Instance = GameObject.FindObjectOfType<ConversationManager>();
                    return m_Instance;
                }
            }
        }
        #endregion // * 싱글톤

        #region 스토리 관련 속성
        [Header("Story")]
        [SerializeField]
        private ConversationScriptable m_story;

        [SerializeField]
        private PlayableDirector m_storyDirector;

        private int m_currentConversation;
        private int m_lastConversation;
        #endregion // 스토리 관련 속성
        
        /// <summary>
        /// 스토리 로드 함수. 시스템언어 설정에 맞는 데이터를 불러옴.
        /// </summary>
        /// <param name="_name">스토리 이름</param>
        public void LoadStory(string _name)
        {
            string path = "Story/";
            m_storyDirector.playableAsset = Resources.Load(path + _name) as PlayableAsset;

            switch (Application.systemLanguage)
            {
                case SystemLanguage.Korean:
                    path += "ko/" + _name;
                    break;
                default:
                    path += "en/" + _name;
                    break;
            }

            m_story = Resources.Load(path) as ConversationScriptable;
            Init();
        }

        public override void Init()
        {
            if (m_story == null) return;

            m_currentConversation = 0;
            m_lastConversation = m_story.Length;
        }

        public override void Enter()
        {

        }

        public override void Play()
        {

        }

        public override void Exit()
        {
        }

        public void NextConversation()
        {
            m_currentConversation = Mathf.Clamp(m_currentConversation + 1, 0, m_lastConversation);
        }

        public Conversation GetCurrentConversation()
        {
            return m_lastConversation == m_currentConversation ? null : m_story.GetConversation(m_currentConversation);
        }
    }
}