using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace MVVM.ViewModels
{
    /// <summary>
    /// Вьюмодель способностей
    /// </summary>
    public class SkillsViewModel
    {
        /// <summary>
        /// Нодовый список всех способностей
        /// </summary>
        public List<SkillNode> skillNodeComponents;
        /// <summary>
        /// Выбранная способность
        /// </summary>
        public SkillNode m_selectedSkill;
        /// <summary>
        /// Кол-во скиллпоинтов у игрока
        /// </summary>
        public int skillPoints => m_model.skillPoints;
        /// <summary>
        /// Действие при выборе способности
        /// </summary>
        public Action<SkillNode> onSelectedSkillAction;
        /// <summary>
        /// Действи при изменении кол-ва скиллпоинтов
        /// </summary>
        public Action<int> skillPointChanged;
        /// <summary>
        /// Базовая нода способностей
        /// </summary>
        public SkillNode baseSkillNode;
        /// <summary>
        /// Модель данных игрока
        /// </summary>
        private Model m_model;

        public SkillsViewModel(Model _model)
        {
            m_model = _model;
            skillNodeComponents = new List<SkillNode>();
        }

        /// <summary>
        /// Выбрать способность
        /// </summary>
        /// <param name="_selectedSkill">Нода способности</param>
        public void SelectSkill(SkillNode _selectedSkill)
        {
            m_selectedSkill = _selectedSkill;
            onSelectedSkillAction?.Invoke(_selectedSkill);
        }

        /// <summary>
        /// Установить базовую ноду способностей
        /// </summary>
        /// <param name="_baseSkillNode">Базовая нода</param>
        public void SetBaseSkill(SkillNode _baseSkillNode)
        {
            baseSkillNode = _baseSkillNode;
        }

        /// <summary>
        /// Добавить ноду способностей в общий список
        /// </summary>
        /// <param name="_skill">Нода способности</param>
        public void AddSkill(SkillNode _skill)
        {
            if (skillNodeComponents.Contains(_skill))
                Debug.Log($"Skill with name {SkillExtension.GetSkillName(_skill.skillType)} already exists");
            else
                skillNodeComponents.Add(_skill);
        }

        /// <summary>
        /// Увеличить скиллпоинты
        /// </summary>
        /// <param name="_addPoints">На сколько увеличить</param>
        public void IncreaseSkillPoints(int _addPoints)
        {
            m_model.skillPoints += _addPoints;
            skillPointChanged.Invoke(skillPoints);
        }
        
        /// <summary>
        /// Уменьшить кол-во скиллпоинтов
        /// </summary>
        /// <param name="_decreasePoints">На сколько уменьшить</param>
        public void DecreaseSkillPoints(int _decreasePoints)
        {
            if (_decreasePoints <= m_model.skillPoints)
            {
                m_model.skillPoints -= _decreasePoints;
                skillPointChanged.Invoke(skillPoints);
            }
        }
        
        /// <summary>
        /// Забыть все изученые способности
        /// </summary>
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

        /// <summary>
        /// Изучить текущую выбранную способность
        /// </summary>
        public void LearnSelectedSkill()
        {
            if (SelectedSkillCanBeLearned())
            {
                m_selectedSkill.TryLearnSkill();
                DecreaseSkillPoints(m_selectedSkill.cost);
            }
        }
        
        /// <summary>
        /// Забыть текущую выбранную способность
        /// </summary>
        public void ForgetSelectedSkill()
        {
            if (SelectedSkillCanBeForgotten())
            {
                m_selectedSkill.TryForgetSkill();
                IncreaseSkillPoints(m_selectedSkill.cost);
            }
        }
        
        /// <summary>
        /// Может ли текущая выбранная способность быть изучена
        /// </summary>
        /// <returns></returns>
        public bool SelectedSkillCanBeLearned()
        {
            return skillPoints >= m_selectedSkill.cost && m_selectedSkill.CanBeLearned();
        }
        
        /// <summary>
        /// Может ли текущая выбранная способность быть забыта
        /// </summary>
        /// <returns></returns>
        public bool SelectedSkillCanBeForgotten()
        {
            return m_selectedSkill.CanBeForgotten();
        }
    }
}