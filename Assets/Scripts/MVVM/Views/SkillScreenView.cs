using Helpers;
using meta_core.Services;
using MVVM.ViewModels;
using TMPro;
using UnityEngine;

namespace MVVM.Views
{
    /// <summary>
    /// Компонент экрана способностей
    /// </summary>
    public class SkillScreenView: MonoBehaviour
    {
        [SerializeField] private TMP_Text m_skillName;
        [SerializeField] private TMP_Text m_skillCost;
        [SerializeField] private DisableButton m_learnButton;
        [SerializeField] private DisableButton m_forgetButton;
        [SerializeField] private DisableButton m_forgetAllButton;
        [SerializeField] private BaseSkillNodeView m_baseSkillNodeView;

        /// <summary>
        /// Вьюмодель способностей
        /// </summary>
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

        /// <summary>
        /// Действие при изменении кол-ва скиллпоинтов
        /// </summary>
        /// <param name="_"></param>
        private void OnSkillPointsChanged(int _)
        {
            RedrawButtons();
        }

        /// <summary>
        /// Действие при изменении текущей выбранной способности
        /// </summary>
        /// <param name="_selectedSkill">Нода способности</param>
        public void OnSkillSelectedAction(SkillNode _selectedSkill)
        {
            m_skillName.text = SkillExtension.GetSkillName(_selectedSkill.skillType);
            m_skillCost.text = "Cost: " + _selectedSkill.cost.ToString();
            RedrawButtons();
        }

        /// <summary>
        /// Перерисовка кнопок связанных с изучением и забыванием
        /// </summary>
        private void RedrawButtons()
        {
            m_learnButton.SetDisable(!m_skillsViewModel.SelectedSkillCanBeLearned());
            m_forgetButton.SetDisable(!m_skillsViewModel.SelectedSkillCanBeForgotten());
        }

        /// <summary>
        /// Забыть все способности
        /// </summary>
        private void ForgetAllSkills()
        {
            m_skillsViewModel.ForgetAllSkills();
        }

        /// <summary>
        /// Изучить текущую выбранную способность
        /// </summary>
        private void LearnSkill()
        {
            m_skillsViewModel.LearnSelectedSkill();
        }
        /// <summary>
        /// Забыть текущую выбранную способность
        /// </summary>
        private void ForgetSkill()
        {
            m_skillsViewModel.ForgetSelectedSkill();
        }
    }
}