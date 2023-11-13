using System;
using System.Collections.Generic;
using Helpers;
using meta_core.Services;
using MVVM.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM.Views
{
    public class SkillScreenView: MonoBehaviour
    {
        [SerializeField] private TMP_Text m_skillName;
        [SerializeField] private TMP_Text m_skillCost;
        [SerializeField] private DisableButton m_learnButton;
        [SerializeField] private DisableButton m_forgetButton;
        [SerializeField] private DisableButton m_forgetAllButton;

        [SerializeField] private BaseSkillNodeView m_baseSkillNodeView;
        [SerializeField] private List<SkillNodeView> m_skillNodeComponents;

        private SkillsViewModel m_skillsViewModel;

        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
            
            m_baseSkillNodeView.Init();

            m_skillsViewModel.onSelectedSkillAction += OnSkillSelectedAction;
            m_skillsViewModel.skillPointChanged += OnSkillPointsChanged;
            m_learnButton.AddActionOnClick(LearnSkill);
            m_forgetButton.AddActionOnClick(ForgetSkill);
            m_forgetAllButton.AddActionOnClick(ForgetAllSkills);
        }

        private void OnSkillPointsChanged(int _)
        {
            RedrawButtons();
        }

        public void OnSkillSelectedAction(SkillNode _selectedSkill)
        {
            m_skillName.text = SkillExtension.GetSkillName(_selectedSkill.skillName);
            m_skillCost.text = "Cost: " + _selectedSkill.cost.ToString();
            RedrawButtons();
        }

        private void RedrawButtons()
        {
            m_learnButton.SetDisable(!m_skillsViewModel.SelectedSkillCanBeLearned());
            m_forgetButton.SetDisable(!m_skillsViewModel.SelectedSkillCanBeForgotten());
            m_forgetAllButton.SetDisable(!m_skillsViewModel.AnySkillLearned());
        }

        private void ForgetAllSkills()
        {
            m_skillsViewModel.ForgetAllSkills();
        }

        private void LearnSkill()
        {
            m_skillsViewModel.LearnSelectedSkill();
        }
        private void ForgetSkill()
        {
            m_skillsViewModel.ForgetSelectedSkill();
        }
    }
}