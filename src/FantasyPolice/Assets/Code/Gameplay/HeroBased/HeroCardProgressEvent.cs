using CodeBase.Components.Animations;
using Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.HeroBased
{
    [RequireComponent(typeof(ShowableAnimatorComponent))]
    public class HeroCardProgressEvent : MonoBehaviour, IItemRenderer<float>
    {
        [SerializeField] private ShowableAnimatorComponent _showable;
        [SerializeField] private Image _bar;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _showable = GetComponent<ShowableAnimatorComponent>();
        }
#endif
    
        public void Render(float data)
        {
            _bar.fillAmount = data;
        }

        public void Show()
        {
            _showable.Show();
        }
    
        public void Hide()
        {
            _showable.Hide();
        }
    }
}