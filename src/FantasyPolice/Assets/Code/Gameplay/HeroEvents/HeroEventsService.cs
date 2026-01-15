using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.HeroBased;
using Gameplay.HeroBased.StatsBased.Configs;
using Gameplay.StaticData;
using Infrastructure.Factory;
using Nani.UI.Custom.HeroEvents;
using Naninovel;

namespace Gameplay.HeroEvents
{
    public class HeroEventsService : IHeroEventsService
    {
        private readonly IStaticDataService _staticData;

        private readonly List<HeroEvent> _currentHeroEvents = new(16);
        private readonly HeroEventsUI _heroEventsUI;
        private readonly IGameFactory _gameFactory;

        public event Action<HeroEvent> OnHeroEvent;
        public event Action<HeroCard> OnHeroGoToOnEvent;
        public event Action<string> OnEventTimesUp;
        public event Action<string> OnEventFinished;

        public HeroEventsService(IStaticDataService staticData, IGameFactory gameFactory)
        {
            _staticData = staticData;
            _gameFactory = gameFactory;

            _gameFactory.OnHeroCardCreated += OnHeroCardCreated;
        }
        
        // Unusing coz HeroEventsService live on all app lifetime.
        public void Cleanup() => 
            _gameFactory.OnHeroCardCreated += OnHeroCardCreated;

        private void OnHeroCardCreated(HeroCard heroCard)
        {
            heroCard.OnGoToEvent += HandleOnHeroGoToOnEvent;
            heroCard.OnDestroyed += OnHeroDestroyed;
        }

        private void OnHeroDestroyed(HeroCard heroCard)
        {
            heroCard.OnGoToEvent -= HandleOnHeroGoToOnEvent;
            heroCard.OnDestroyed -= OnHeroDestroyed;
        }

        private void HandleOnHeroGoToOnEvent(HeroCard hero)
        {
            OnHeroGoToOnEvent?.Invoke(hero);
        }

        public HeroEvent GetEvent(string id) =>
            _currentHeroEvents.First(x => x.Id == id);

        public void GoToEvent(string eventId, HeroCard heroCard)
        {
            var targetEvent = _currentHeroEvents.First(x => x.Data.Id == eventId);
            targetEvent.Send(heroCard);
        }

        public HeroEvent Start(string id)
        {
            var data = _staticData.GetHeroEventsConfig()
                .Collection
                .First(data => data.Id == id);

            var heroEvent = new HeroEvent(data);

            heroEvent.OnFinished += OnFinished;
            heroEvent.OnTimesUp += HandleOnTimesUp;
            _currentHeroEvents.Add(heroEvent);

            OnHeroEvent?.Invoke(heroEvent);

            return heroEvent;
        }

        public void StartRandom()
        {
            var availableEvents = _staticData
                .GetHeroEventsConfig()
                .Collection
                .Where(config =>
                    _currentHeroEvents.All(heroEvent => heroEvent.Data.Id != config.Id))
                .ToList();
        

            if (availableEvents.Any() == false)
                return;

            Start(availableEvents.Random().Id);
        }

        private void HandleOnTimesUp(string id) =>
            OnEventTimesUp?.Invoke(id);

        public void FinishEvent(string id)
        {
            var target = _currentHeroEvents.First(x => x.Id == id);
            target.FinishEvent();
        }

        private void OnFinished(HeroEvent heroEvent, bool isHeroWin)
        {
            if (isHeroWin)
                heroEvent.CurrentHeroCard.Progress.AddExp(heroEvent.Data.Exp);
            else
                heroEvent.CurrentHeroCard.Stats.ApplyModifier(_staticData.StatModifier(StatModifierType.LooseEvent));
        
            heroEvent.OnFinished -= OnFinished;
            heroEvent.OnTimesUp -= HandleOnTimesUp;
        
            _currentHeroEvents.Remove(heroEvent);
            OnEventFinished?.Invoke(heroEvent.Data.Id);
        }
    }
}