using Services.Interactions;
using UnityEngine.EventSystems;

namespace Services.Interaction
{
    public interface IInteractorService
    {
        bool TryGetAnyFirstInteractorFromMousePosition(PointerEventData eventData, out IInteractor targetInteractor);
    }
}