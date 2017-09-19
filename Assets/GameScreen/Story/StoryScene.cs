using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redemption.Story
{
    public class StoryScene : BaseScene
    {
        public override void Init()
        {
            for (int i = 0; i < m_ObjManager.Length; i++)
            {
                m_ObjManager[i].Init();
            }
            m_Canvas.Init();
        }

        public override void Enter()
        {
            for (int i = 0; i < m_ObjManager.Length; i++)
            {
                m_ObjManager[i].Enter();
            }
            m_Canvas.Enter();
        }

        public override void Play()
        {
            //m_Canvas.Play();
            for (int i = 0; i < m_ObjManager.Length; i++)
            {
                m_ObjManager[i].Play();
            }
        }

        public override void Exit()
        {
            m_Canvas.Exit();
            for (int i = 0; i < m_ObjManager.Length; i++)
            {
                m_ObjManager[i].Exit();
            }
        }
    }
}