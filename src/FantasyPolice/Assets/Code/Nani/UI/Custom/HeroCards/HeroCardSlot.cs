using System;
using Gameplay.HeroBased;
using Model.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nani.UI.Custom.HeroCards
{
    public class HeroCardSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler,
        IPointerUpHandler, IDraggable, IClickable, IContentHolder<ISlotContent>, ISelectable, IHaveTransform
    {
        public ISlotContent Content { get; private set; }
        public bool HasContent => Content != null;

        public bool IsSelected { get; private set; }

        public void UnPut()
        {
            var content = Content;
            Content.OnUnput();
            Content = null;
            OnUnPut?.Invoke(content);
        }

        private bool _isDraging;
        private Transform _transform;

        public event Action<IClickable, PointerEventData> OnClicked;
        public event Action<IDraggable, PointerEventData> OnDragStarted;
        public event Action<IDraggable, PointerEventData> OnDraging;
        public event Action<IDraggable, PointerEventData> OnDragEnded;
        public event Action<ISlotContent> OnAccepted;
        public event Action<ISlotContent> OnUnPut;

        public event Action<HeroCardSlot> OnDestroyed;


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isDraging)
                return;
            
            OnDragStarted?.Invoke(this, eventData);
            _isDraging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDraging?.Invoke(this, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDraging == false)
                return;

            _isDraging = false;
            OnDragEnded?.Invoke(this, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isDraging)
                return;
            
            OnClick(eventData);
        }

        public void Select()
        {
            IsSelected = true;
        }

        public void Deselect()
        {
            IsSelected = false;
        }

        public Transform Transform => _transform ??= transform;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        private void OnClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this, eventData);
            
            (Content as IChildClickable)?.TryAcceptClick();
        }

        public void OnPointerDown(PointerEventData eventData) { }

        // CanAccept and CanSelfAccept in case to feature on child can accept check.
        public virtual bool CanAccept(IDrop drop) =>
            CanSelfAccept(drop);

        private bool CanSelfAccept(IDrop drop) =>
            drop != null && HasContent == false && drop is HeroCard;

        public void Accept(IDrop drop)
        {
            if (CanSelfAccept(drop))
                SelfAccept(drop);
            else
                throw new ArgumentException();
        }

        private void SelfAccept(IDrop drop)
        {
            var slotContent = (ISlotContent)drop;
            Content = slotContent;
            Content.MoveTo(Transform.position);
            OnAccepted?.Invoke(slotContent);
        }
    }
}
