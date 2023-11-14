using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using MVVM.Views;

/// <summary>
/// Нода способности
/// </summary>
public class SkillNode
{
    /// <summary>
    /// Тип способности
    /// </summary>
    public SkillsEnum skillType;
    /// <summary>
    /// Стоимость изучения способности
    /// </summary>
    public int cost;
    /// <summary>
    /// Изучена ли способность
    /// </summary>
    public bool learned;
    /// <summary>
    /// Следующие способности связанные с этой способностью
    /// </summary>
    public List<SkillNodeView> previousNodes;
    /// <summary>
    /// Предыдущие способности ведущие к этой способности
    /// </summary>
    public List<SkillNodeView> nextNodes;
    /// <summary>
    /// Действие при забывании способности
    /// </summary>
    public Action onForgottenAction;
    /// <summary>
    /// Действие при изучении способности
    /// </summary>
    public Action onLearnedAction;
    
    /// <summary>
    /// Базовая нода способностей
    /// </summary>
    private SkillNode m_baseSkillNode;
    /// <summary>
    /// Идет ли эта способность напрямую из базовой ноды
    /// </summary>
    private bool m_previousIsBase;
    
    /// <summary>
    /// Нода способности
    /// </summary>
    /// <param name="_skillType">Тип способности</param>
    /// <param name="_cost">Стоимость изучения</param>
    /// <param name="_baseNode">Базовая нода</param>
    /// <param name="_previousNodes">Предыдущие ноды</param>
    /// <param name="_nextNodes">Следующие ноды</param>
    /// <param name="_previousIsBase">Связана ли напрямую с базовой нодой</param>
    public SkillNode(SkillsEnum _skillType, int _cost, SkillNode _baseNode, List<SkillNodeView> _previousNodes = null, List<SkillNodeView> _nextNodes = null, bool _previousIsBase = false)
    {
        skillType = _skillType;
        cost = _cost;
        m_baseSkillNode = _baseNode;
        previousNodes = _previousNodes;
        nextNodes = _nextNodes;
        m_previousIsBase = _previousIsBase;
    }

    /// <summary>
    /// Может ли способность быть изучена
    /// </summary>
    /// <returns></returns>
    public bool CanBeLearned()
    {
        if (learned)
            return false;

        if (m_previousIsBase)
            return true;
        
        var anyLearnPreviousSkills = previousNodes.Any(x => x.m_skillNode.learned);
        return anyLearnPreviousSkills;
    }

    /// <summary>
    /// Может ли способность быть забыта
    /// </summary>
    /// <returns></returns>
    public bool CanBeForgotten()
    {
        if (!learned)
            return false;
        
        if (nextNodes == null)
            return true;

        var anyLearnNextSkills = nextNodes.Any(x => x.m_skillNode.learned);
        return !anyLearnNextSkills;
    }
    
    /// <summary>
    /// Попытаться выучить способность
    /// </summary>
    /// <returns>Получилось ли выучить</returns>
    public bool TryLearnSkill()
    {
        if (CanBeLearned())
        {
            onLearnedAction?.Invoke();
            learned = true;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Попытаться забыть способность
    /// </summary>
    /// <returns>Получилось ли забыть</returns>
    public bool TryForgetSkill()
    {
        if (CanBeForgotten())
        {
            onForgottenAction?.Invoke();
            learned = false;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Насильно забыть способность 
    /// </summary>
    public void ForceForget()
    {
        onForgottenAction?.Invoke();
        learned = false;
    }
}