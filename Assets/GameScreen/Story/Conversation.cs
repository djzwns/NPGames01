using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace Redemption.Story
{
    // 스프라이트 위치
    public enum SPRITEPOS { LEFT, CENTER, RIGHT, NONE }

    [System.Serializable]
    public class Conversation
    {
        [Header("Graphics")]
        [SerializeField]
        private Sprite m_sprite;
        public Sprite sprite { get { return m_sprite; } }

        [SerializeField]
        private SPRITEPOS m_position;
        public SPRITEPOS position { get { return m_position; } }

        [SerializeField]
        private bool m_filp;
        public bool flip { get { return m_filp; } }

        [Header("Conversation")]
        [SerializeField]
        private string m_speaker;
        public string speaker { get { return m_speaker; } }

        [SerializeField]
        private string m_text;
        public string text { get { return m_text; } }        
    }
}