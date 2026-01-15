using Gameplay.HeroBased;
using Infrastructure.Factory;
using Model.Data;
using Nani.UI.Custom.HeroCards;
using Services.InputService;
using UnityEngine.EventSystems;

namespace Services.Render
{
    public class CardsSortingRenderService : ICardsSortingRenderService, INonLazyResolveable
    {
        private HeroCard _draging;

        private const int LayerOnMoveEnded = 1;
        private const int LayerOnMoveStarted = 475;
        private const int LayerOnDragStarted = 500;
        private const int LayerOnDragEnded = 450;

        public CardsSortingRenderService(IInputService inputService, IGameFactory gameFactory)
        {
            inputService.OnDragStarted += OnDragStarted;
            inputService.OnDragEnded += OnDragEnded;
            gameFactory.OnHeroCardCreated += OnHeroCardCreated;
        }

        private void OnHeroCardCreated(HeroCard heroCard)
        {
            heroCard.OnMoveStarted += OnMoveStarted;
            heroCard.OnMoveEnded += OnMoveEnded;
        }

        private void OnMoveEnded(IMoveable moveable)
        {
            // It's ok coz subscribed from hero card.
            var card = moveable as HeroCard;
            card.Canvas.sortingOrder = LayerOnMoveEnded;
            card.Canvas.overrideSorting = false;
        }

        private void OnMoveStarted(IMoveable moveable)
        {
            var card = moveable as HeroCard;
            
            if (_draging == card)
                return;
        
            card.Canvas.sortingOrder = LayerOnMoveStarted;
        }

        private void OnDragStarted(IDraggable draggable, PointerEventData data)
        {
            if (draggable is HeroCardSlot fieldTile == false)
                return;
        
            if (fieldTile.HasContent == false)
                return;
        
            if (fieldTile.Content is HeroCard sortingLayerChangeable == false)
                return;
        
            _draging = sortingLayerChangeable;
            _draging.Canvas.overrideSorting = true;
            _draging.Canvas.sortingOrder = LayerOnDragStarted;
        
        }

        private void OnDragEnded(IDraggable draggable, PointerEventData data)
        {
            if (_draging == null)
                return;
        
            _draging.Canvas.sortingOrder = LayerOnDragEnded;
            
            _draging = null;
        }
    }
}