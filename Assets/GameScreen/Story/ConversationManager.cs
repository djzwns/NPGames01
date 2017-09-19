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

        #region 스토리 관련 어트리뷰트
        [Header("Story")]
        [SerializeField]
        private ConversationScriptable m_story;

        [SerializeField]
        private GameObject m_Canvas;
        private GameObject m_storyObj;
        private PlayableDirector m_storyDirector;

        private int m_currentConversation;
        private int m_lastConversation;

        private string m_storyPath;
        #endregion // 스토리 관련 어트리뷰트
        
        /// <summary>
        /// 스토리 로드 함수. 시스템언어 설정에 맞는 데이터를 불러옴.
        /// </summary>
        /// <param name="_name">스토리 이름</param>
        public void LoadStory(string _name)
        {
            string path = "Story/";
            m_storyPath = path + _name;
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
        public double GetStoryDuration()
        {
            return m_storyDirector.duration;
        }
        
        public override void Init()
        {
            if (m_story == null) return;

            m_currentConversation = 0;
            m_lastConversation = m_story.Length;
        }

        public override void Enter()
        {
            m_storyObj = Instantiate(Resources.Load(m_storyPath), m_Canvas.transform) as GameObject;
            m_storyDirector = m_storyObj.GetComponent<PlayableDirector>();
            m_storyDirector.Play();
        }

        public override void Play()
        {

        }

        public override void Exit()
        {
            Destroy(m_storyObj);
            m_storyDirector = null;
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