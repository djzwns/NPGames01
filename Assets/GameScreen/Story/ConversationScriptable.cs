using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redemption.Story
{
    [CreateAssetMenu(menuName = "Story/Conversation")]
    public class ConversationScriptable : ScriptableObject
    {
        [SerializeField]
        private Conversation[] m_conversations;

        public int Length { get { return m_conversations.Length; } }

        public Conversation GetConversation(int _index)
        {
            return m_conversations[_index];
        }
    }
}