using System;
using meta_core.Services;
using MVVM.ViewModels;
using TMPro;
using UnityEngine;

namespace MVVM.Views
{
    public class SkillPointsCounter: MonoBehaviour
    {
        [SerializeField] private TMP_Text m_skillPointsText;

        private SkillsViewModel m_skillsViewModel;

        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
            m_skillsViewModel.skillPointChanged += OnSkillPointsChangedAction;
        }

        private void OnSkillPointsChangedAction(int _newValue)
        {
            m_skillPointsText.text = _newValue.ToString();
        }
    }
}