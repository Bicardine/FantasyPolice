using System;
using Gameplay.HeroBased;
using Gameplay.HeroBased.StatsBased.Configs;
using Gameplay.Locations;
using ModifiersBased;
using Nani.UI.Custom.EventsDescription;
using Nani.UI.Custom.HeroCards;
using Nani.UI.Custom.HeroEvents;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        HeroCard HeroCard(string heroId, Transform parent = null);
        HeroEventView HeroEventView(string eventId, LocationType location, Transform parent = null);
        HeroCardSlot HeroCardSlot(Transform parent = null, Vector3 at = default);
        HeroEventsCardSlot HeroEventsCardSlot(Transform parent = null, Vector3 at = default);
        ModifierView ModifierView(StatModifierType modifierType, Transform parent = null, Vector3 at = default);
        event Action<HeroCardSlot> OnCardSlotCreated;
        event Action<HeroCard> OnHeroCardCreated;
    }
}