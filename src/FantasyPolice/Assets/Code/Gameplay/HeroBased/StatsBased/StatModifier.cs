using Gameplay.HeroBased.StatsBased.Configs;

namespace Gameplay.HeroBased.StatsBased
{
    public sealed class StatModifier
    {
        public StatModifierType Type { get; }
        public StatType Stat { get; }
        public int Value { get; private set; }

        public StatModifier(
            StatModifierType type,
            StatType stat,
            int value)
        {
            Type = type;
            Stat = stat;
            Value = value;
        }

        public void Add(int delta)
        {
            Value += delta;
        }
    }
}