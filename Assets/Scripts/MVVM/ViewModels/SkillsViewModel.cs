using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

namespace MVVM.ViewModels
{
    public class SkillsViewModel
    {
        public List<SkillNode> skillNodeComponents;
        public SkillNode m_selectedSkill;
        public int skillPoints => m_model.skillPoints;
        public Action<SkillNode> onSelectedSkillAction;
        public Action<int> skillPointChanged;
        
        private Model m_model;
        public SkillNode baseSkillNode;

        public SkillsViewModel(Model _model)
        {
            m_model = _model;
            skillNodeComponents = new List<SkillNode>();
        }

        public void SelectSkill(SkillNode _selectedSkill)
        {
            m_selectedSkill = _selectedSkill;
            onSelectedSkillAction?.Invoke(_selectedSkill);
        }

        public void SetBaseSkill(SkillNode _baseSkillNode)
        {
            baseSkillNode = _baseSkillNode;
        }

        public void AddSkill(SkillNode _skill)
        {
            if (skillNodeComponents.Contains(_skill))
                Debug.Log($"Skill with name {SkillExtension.GetSkillName(_skill.skillName)} already exists");
            else
                skillNodeComponents.Add(_skill);
        }

        public void IncreaseSkillPoints(int _addPoints)
        {
            m_model.skillPoints += _addPoints;
            skillPointChanged.Invoke(skillPoints);
        }
        
        public void DecreaseSkillPoints(int _decreasePoints)
        {
            if (_decreasePoints <= m_model.skillPoints)
            {
                m_model.skillPoints -= _decreasePoints;
                skillPointChanged.Invoke(skillPoints);
            }
        }
        
        public void ForgetAllSkills()
        {
            int backSkillPoints = 0;
            
            foreach (var skillNode in skillNodeComponents)
            {
                if (skillNode.learned)
                {
                    skillNode.ForceForget();
                    backSkillPoints += skillNode.cost;
                }
            }
                
            IncreaseSkillPoints(backSkillPoints);
        }

        public void LearnSelectedSkill()
        {
            if (SelectedSkillCanBeLearned())
            {
                m_selectedSkill.TryLearnSkill();
                DecreaseSkillPoints(m_selectedSkill.cost);
            }
        }
        
        public void ForgetSelectedSkill()
        {
            if (SelectedSkillCanBeForgotten())
            {
                m_selectedSkill.TryForgetSkill();
                IncreaseSkillPoints(m_selectedSkill.cost);
            }
        }
        
        public bool SelectedSkillCanBeLearned()
        {
            return skillPoints >= m_selectedSkill.cost && m_selectedSkill.CanBeLearned();
        }
        
        public bool SelectedSkillCanBeForgotten()
        {
            return m_selectedSkill.CanBeForgotten();
        }
        
        public bool AnySkillLearned()
        {
            return skillNodeComponents.Any(x => x.learned);
        }
    }
}