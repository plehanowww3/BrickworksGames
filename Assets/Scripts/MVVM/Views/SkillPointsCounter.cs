using meta_core.Services;
using MVVM.ViewModels;
using TMPro;
using UnityEngine;

namespace MVVM.Views
{
    /// <summary>
    /// Счетчик скиллпоинтов
    /// </summary>
    public class SkillPointsCounter: MonoBehaviour
    {
        [SerializeField] private TMP_Text m_skillPointsText;

        /// <summary>
        /// Вьюмодель способностей
        /// </summary>
        private SkillsViewModel m_skillsViewModel;

        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
            m_skillsViewModel.skillPointChanged += OnSkillPointsChangedAction;
        }

        /// <summary>
        /// Действие при изменении кол-ва скиллпоинтов
        /// </summary>
        /// <param name="_newValue"></param>
        private void OnSkillPointsChangedAction(int _newValue)
        {
            m_skillPointsText.text = _newValue.ToString();
        }
    }
}