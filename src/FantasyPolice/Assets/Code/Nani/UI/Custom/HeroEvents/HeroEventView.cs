using System;
using CodeBase.Components.Animations;
using Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Nani.UI.Custom.HeroEvents
{
    [RequireComponent(typeof(ShowableAnimatorComponent))]
    public class HeroEventView : MonoBehaviour, IHaveId
    {
        [SerializeField] private ShowableAnimatorComponent _showable;
        [SerializeField] private Button _alertButton;
        public string Id { get; private set; }

        public event Action<string> OnClicked;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _showable = GetComponent<ShowableAnimatorComponent>();
        }
#endif

        public void Construct(string id)
        {
            Id = id;
        }

        public void Show()
        {
            _showable.Show();
        }

        public void Hide()
        {
            _showable.Hide();
        }

        private void OnEnable()
        {
            _alertButton.onClick.AddListener(OnAlertButtonClicked);
        }

        private void OnDisable()
        {
            _alertButton.onClick.RemoveListener(OnAlertButtonClicked);
        }

        private void OnAlertButtonClicked()
        {
            Hide();
            OnClicked?.Invoke(Id);
        }
    }
}