namespace Gameplay.HeroBased.HeroProgressBased.Configs
{
    public class HeroExpProgressData
    {
        public string Label { get; private set; }
        public float NormalizedValue { get; private set; }

        public HeroExpProgressData(float normalizedValue, string label)
        {
            NormalizedValue = normalizedValue;
            Label = label;
        }
    }
}