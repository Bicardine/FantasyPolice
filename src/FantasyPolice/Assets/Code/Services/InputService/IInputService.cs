using System;
using Model.Data;
using UnityEngine.EventSystems;

namespace Services.InputService
{
    public interface IInputService
    {
        event Action<IClickable, PointerEventData> OnClicked;
        event Action<IDraggable, PointerEventData> OnDragStarted;
        event Action<IDraggable, PointerEventData> OnDraging;
        event Action<IDraggable, PointerEventData> OnDragEnded;
        bool IsDragingNow(IDraggable draggable);
    }
}