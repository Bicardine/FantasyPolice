using System;
using System.Collections.Generic;
using Gameplay.HeroBased;
using Infrastructure.Factory;
using Model.Data;
using Nani.UI.Custom.HeroCards;
using UnityEngine.EventSystems;

namespace Services.InputService
{
    public class InputService : IInputService, INonLazyResolveable
    {
        private readonly IGameFactory _gameFactory;

        private HeroCard _selectedTile;
        
        private readonly HashSet<HeroCardSlot> _heroCardSlots = new();

        private readonly HashSet<IDraggable> _dragging = new(2);
        
        public event Action<IClickable, PointerEventData> OnClicked;
        public event Action<IDraggable, PointerEventData> OnDragStarted;
        public event Action<IDraggable, PointerEventData> OnDraging;
        public event Action<IDraggable, PointerEventData> OnDragEnded;
        
        public bool IsDragingNow(IDraggable draggable)
        {
            return _dragging.Contains(draggable);
        }

        public InputService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            _gameFactory.OnCardSlotCreated += OnHeroCardSlotCreated;
        }

        private void OnHeroCardSlotCreated(HeroCardSlot heroCardSlot)
        {
            _heroCardSlots.Add(heroCardSlot);

            heroCardSlot.OnClicked += HandleOnClicked;
            heroCardSlot.OnDragStarted += HandleOnDragStarted;
            heroCardSlot.OnDraging += HandleOnDraging;
            heroCardSlot.OnDragEnded += HandleOnDragEnded;
            heroCardSlot.OnDestroyed += OnFieldTileReleased;
        }

        private void OnFieldTileReleased(HeroCardSlot heroCardSlot)
        {
            _heroCardSlots.Remove(heroCardSlot);
            heroCardSlot.OnClicked -= HandleOnClicked;
            heroCardSlot.OnDragStarted -= HandleOnDragStarted;
            heroCardSlot.OnDraging -= HandleOnDraging;
            heroCardSlot.OnDragEnded -= HandleOnDragEnded;
            heroCardSlot.OnDestroyed -= OnFieldTileReleased;
        }
        
        private void HandleOnClicked(IClickable clickable, PointerEventData eventData)
        {
            OnClicked?.Invoke(clickable, eventData);
        }

        private void HandleOnDragStarted(IDraggable draggable, PointerEventData eventData)
        {
            if (IsDragingNow(draggable) == false)
            {
                OnDragStarted?.Invoke(draggable, eventData);
                _dragging.Add(draggable);
            }
        }

        private void HandleOnDraging(IDraggable draggable, PointerEventData eventData)
        {
            OnDraging?.Invoke(draggable, eventData);
        }

        private void HandleOnDragEnded(IDraggable draggable, PointerEventData eventData)
        {
            if (IsDragingNow(draggable))
            {
                _dragging.Remove(draggable);
                OnDragEnded?.Invoke(draggable, eventData);
            }
        }

        public void Cleanup()
        {
            _gameFactory.OnCardSlotCreated -= OnHeroCardSlotCreated;
            
            foreach (var fieldTile in _heroCardSlots)
            {
                fieldTile.OnClicked -= HandleOnClicked;
                fieldTile.OnDragStarted -= HandleOnDragStarted;
                fieldTile.OnDraging -= HandleOnDraging;
                fieldTile.OnDragEnded -= HandleOnDragEnded;
            }
            
            _heroCardSlots.Clear();
        }
    }
}