using meta_core.Services;
using MVVM.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM.Views
{
    public class GetPointsButton: MonoBehaviour
    {
        [SerializeField] private int m_pointsPerClick = 1;
        [SerializeField] private Button m_button;

        private SkillsViewModel m_skillsViewModel;
        private void Awake()
        {
            m_skillsViewModel = ServiceLocator.instance.Resolve<SkillsViewModel>();
                
            m_button.onClick.AddListener(OnClickAction);
        }

        private void OnClickAction()
        {
            m_skillsViewModel.IncreaseSkillPoints(m_pointsPerClick);
        }
    }
}