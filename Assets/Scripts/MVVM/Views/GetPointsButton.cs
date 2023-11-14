using meta_core.Services;
using MVVM.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM.Views
{
    /// <summary>
    /// Чит-кнопка получения скиллпоинтов
    /// </summary>
    public class GetPointsButton: MonoBehaviour
    {
        [SerializeField] private int m_pointsPerClick = 1;
        [SerializeField] private Button m_button;

        /// <summary>
        /// Вьюмодель способностей
        /// </summary>
        private SkillsViewModel m_skillsViewModel;
        
        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
                
            m_button.onClick.AddListener(OnClickAction);
        }

        /// <summary>
        /// Действие при нажатии на кнопку получения скиллпоинтов
        /// </summary>
        private void OnClickAction()
        {
            m_skillsViewModel.IncreaseSkillPoints(m_pointsPerClick);
        }
    }
}