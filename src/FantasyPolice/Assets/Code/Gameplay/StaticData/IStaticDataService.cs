using System.Collections.Generic;
using Gameplay.HeroBased.Configs;
using Gameplay.HeroBased.StatsBased.Configs;
using Gameplay.HeroEvents;
using Gameplay.Locations;
using Model.Scriptable;
using UnityEngine;

namespace Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        HeroesConfig GetHeroConfigs();
        HeroEventsConfig GetHeroEventsConfig();
        EaseDurationConfig EaseDurationConfig(string id);
        int MaxHeroLevel();
        int ExperienceForLevel(int level);
        StatModifierData StatModifier(StatModifierType modifierType);
        Sprite ModifierIcon(StatModifierType modifierType);
        Vector3 LocationPosition(LocationType locationType);
        IEnumerable<string> StartHeroesIds();
    }
}