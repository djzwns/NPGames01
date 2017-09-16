using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Redemption.UIAnimation
{
    #region Animation 관련 구조체
    [System.Serializable]
    public class Anima
    {
        public bool Active;     // 사용시 체크
        public float InDelay;   // Delay 후 애니메이션 동작.
        public float OutDelay;  
        public float Time;      // Time 시간동안 애니메이션 완료.
        public bool Loop;       // 반복동작 원할 때 체크.
    }

    [System.Serializable]
    public class Fade : Anima
    {
        public Color StartColor;
        public Color EndColor;
    }

    [System.Serializable]
    public class Move : Anima
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;
    }

    [System.Serializable]
    public class Scale : Anima
    {
        public Vector3 StartScale;
        public Vector3 EndScale;
    }


    #endregion // Animation 관련 구조체

    public class UIAnimator : MonoBehaviour
    {
        #region animation attribute
        [Header("Animation")]
        [SerializeField]
        private Fade m_fade;
        public Fade fade { get { return m_fade; } }

        [SerializeField]
        private Move m_move;
        public Move move { get { return m_move; } }

        [SerializeField]
        private Scale m_scale;
        public Scale scale { get { return m_scale; } }

        [Header("UGUI")]
        [SerializeField]
        private Image m_image;
        public Image image { get { return m_image; } }

        [SerializeField]
        private Text m_text;
        public Text text { get { return m_text; } }

        [SerializeField]
        private RectTransform m_transform;
        public RectTransform transform { get { return m_transform; } }
        #endregion // animation attribute

        #region 실행 함수
        public void Init()
        {
            m_image = GetComponent<Image>();
            m_text = GetComponent<Text>();
            m_transform = GetComponent<RectTransform>();

            FadeInit();
            MoveInit();
            ScaleInit();
        }

        public void Enter()
        {
            //UIAnimationManager.Instance.ButtonEnable(gameObject, true, 0.3f);
            UIAnimationManager.Instance.AnimIn(this);
        }

        public void Exit()
        {
            //UIAnimationManager.Instance.ButtonEnable(gameObject, false, 0.3f);
            UIAnimationManager.Instance.AnimOut(this);
        }
        #endregion // 실행 함수

        private void FadeInit()
        {
            if (!m_fade.Active) return;
            if (m_image != null)
            {
                m_fade.StartColor = m_image.color;
                m_image.color = m_fade.EndColor;
            }
            else
            {
                m_fade.StartColor = m_text.color;
                m_text.color = m_fade.EndColor;
            }

        }
        private void MoveInit()
        {
            if (!m_move.Active) return;

            m_move.StartPosition = m_transform.anchoredPosition;
            m_transform.anchoredPosition = m_move.EndPosition;
        }
        private void ScaleInit()
        {
            if (!m_scale.Active) return;

            m_scale.StartScale = m_transform.localScale;
            m_transform.localScale = m_scale.EndScale;
        }

    }
}