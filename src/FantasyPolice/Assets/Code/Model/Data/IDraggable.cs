using System;
using UnityEngine.EventSystems;

namespace Model.Data
{
    public interface IDraggable
    {
        public event Action<IDraggable, PointerEventData> OnDragStarted;
        public event Action<IDraggable, PointerEventData> OnDraging;
        public event Action<IDraggable, PointerEventData> OnDragEnded;
    }
}