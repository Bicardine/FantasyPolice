using System.Collections.Generic;
using System.Linq;
using Extensions;
using Gameplay.HeroBased.Configs;
using Gameplay.HeroBased.HeroProgressBased.Configs;
using Gameplay.HeroBased.StatsBased.Configs;
using Gameplay.HeroEvents;
using Gameplay.Locations;
using Gameplay.Locations.Config;
using Infrastructure.AssetManagement;
using Model.Scriptable;
using UnityEngine;

namespace Gameplay.StaticData
{
    // For now it's just a Resources, not Addressables.
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        
        private HeroesConfig _heroes;
        private HeroEventsConfig _heroEvents;
        private EaseDurationRepository _easeDurationRepository;
        private HeroExpLevelConfig _heroExpLevelConfig;
        private StatModifiersConfig _statModifiers;
        private LocationPositionsConfig _locationPositions;
        private StartHeroesConfig _startHeroes;

        // In Addressables there would be persistant labels heres, not paths.
        private const string HeroesConfigPath = "StaticData/Heroes/HeroesConfig";
        private const string HeroEventsConfigPath = "StaticData/HeroEventsConfig";
        private const string EaseDurationRepositoryPath = "StaticData/Animations/EaseDurationRepository";
        private const string ExpConfigPath = "StaticData/Heroes/HeroExpLevelConfig";
        private const string StatModifiersConfigPath = "StaticData/Heroes/StatModifiersConfig";
        private const string LocationPositionConfigPath = "StaticData/LocationPositionsConfig";
        private const string StartHeroesConfigPath = "StaticData/Heroes/StartHeroesConfig";

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public HeroesConfig GetHeroConfigs() => _heroes;
        
        public HeroEventsConfig GetHeroEventsConfig() => _heroEvents;

        // In Addressables case should be WarmUp() method.
        public void LoadAll()
        {
            LoadHeroesConfig();
            LoadHeroEventsConfig();
            LoadEaseDurationRepository();
            LoadHeroExpLevelConfig();
            LoadStatModifiersConfig();
            LoadLocationPositionsConfig();
            LoadStartHeroesConfig();
        }

        public int MaxHeroLevel() =>
            _heroExpLevelConfig.Collection.Count.Previous();

        public int ExperienceForLevel(int level) =>
            _heroExpLevelConfig.Collection[level].ExpToGetLvl;

        public EaseDurationConfig EaseDurationConfig(string id) =>
            _easeDurationRepository.Collection.First(config => config.Id == id);

        public StatModifierData StatModifier(StatModifierType modifierType) =>
            _statModifiers.Collection.First(x => x.StatModifierType == modifierType);

        public Sprite ModifierIcon(StatModifierType modifierType) =>
            _statModifiers.Collection.First(x => x.StatModifierType == modifierType).Icon;

        public Vector3 LocationPosition(LocationType locationType) => 
            _locationPositions.Collection.First(x => x.LocationType == locationType).Position;

        public IEnumerable<string> StartHeroesIds() => _startHeroes.Collection;

        private void LoadHeroesConfig() =>
            _heroes = _assetProvider.LoadAsset<HeroesConfig>(HeroesConfigPath);

        private void LoadHeroEventsConfig() =>
            _heroEvents = _assetProvider.LoadAsset<HeroEventsConfig>(HeroEventsConfigPath);

        private void LoadEaseDurationRepository() =>
            _easeDurationRepository = _assetProvider.LoadAsset<EaseDurationRepository>(EaseDurationRepositoryPath);

        private void LoadHeroExpLevelConfig() =>
            _heroExpLevelConfig = _assetProvider.LoadAsset<HeroExpLevelConfig>(ExpConfigPath);

        private void LoadLocationPositionsConfig() =>
            _locationPositions = _assetProvider.LoadAsset<LocationPositionsConfig>(LocationPositionConfigPath);

        private void LoadStartHeroesConfig() =>
            _startHeroes = _assetProvider.LoadAsset<StartHeroesConfig>(StartHeroesConfigPath);

        private void LoadStatModifiersConfig() =>
            _statModifiers = _assetProvider.LoadAsset<StatModifiersConfig>(StatModifiersConfigPath);
    }
}