using System;
using UnityEngine;

namespace Gameplay.HeroBased.Configs
{
    [Serializable]
    public class HeroViewData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
    }
}