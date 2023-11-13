using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MVVM.Views
{
    public class DisableButton: MonoBehaviour
    {
        [SerializeField] private Button m_button;
        [SerializeField] private Image m_disableObject;

        public void SetDisable(bool _needDisable)
        {
            m_button.interactable = !_needDisable;
            m_disableObject.gameObject.SetActive(_needDisable);
        }

        public void AddActionOnClick(UnityAction _action)
        {
            m_button.onClick.AddListener(_action);
        }
    }
}