using System.Collections.Generic;
using Enums;
using meta_core.Services;
using MVVM.ViewModels;
using UnityEngine;

namespace MVVM.Views
{
    public class BaseSkillNodeView: MonoBehaviour
    {
        [SerializeField] private List<SkillNodeView> m_nextSkills;
        
        public SkillNode m_skillNode;
        
        private SkillsEnum m_skillType = SkillsEnum.BASE_SKILL;
        private SkillsViewModel m_skillsViewModel;

        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
            Init();
            
            m_skillsViewModel.SetBaseSkill(m_skillNode);
            m_skillsViewModel.SelectSkill(m_skillNode);
        }

        public void Init()
        {
            var nextSkillNodes = new List<SkillNodeView>();
            m_skillNode = new SkillNode(m_skillType, 0, null, nextSkillNodes);
            m_skillNode.learned = true;
        }
    }
}