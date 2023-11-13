using System;
using System.Collections.Generic;
using UnityEngine;

namespace meta_core.Services
{
    /// <summary>
    /// Сервислокатор. Реализует DI
    /// </summary>
    internal class ServiceLocator : Singleton<ServiceLocator>
    {
        /// <summary>
        /// Хранилище сервисов. Ключ - имя типа сервиса, значение - функция получения объекта сервиса.
        /// </summary>
        private readonly Dictionary<string, Func<object>> m_services = new Dictionary<string, Func<object>>();

        /// <summary>
        /// Регистрирует объект сервиса. Делает это таким образом, что при вызове Resolve объект будет всегда один.
        /// </summary>
        /// <param name="_instance">Объект сервиса, который регист рируем</param>
        /// <typeparam name="T">Тип сервиса</typeparam>
        /// <returns></returns>
        public ServiceLocator Register<T>(T _instance)
        {
            var typeName = typeof(T).ToString();
            object Factory() { return _instance;}
            m_services[typeName] = Factory;
            return this;
        }

        /// <summary>
        /// Регистрирует фабричный метод объекта сервиса. На каждый вызов Resolve возвращает разный объект
        /// </summary>
        /// <param name="_factory">Фабричный метод, возвращающий объект сервиса</param>
        /// <typeparam name="T">Тип сервиса</typeparam>
        /// <returns></returns>
        public ServiceLocator Register<T>(Func<T> _factory) where T : class
        {
            var typeName = typeof(T).ToString();
            m_services[typeName] = _factory;
            return this;
        }

        /// <summary>
        /// Ищет сервис по типу и возвращает его. В случае ошибки поиска кидается исключение
        /// </summary>
        /// <typeparam name="T">Тип искомого сервиса</typeparam>
        /// <returns></returns>
        /// <exception cref="Exception">Исключение, вызванное отсутствием искомого сервиса</exception>
        public T Resolve<T>()
        {
            var typeName = typeof(T).ToString();
            if (m_services.TryGetValue(typeName, out var service))
            {
                return (T) service.Invoke();
            }

            throw new Exception($"Can`t find service with type {typeName}");
        }
        
        /// <summary>
        /// Инициализация сервислокатора
        /// </summary>
        /// <param name="_onInstanceReady">Действие по окончанию инициализации</param>
        public static void InstantiateServiceLocator(Action _onInstanceReady)
        {
            if (instance != null)
                return;

            var go = new GameObject(nameof(ServiceLocator));
            go.AddComponent<ServiceLocator>();

            m_onInstanceReady = _onInstanceReady;

            /*if (!MetaSdkAPI.appIdentifier.Equals(ExternalLibrariesConstants.TEST_APP_BUNDLE))
                go.hideFlags = HideFlags.HideInHierarchy; todo*/
        }

        /// <summary>
        /// Очищает сервислокатор
        /// </summary>
        public static void Reset()
        {
            if (instance == null)
                return;
            
            Destroy(instance.gameObject);

            m_onInstanceReady = null;
            
            instance = null;
        }
    }
}