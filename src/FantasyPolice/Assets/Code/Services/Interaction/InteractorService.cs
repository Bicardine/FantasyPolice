using System.Collections.Generic;
using Model.Data;
using Nani.UI.Custom.EventsDescription;
using Services.Interactions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services.Interaction
{
    public class InteractorService : IInteractorService
    {
        private readonly ICamera _camera;
    
        private readonly List<RaycastResult> _raycastResults = new(32);

        public InteractorService(ICamera camera)
        {
            _camera = camera;
        }
    
        public bool TryGetAnyFirstInteractorFromMousePosition(PointerEventData eventData, out IInteractor targetInteractor)
        {
            targetInteractor = null;

            if (TryGetPhysicsInteractor(Input.mousePosition, out targetInteractor))
                return true;
            if (TryGetUIInteractor(eventData, out targetInteractor))
                return true;
        
            return targetInteractor != null;
        }

        private bool TryGetPhysicsInteractor(Vector3 from, out IInteractor targetInteractor)
        {
            targetInteractor = null;
        
            var hit = Physics2D.Raycast(_camera.Value.ScreenToWorldPoint(from), Vector2.zero);
            if (hit.collider != null)
                hit.collider.TryGetComponent(out targetInteractor);

            return targetInteractor != null;
        }

        private bool TryGetUIInteractor(PointerEventData eventData, out IInteractor targetInteractor)
        {
            targetInteractor = null;
        
            _raycastResults.Clear();
            EventSystem.current.RaycastAll(eventData, _raycastResults);
    
            foreach (var result in _raycastResults)
            {
                if (result.gameObject.TryGetComponent<IInteractor>(out var interactor) == false) continue;
                targetInteractor = interactor;
                break;
            }
        
            return targetInteractor != null;
        }
    }
}
