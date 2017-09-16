using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redemption.UIAnimation
{
    public class UIAnimationManager : Singleton<UIAnimationManager>
    {
        public void AnimIn(UIAnimator _ui)
        {
            FadeIn(_ui);
            MoveIn(_ui);
            ScaleIn(_ui);
        }

        public void AnimOut(UIAnimator _ui)
        {
            FadeOut(_ui);
            MoveOut(_ui);
            ScaleOut(_ui);
        }

        public void ButtonEnable(GameObject _obj,bool _enable, float _time = 0)
        {
            StartCoroutine(ButtonEnableTime(_obj, _enable, _time));
        }

        private IEnumerator ButtonEnableTime(GameObject _obj, bool _enable, float _time = 0)
        {
            _obj.GetComponent<UnityEngine.UI.Button>().interactable = !_enable;

            yield return new WaitForSeconds(_time);

            _obj.GetComponent<UnityEngine.UI.Button>().interactable = _enable;
        }

        #region 애니메이션 함수
        private void FadeIn(UIAnimator _ui)
        {
            if (!_ui.fade.Active) return;

            if (_ui.fade.Loop)
                StartCoroutine(_FadeLoop(_ui));
            else
                StartCoroutine(_FadeIn(_ui));
        }
        private void FadeOut(UIAnimator _ui)
        {
            if (!_ui.fade.Active) return;
            StartCoroutine(_FadeOut(_ui));
        }
        private void MoveIn(UIAnimator _ui)
        {
            if (!_ui.move.Active) return;

            if (_ui.move.Loop)
                StartCoroutine(_MoveLoop(_ui));
            else
                StartCoroutine(_MoveIn(_ui));
        }
        private void MoveOut(UIAnimator _ui)
        {
            if (!_ui.move.Active) return;
            StartCoroutine(_MoveOut(_ui));
        }
        private void ScaleIn(UIAnimator _ui)
        {
            if (!_ui.scale.Active) return;

            if (_ui.scale.Loop)
                StartCoroutine(_ScaleLoop(_ui));
            else
                StartCoroutine(_ScaleIn(_ui));
        }
        private void ScaleOut(UIAnimator _ui)
        {
            if (!_ui.scale.Active) return;
            StartCoroutine(_ScaleOut(_ui));
        }
        #endregion // 애니메이션 함수

        #region 애니메이션 코루틴 함수
        private IEnumerator _FadeIn(UIAnimator _ui)
        {
            yield return new WaitForSeconds(_ui.fade.InDelay);

            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime / _ui.fade.Time;

                if (_ui.image != null)
                    _ui.image.color = Color.Lerp(_ui.fade.EndColor, _ui.fade.StartColor, time);
                else
                    _ui.text.color = Color.Lerp(_ui.fade.EndColor, _ui.fade.StartColor, time);

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator _FadeOut(UIAnimator _ui)
        {
            yield return new WaitForSeconds(_ui.fade.OutDelay);

            float time = 0;
            Color color = _ui.image != null ? _ui.image.color : _ui.text.color;

            while (time < 1)
            {
                time += Time.deltaTime / _ui.fade.Time;

                if (_ui.image != null)
                    _ui.image.color = Color.Lerp(_ui.fade.StartColor, _ui.fade.EndColor, time);
                else
                    _ui.text.color = Color.Lerp(_ui.fade.StartColor, _ui.fade.EndColor, time);

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator _MoveIn(UIAnimator _ui)
        {
            yield return new WaitForSeconds(_ui.move.InDelay);

            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime / _ui.move.Time;

                _ui.transform.anchoredPosition = Vector2.Lerp(_ui.move.EndPosition, _ui.move.StartPosition, time);

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator _MoveOut(UIAnimator _ui)
        {
            yield return new WaitForSeconds(_ui.move.OutDelay);

            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime / _ui.move.Time;

                _ui.transform.anchoredPosition = Vector2.Lerp(_ui.move.StartPosition, _ui.move.EndPosition, time);

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator _ScaleIn(UIAnimator _ui)
        {
            yield return new WaitForSeconds(_ui.scale.InDelay);

            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime / _ui.scale.Time;

                _ui.transform.localScale = Vector2.Lerp(_ui.scale.EndScale, _ui.scale.StartScale, time);

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator _ScaleOut(UIAnimator _ui)
        {
            yield return new WaitForSeconds(_ui.scale.OutDelay);

            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime / _ui.scale.Time;

                _ui.transform.localScale = Vector2.Lerp(_ui.scale.StartScale, _ui.scale.EndScale, time);

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator _FadeLoop(UIAnimator _ui)
        {
            while (true)
            {
                yield return StartCoroutine(_FadeIn(_ui));
                yield return StartCoroutine(_FadeOut(_ui));
            }
        }

        private IEnumerator _MoveLoop(UIAnimator _ui)
        {
            while (true)
            {
                yield return StartCoroutine(_MoveIn(_ui));
                yield return StartCoroutine(_MoveOut(_ui));
            }
        }

        private IEnumerator _ScaleLoop(UIAnimator _ui)
        {
            while (true)
            {
                yield return StartCoroutine(_ScaleIn(_ui));
                yield return StartCoroutine(_ScaleOut(_ui));
            }
        }
        #endregion // 애니메이션 코루틴 함수.
    }
}