using UnityEngine;

namespace CodeBase.Components.Animations
{
    [RequireComponent(typeof(Animator))]
    public class ShowableAnimatorComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int _isShownKey = Animator.StringToHash("IsShown");

#if UNITY_EDITOR
        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
        }
#endif

#if UNITY_EDITOR
        [ContextMenu("Show")]
#endif
        public void Show()
        {
            _animator.SetBool(_isShownKey, true);
        }

#if UNITY_EDITOR
        [ContextMenu("Hide")]
#endif
        public void Hide()
        {
            _animator.SetBool(_isShownKey, false);
        }
    }
}