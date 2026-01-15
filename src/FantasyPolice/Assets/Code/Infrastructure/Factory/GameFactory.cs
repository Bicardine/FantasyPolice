using System;
using System.Linq;
using Components.NotMono.TransformBased.Movement;
using Extensions;
using Gameplay.HeroBased;
using Gameplay.HeroBased.Configs;
using Gameplay.HeroBased.StatsBased;
using Gameplay.HeroBased.StatsBased.Configs;
using Gameplay.Locations;
using Gameplay.StaticData;
using Infrastructure.AssetManagement;
using ModifiersBased;
using Nani.UI.Custom.EventsDescription;
using Nani.UI.Custom.HeroCards;
using Nani.UI.Custom.HeroEvents;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assets;

        private const string HeroCardPath = "Gameplay/Heroes/HeroCard";
        private const string HeroCardSlotPath = "Gameplay/Heroes/HeroCardSlot";
        private const string HeroEventsCardSlotPath = "Gameplay/Heroes/HeroEventsCardSlot";
        private const string HeroEventViewPath = "Gameplay/HeroEvents/HeroEventView";
        private const string ModifierViewPath = "Gameplay/ModifierView";

        public event Action<HeroCardSlot> OnCardSlotCreated;
        public event Action<HeroCard> OnHeroCardCreated;

        public GameFactory(IStaticDataService staticData, IAssetProvider assets)
        {
            _staticData = staticData;
            _assets = assets;
        }

        public HeroCard HeroCard(string heroId, Transform parent = null)
        {
            var heroData = _staticData.GetHeroConfigs().Collection.First(hero => hero.Id == heroId);
            var prefab = _assets.LoadAsset(HeroCardPath);
            var heroCard = Object.Instantiate(prefab, parent, worldPositionStays: false).GetComponentOrError<HeroCard>();
            var outExpoMovementStrategy = new OutExpoMovementStrategy(_staticData.EaseDurationConfig("SpaceableMoveableConfig"), heroCard.Transform);

            heroCard.Construct(heroData.Id, BaseHeroStats(heroData), outExpoMovementStrategy, heroData.ViewData, this);
        
            OnHeroCardCreated?.Invoke(heroCard);

            return heroCard;
        }

        private HeroStats BaseHeroStats(HeroData heroData)
        {
            var baseStats = InitStats.EmptyStatDictionary().With(x => x[StatType.Power] = heroData.StatsConfigData.Power);
            var heroStats = new HeroStats(baseStats);

            return heroStats;
        }

        public HeroEventView HeroEventView(string eventId, LocationType location, Transform parent = null)
        {
            var prefab = _assets.LoadAsset(HeroEventViewPath);
            var heroEventView = Object.Instantiate(prefab, parent, worldPositionStays: false).GetComponentOrError<HeroEventView>();
            heroEventView.Construct(eventId);
            heroEventView.transform.position = _staticData.LocationPosition(location);

            return heroEventView;
        }

        public HeroCardSlot HeroCardSlot(Transform parent = null, Vector3 at = default)
        {
            var prefab = _assets.LoadAsset(HeroCardSlotPath);
            var heroCardSlot = Object.Instantiate(prefab, parent, worldPositionStays: false)
                .GetComponentOrError<HeroCardSlot>();

            OnCardSlotCreated?.Invoke(heroCardSlot);

            return heroCardSlot;
        }
        
        public HeroEventsCardSlot HeroEventsCardSlot(Transform parent = null, Vector3 at = default)
        {
            var prefab = _assets.LoadAsset(HeroEventsCardSlotPath);
            var heroCardSlot = Object.Instantiate(prefab, parent, worldPositionStays: false)
                .GetComponentOrError<HeroEventsCardSlot>();

            OnCardSlotCreated?.Invoke(heroCardSlot);

            return heroCardSlot;
        }

        public ModifierView ModifierView(StatModifierType modifierType, Transform parent = null, Vector3 at = default)
        {
            var prefab = _assets.LoadAsset(ModifierViewPath);
            var modifierView = Object.Instantiate(prefab, parent, worldPositionStays: false)
                .GetComponentOrError<ModifierView>();
            var icon = _staticData.ModifierIcon(modifierType);
            modifierView.Render(icon);
        
            return modifierView;
        }
    }
}