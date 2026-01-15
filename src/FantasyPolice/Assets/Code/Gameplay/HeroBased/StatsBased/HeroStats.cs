using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Gameplay.HeroBased.StatsBased.Configs;

namespace Gameplay.HeroBased.StatsBased
{
    public class HeroStats
    {
        private readonly Dictionary<StatType, int> _baseStats;

        private readonly Dictionary<StatType, List<StatModifier>> _modifiers = new();


        public IReadOnlyDictionary<StatType, int> BaseStats => _baseStats;

        public event Action<StatType> OnBaseStatChanged;
        public event Action<StatModifierType> OnModifierChanged;
        public event Action<StatModifierType> OnModifierAdded;

        private const int DefaultUpgradeValue = 1;

        public HeroStats(Dictionary<StatType, int> baseStats)
        {
            _baseStats = baseStats;
            foreach (var stat in InitStats.EmptyStatDictionary().Keys)
                _modifiers[stat] = new List<StatModifier>();
        }

        public void Upgrade(StatType stat, int upgradeValue = DefaultUpgradeValue)
        {
            _baseStats[stat] += DefaultUpgradeValue;
            OnBaseStatChanged?.Invoke(stat);
        }
    
        public void ApplyModifier(StatModifierData data)
        {
            var modifiersByType = _modifiers[data.StatType];

            if (data.Mode == ModifierApplyMode.Stack)
            {
                var existing = modifiersByType.FirstOrDefault(m =>
                    m.Type == data.StatModifierType);

                if (existing != null)
                {
                    existing.Add(data.Value);
                    OnModifierChanged?.Invoke(data.StatModifierType);
                    return;
                }
            }

            var statModifier = new StatModifier(
                data.StatModifierType,
                data.StatType,
                data.Value);
            modifiersByType.Add(statModifier);

            OnModifierChanged?.Invoke(statModifier.Type);
            OnModifierAdded?.Invoke(statModifier.Type);
        }
    
        public bool RemoveOne(StatType stat, StatModifierType type)
        {
            var list = _modifiers[stat];
            var index = list.FindIndex(m => m.Type == type);
            if (index.IsLessThanZero())
                return false;

            list.RemoveAt(index);
            
            OnModifierChanged?.Invoke(type);
            
            return true;
        }
    
        public void RemoveAll(StatType stat, StatModifierType type)
        {
            _modifiers[stat].RemoveAll(m => m.Type == type);
            OnModifierChanged?.Invoke(type);
        }
    
        public int Final(StatType stat)
        {
            var baseValue = _baseStats[stat];
            var sum = _modifiers[stat].Sum(m => m.Value);
            return baseValue + sum;
        }
    }
}