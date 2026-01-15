using System;
using UnityEngine;

namespace Gameplay.HeroBased.StatsBased.Configs
{
    [Serializable]
    public class HeroStatsConfigData
    {
        [SerializeField] [Range(0, 100)] private int _power;

        public int Power => _power;
    }

    [Serializable]
    public class HeroProgress
    {
        private readonly HeroCard _heroCard;
        public int Level { get; private set; } = DefaultLevel;
        public int UpgradePoints { get; set; }
        public int Exp { get; private set; }
        public int TotalExpToNextLevel { get; set; }

        public event Action<HeroCard> OnExpChanged;
        public event Action<HeroCard> OnLevelUped;
    
        private const int DefaultLevel = 0;

        public HeroProgress(HeroCard heroCard)
        {
            _heroCard = heroCard;
        }

        public void LevelUp()
        {
            Level++;
            UpgradePoints++;
            OnLevelUped?.Invoke(_heroCard);
        }

        public void ChangeExp(int exp)
        {
            Exp = exp;
            OnExpChanged?.Invoke(_heroCard);
        }

        public void AddExp(int exp)
        {
            Exp += exp;
            OnExpChanged?.Invoke(_heroCard);
        }
    }
}