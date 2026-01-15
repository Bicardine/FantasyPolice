using System;
using UnityEngine;

namespace Gameplay.HeroBased.StatsBased.Configs
{
    [Serializable]
    public class StatModifierData
    {
        [SerializeField] private StatModifierType _statModifierType;
        [SerializeField] private StatType _statType;
        [SerializeField] private ModifierApplyMode _mode;
        [SerializeField] private int _value;
        [SerializeField] private Sprite _icon;
    
        public StatModifierType StatModifierType => _statModifierType;
        public StatType StatType => _statType;
        public ModifierApplyMode Mode => _mode;
        public int Value => _value;
        public Sprite Icon => _icon;
    }

    public enum ModifierApplyMode
    {
        Stack,
        Instance
    }

    public enum StatModifierType
    {
        Unknown = 0,
        LooseEvent = 1,
    }
}