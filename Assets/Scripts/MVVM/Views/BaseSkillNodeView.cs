using System.Collections.Generic;
using Enums;
using meta_core.Services;
using MVVM.ViewModels;
using UnityEngine;

namespace MVVM.Views
{
    /// <summary>
    /// Компонент базовой ноды дерева способностей
    /// </summary>
    public class BaseSkillNodeView: MonoBehaviour
    {
        
        [SerializeField] private List<SkillNodeView> m_nextSkills;
        
        /// <summary>
        /// Нода базовой способности
        /// </summary>
        public SkillNode m_skillNode;
        /// <summary>
        /// Тип способности
        /// </summary>
        private SkillsEnum m_skillType = SkillsEnum.BASE_SKILL;
        /// <summary>
        /// Вьюмодель способностей
        /// </summary>
        private SkillsViewModel m_skillsViewModel;

        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
            Init();
            
            m_skillsViewModel.SetBaseSkill(m_skillNode);
            m_skillsViewModel.SelectSkill(m_skillNode);
        }

        /// <summary>
        /// Инициализация базовой ноды дерева способностей
        /// </summary>
        public void Init()
        {
            var nextSkillNodes = new List<SkillNodeView>();
            m_skillNode = new SkillNode(m_skillType, 0, null, nextSkillNodes);
            m_skillNode.learned = true;
        }
    }
}