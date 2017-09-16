using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton 부모 클래스
/// 싱글톤 필요시 상속받아 사용
/// ex) class NeedSingleton : Singleton<NeedSingleton> {}
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T m_Instance;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)         
            {
                m_Instance = FindObjectOfType(typeof(T)) as T;
                if (FindObjectsOfType(typeof(T)).Length > 1) return null;
                return m_Instance;
            }
            return m_Instance;
        }
    }
}
