using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MVVM.Views
{
    /// <summary>
    /// Дизейбл компонент кнопки
    /// </summary>
    public class DisableButton: MonoBehaviour
    {
        [SerializeField] private Button m_button;
        [SerializeField] private Image m_disableObject;

        /// <summary>
        /// Поменять состояния дизейбла кнопки
        /// </summary>
        /// <param name="_needDisable">Нужно ли ее задизейблить?</param>
        public void SetDisable(bool _needDisable)
        {
            m_button.interactable = !_needDisable;
            m_disableObject.gameObject.SetActive(_needDisable);
        }

        /// <summary>
        /// Добавить действие при нажатии на кнопку
        /// </summary>
        /// <param name="_action"></param>
        public void AddActionOnClick(UnityAction _action)
        {
            m_button.onClick.AddListener(_action);
        }
    }
}