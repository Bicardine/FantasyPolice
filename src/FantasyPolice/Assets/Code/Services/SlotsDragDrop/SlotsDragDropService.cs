using Cysharp.Threading.Tasks;
using Extensions;
using Model.Data;
using Nani.UI.Custom.HeroCards;
using Services.InputService;
using Services.Interaction;
using Services.Positions;
using UnityEngine.EventSystems;

namespace Services.SlotsDragDrop
{
    public class SlotsDragDropService : ISlotsDragDropService, INonLazyResolveable
    {
        private readonly ISlotsInteractionService _slotsInteractionService;

        private readonly IInputService _inputService;
        private readonly IInteractorService _interactorService;
        private readonly IPositionsService _positionsService;

        private HeroCardSlot _selectedSlot;

        public SlotsDragDropService(IInputService inputService,
            ISlotsInteractionService slotsInteractionService, IInteractorService interactorService,
            IPositionsService positionsService)
        {
            _inputService = inputService;

            _inputService.OnClicked += OnClicked;
            _inputService.OnDraging += OnDraging;
            _inputService.OnDragEnded += OnDragEnded;

            _slotsInteractionService = slotsInteractionService;
            _interactorService = interactorService;
            _positionsService = positionsService;
        }

        private void OnClicked(IClickable clickable, PointerEventData eventData)
        {
            // Check type coz flexible input there can observe all IClickable and IDraggable.
            // And the SlotsDragDropService using specific logic.
            if (clickable is HeroCardSlot heroSlot == false)
                return;

            _selectedSlot?.Deselect();
            _selectedSlot = heroSlot;
            _selectedSlot.Select();
        }

        private void OnDraging(IDraggable draggable, PointerEventData eventData)
        {
            if (draggable is HeroCardSlot heroCard == false)
                return;
            
            if (HasMovableContent(heroCard, out var movable) == false)
                return;
            
            var screenToWorldPoint = _positionsService
                .ScreenToWorldPoint(eventData.position, NaniCameraType.UI)
                .ResetZ();

            movable.MoveTo(screenToWorldPoint);
        }

        private void OnDragEnded(IDraggable draggable, PointerEventData eventData)
        {
            if (draggable is HeroCardSlot heroCard == false)
                return;
            
            if (HasMovableContent(heroCard, out var movable) == false)
                return;

            if (_interactorService.TryGetAnyFirstInteractorFromMousePosition(eventData, out var targetInteractor))
                if (targetInteractor != draggable)
                    if (_slotsInteractionService.TryInteract(heroCard, targetInteractor))
                        return;

            movable.MoveTo(heroCard.Transform.position).Forget();
        }
        
        private bool HasMovableContent(HeroCardSlot heroCard, out IMoveable moveable)
        {
            moveable = null;
            
            if (heroCard.HasContent == false)
                return false;

            if (heroCard.Content is IMoveable movable == false)
                return false;
            
            moveable = movable;
            
            return true;
        }
    }
}