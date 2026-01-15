using System.Collections.Generic;
using System.Linq;
using Gameplay.HeroBased;
using Gameplay.HeroEvents;
using Infrastructure.Factory;
using Model.Data;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Nani.UI.Custom.HeroCards
{
    public class HeroCardsUI : CustomUI, IDropTarget, IItemRenderer<string>
    {
        [SerializeField] private RectTransform _slotsContainer;
        [SerializeField] private Transform _cardsContainer;
        [SerializeField] [Range(1, 10)] private int _slotsCount = 10;

        private IHeroesService _heroes;
        private IGameFactory _gameFactory;
        private IHeroEventsService _heroEvents;
        private bool _isWarmed;
        private readonly List<HeroCardSlot> _slots = new(10);


        [Inject]
        private void Construct(IHeroesService heroesService, IGameFactory gameFactory, IHeroEventsService heroEvents)
        {
            _heroes = heroesService;
            _gameFactory = gameFactory;
            _heroEvents = heroEvents;

            _heroEvents.OnHeroGoToOnEvent += OnHeroGoToOnEvent;
            _heroes.OnAvailable += Render;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        
            _heroEvents.OnHeroGoToOnEvent -= OnHeroGoToOnEvent;
            _heroes.OnAvailable -= Render;
        }

        private void WarmUp()
        {
            if (_isWarmed)
            {
                Debug.LogError($"{nameof(HeroCardsUI)} is already warmed.");
                return;
            }
        
            for (var i = 0; i < _slotsCount; i++)
                CreateSlot();
        
            LayoutRebuilder.ForceRebuildLayoutImmediate(_slotsContainer);
        
            _isWarmed = true;
        }

        private void CreateSlot()
        {
            _slots.Add(_gameFactory.HeroCardSlot(_slotsContainer));
        }

        public void Render(string data)
        {
            if (_isWarmed == false)
                WarmUp();
        
            var heroCard = _gameFactory.HeroCard(data, _cardsContainer);
            Accept(heroCard);
        }

        public bool CanAccept(IDrop drop)
        {
            return drop is HeroCard && _slots.Any(slot => slot.HasContent == false);
        }

        public void Accept(IDrop drop)
        {
            if (CanAccept(drop))
                _slots.First(x => x.HasContent == false).Accept(drop);
            else
                Debug.LogError($"Can't accept {drop}.");
        }

        private void OnHeroGoToOnEvent(HeroCard heroCard) =>
            Accept(heroCard);
    }
}