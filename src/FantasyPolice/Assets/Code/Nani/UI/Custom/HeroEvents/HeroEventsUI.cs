using System.Collections.Generic;
using System.Linq;
using Gameplay.HeroBased.StatsBased.Configs;
using Gameplay.HeroEvents;
using Infrastructure.Factory;
using Nani.UI.Custom.EventsDescription;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using Zenject;

namespace Nani.UI.Custom.HeroEvents
{
    public class HeroEventsUI : CustomUI
    {
        [SerializeField] private Transform _container;
    
        private IHeroEventsService _heroEvents;
        private IGameFactory _gameFactory;
        private readonly List<HeroEventView> _current = new(12);
        private EventsDescriptionUI _eventsDescriptionUI;
        private HeroEventResultUI _resultUI;

        [Inject]
        private void Construct(
            IHeroEventsService heroEvents,
            IGameFactory gameFactory,
            EventsDescriptionUI eventsDescriptionUI,
            HeroEventResultUI resultUI)
        {
            _heroEvents = heroEvents;
            _gameFactory = gameFactory;
            _eventsDescriptionUI = eventsDescriptionUI;
            _resultUI = resultUI;

            _eventsDescriptionUI.OnCloseButtonClicked += OnDescriptionCloseButtonClicked;
            _heroEvents.OnHeroEvent += OnHeroEvent;
            _heroEvents.OnEventTimesUp += OnEventTimesUp;
            _resultUI.OnContinueClicked += OnShowResultContinue;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        
            _eventsDescriptionUI.OnCloseButtonClicked -= OnDescriptionCloseButtonClicked;
            _heroEvents.OnHeroEvent -= OnHeroEvent;
            _heroEvents.OnEventTimesUp -= OnEventTimesUp;
            _resultUI.OnContinueClicked -= OnShowResultContinue;
        }

        private void ShowResult(HeroEventResultData data)
        {
            _resultUI.Render(data);
        }

        private void OnEventTimesUp(string id)
        {
            _current.First(x => x.Id == id).Show();
        }

        private void OnDescriptionCloseButtonClicked(string id)
        {
            _current.First(x => x.Id == id).Show();
        }

        private void OnShowResultContinue(string id)
        {
            _current.RemoveAll(x => x.Id == id);
        }

        private void OnHeroEvent(HeroEvent heroEvent)
        {
            Render(heroEvent);
        }

        private void Render(HeroEvent heroEvent)
        {
            var heroEventView = _gameFactory.HeroEventView(heroEvent.Data.Id, heroEvent.Data.LocationType, _container);
            heroEventView.OnClicked += HandleOnEventViewClicked;
            heroEventView.Show();
            _current.Add(heroEventView);
        }

        private void HandleOnEventViewClicked(string id)
        {
            var targetEvent = _heroEvents.GetEvent(id);

            if (targetEvent.IsEventStarted)
                ShowResult(new HeroEventResultData(targetEvent.Id,
                    targetEvent.Data.PowerToComplete,
                    targetEvent.CurrentHeroCard.Stats.Final(StatType.Power), targetEvent.IsHeroWin));
            else
                _eventsDescriptionUI.Render(targetEvent.Id);
        }
    }
}
