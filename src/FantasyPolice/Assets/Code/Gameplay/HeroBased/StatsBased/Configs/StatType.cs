using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.HeroBased.StatsBased.Configs
{
    public enum StatType
    {
        Unknown = 0,
        Power = 1
    }

    public static class InitStats
    {
        public static Dictionary<StatType, int> EmptyStatDictionary()
        {
            return Enum.GetValues(typeof(StatType))
                .Cast<StatType>()
                .Except(new[] {StatType.Unknown})
                .ToDictionary(x => x, _ => 0);
        }
    }
}