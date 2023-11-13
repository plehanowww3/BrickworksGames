using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using MVVM.Views;

public class SkillNode
{
    public SkillsEnum skillName;
    public int cost;
    public bool learned;
    public List<SkillNodeView> previousNodes;
    public List<SkillNodeView> nextNodes;
    public Action onForgottenAction;
    public Action onLearnedAction;

    private SkillNode m_baseSkillNode;
    private bool m_previousIsBase;
    public SkillNode(SkillsEnum _skillName, int _cost, SkillNode _baseNode, List<SkillNodeView> _previousNodes = null, List<SkillNodeView> _nextNodes = null, bool _previousIsBase = false)
    {
        skillName = _skillName;
        cost = _cost;
        m_baseSkillNode = _baseNode;
        previousNodes = _previousNodes;
        nextNodes = _nextNodes;
        m_previousIsBase = _previousIsBase;
    }

    public bool CanBeLearned()
    {
        if (learned)
            return false;

        if (m_previousIsBase)
            return true;
        
        var anyLearnPreviousSkills = previousNodes.Any(x => x.m_skillNode.learned);
        return anyLearnPreviousSkills;
    }

    public bool CanBeForgotten()
    {
        if (!learned)
            return false;
        
        if (nextNodes == null)
            return true;

        var anyLearnNextSkills = nextNodes.Any(x => x.m_skillNode.learned);
        return !anyLearnNextSkills;
    }
    
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

    public void ForceForget()
    {
        onForgottenAction?.Invoke();
        learned = false;
    }
}