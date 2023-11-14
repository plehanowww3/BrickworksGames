using System.Collections.Generic;
using Enums;
using Helpers;
using meta_core.Services;
using MVVM.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MVVM.Views
{
    /// <summary>
    /// Монобех компонент способности
    /// </summary>
    public class SkillNodeView: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI")]
        [SerializeField] private TMP_Text m_skillName;
        [SerializeField] private Button m_skillButton;
        [SerializeField] private GameObject m_onMouseEnterObject;
        [Header("Параметры способности")]
        [SerializeField] private SkillsEnum m_skillType;
        [SerializeField] private int m_cost;
        [SerializeField] private bool m_previousSkillIsBase;
        [Header("Ссылки на способности")]
        [SerializeField] private List<SkillNodeView> m_previousSkills;
        [SerializeField] private List<SkillNodeView> m_nextSkills;
        
        /// <summary>
        /// Нода способности
        /// </summary>
        public SkillNode m_skillNode;
        /// <summary>
        /// Вьюмодель способностей
        /// </summary>
        private SkillsViewModel m_skillsViewModel;

        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
            Init();
            
            m_skillsViewModel.AddSkill(m_skillNode);
            m_skillButton.onClick.AddListener(OnSkillSelected);
        }

        /// <summary>
        /// Действие при нажатии на UI кнопки способности
        /// </summary>
        private void OnSkillSelected()
        {
            m_skillsViewModel.SelectSkill(m_skillNode);
        }
        
        /// <summary>
        /// Действие при изучении способности
        /// </summary>
        private void OnSkillLearnedAction()
        {
            m_skillButton.image.color = Color.green;
        }
        
        /// <summary>
        /// Действие при забываниии способности
        /// </summary>
        private void OnSkillForgottenAction()
        {
            m_skillButton.image.color = Color.white;
        }

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        public void Init()
        {
            m_skillName.text = SkillExtension.GetSkillName(m_skillType);
            
            m_skillNode = new SkillNode(m_skillType, m_cost,m_skillsViewModel.baseSkillNode, m_previousSkills, m_nextSkills, m_previousSkillIsBase);
            m_skillNode.onForgottenAction += OnSkillForgottenAction;
            m_skillNode.onLearnedAction += OnSkillLearnedAction;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_onMouseEnterObject.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            m_onMouseEnterObject.SetActive(false);
        }
    }
}