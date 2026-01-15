using Gameplay.HeroBased.Configs;
using Model.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.HeroBased
{
    public class HeroCardView : MonoBehaviour, IItemRenderer<HeroViewData>
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Image _image;
    
        public void Render(HeroViewData data)
        {
            _image.sprite = data.Icon;
        
            // Here should be a service localization (from NameKey) in real project. Not a real name.
            _label.SetText(data.Name);
        }
    }
}