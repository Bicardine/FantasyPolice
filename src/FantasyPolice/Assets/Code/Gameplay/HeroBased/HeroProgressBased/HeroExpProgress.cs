using DG.Tweening;
using Gameplay.HeroBased.HeroProgressBased.Configs;
using Model.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.HeroBased.HeroProgressBased
{
    public class HeroExpProgress : MonoBehaviour, IItemRenderer<HeroExpProgressData>
    {
        [SerializeField] private Image _bar;
        [SerializeField] private TMP_Text _expText;
    
        private Tween _currentTween;
    
        public void Render(HeroExpProgressData data)
        {
            if (_currentTween != null && _currentTween.IsActive())
                _currentTween.Kill();
        
            _expText.SetText(data.Label);
        
            _currentTween = _bar.DOFillAmount(data.NormalizedValue, 0.5f)
                .SetEase(Ease.InOutQuint);
        }
    
        private void OnDestroy()
        {
            _currentTween?.Kill();
        }
    }
}