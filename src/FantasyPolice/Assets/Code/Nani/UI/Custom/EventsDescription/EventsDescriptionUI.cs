using System;
using System.Linq;
using Gameplay.HeroBased;
using Gameplay.HeroEvents;
using Gameplay.StaticData;
using Infrastructure.Factory;
using Model.Data;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Nani.UI.Custom.EventsDescription
{
    public class EventsDescriptionUI : CustomUI, IItemRenderer<string>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _goToEventButton;
        [SerializeField] private Transform _slotContainer;
        [SerializeField] private Transform _dragHeroBackground;

        private HeroEventsCardSlot _slot;
        private IStaticDataService _staticData;
        private IHeroEventsService _heroEventsService;
        private string _currentEventId;
        private IGameFactory _gameFactory;
        private bool _isWarmed;
        public HeroEventDataView Data { get; private set; }

        public event Action<string> OnCloseButtonClicked;

        [Inject]
        private void Construct(IStaticDataService staticData, IHeroEventsService heroEventsService, IGameFactory gameFactory)
        {
            _staticData = staticData;
            _heroEventsService = heroEventsService;

            _gameFactory = gameFactory;
            _heroEventsService.OnHeroGoToOnEvent += OnHeroGoToOnEvent;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _closeButton.onClick.AddListener(HandleOnCloseButtonClicked);
            _goToEventButton.onClick.AddListener(HandleGoToEventButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _closeButton.onClick.RemoveListener(HandleOnCloseButtonClicked);
            _goToEventButton.onClick.RemoveListener(HandleGoToEventButtonClicked);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _heroEventsService.OnHeroGoToOnEvent += OnHeroGoToOnEvent;

            if (_isWarmed == false)
                return;
            
            _slot.OnAccepted -= OnSlotAccepted;
            _slot.OnUnPut -= OnSlotUnPuted;
        }

        public override void Show()
        {
            base.Show();

            if (_slot is null)
            {
                ShowOnlyCloseButton();
                return;
            }
            
            if (_slot.HasContent)
                ShowOnlyGoButton();
            else
                ShowOnlyCloseButton();
        }

        public void Render(string eventId)
        {
            _currentEventId = eventId;
            var eventDataView = _staticData
                .GetHeroEventsConfig()
                .Collection
                .First(x => x.Id == eventId)
                .View;
            
            Render(eventDataView);
        }

        private void WarmUp()
        {
            if (_isWarmed)
                return;
            
            _isWarmed = true;
            
            _slot = _gameFactory.HeroEventsCardSlot(_slotContainer);
            _dragHeroBackground.SetParent(_slotContainer);
            _slot.OnAccepted += OnSlotAccepted;
            _slot.OnUnPut += OnSlotUnPuted;
        }

        private void ShowOnlyCloseButton()
        {
            _goToEventButton.gameObject.SetActive(false);
            _closeButton.gameObject.SetActive(true);
        }

        private void ShowOnlyGoButton()
        {
            _goToEventButton.gameObject.SetActive(true);
            _closeButton.gameObject.SetActive(false);
        }

        private void UnPut() =>
            _slot.UnPut();

        private void OnHeroGoToOnEvent(HeroCard _)
            => UnPut();

        private void HandleOnCloseButtonClicked()
        {
            Hide();
            OnCloseButtonClicked?.Invoke(_currentEventId);
        }

        private void Render(HeroEventDataView data)
        {
            WarmUp();
            
            Data = data;
            _name.SetText(data.NameKey);
            _description.SetText(data.DescriptionKey);

            Show();
        }

        private void HandleGoToEventButtonClicked()
        {
            Hide();
            _heroEventsService.GoToEvent(_currentEventId, (HeroCard)_slot.Content);
        }

        private void OnSlotAccepted(ISlotContent slotContent) =>
            ShowOnlyGoButton();

        private void OnSlotUnPuted(ISlotContent slotContent)
        {
            if (Visible == false)
                return;
            
            ShowOnlyCloseButton();
        }
    }
}
