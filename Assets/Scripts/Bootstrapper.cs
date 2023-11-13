using meta_core.Services;
using MVVM;
using MVVM.ViewModels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bootstrapper: MonoBehaviour
    {
        [SerializeField] private ServiceLocator m_serviceLocator;
        private void Awake()
        {
            var model = new Model();
            
            m_serviceLocator.Register(model);
            m_serviceLocator.Register(new SkillsViewModel(model));
        }
    }
}