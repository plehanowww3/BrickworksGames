using System;
using UnityEngine;

/// <summary>
/// Реализация паттерна синглтон. Используется только для имплементации сервислокатора
/// </summary>
/// <typeparam name="T"></typeparam>
internal abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Инстанс синглтона
    /// </summary>
    public static T instance { get; set; } = null;

    /// <summary>
    /// Окончание инициализации
    /// </summary>
    protected static Action m_onInstanceReady;

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
            m_onInstanceReady?.Invoke();
            m_onInstanceReady = null;
            DontDestroyOnLoad(gameObject);
        }
    }
}