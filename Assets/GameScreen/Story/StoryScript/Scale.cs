using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private Vector3 m_earlyScale;
    private Vector3 m_lastScale;

    void Start()
    {
        m_earlyScale = transform.localScale;
        m_lastScale = m_earlyScale + m_earlyScale * 0.1f;

        StartCoroutine(ScaleLoop());
    }

    IEnumerator ScaleUp()
    {
        float time = 0;
        while (!transform.localScale.Equals(m_lastScale))
        {
            yield return null;
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(m_earlyScale, m_lastScale, time);
        }
    }

    IEnumerator ScaleDown()
    {
        float time = 0;
        while (!transform.localScale.Equals(m_earlyScale))
        {
            yield return null;
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(m_lastScale, m_earlyScale, time);
        }
    }

    IEnumerator ScaleLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ScaleUp());
            yield return StartCoroutine(ScaleDown());
        }
    }
}
