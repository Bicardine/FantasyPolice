using System;

namespace Model.Data
{
    public interface ISlotContent : IHaveTransform, IMoveable
    {
        void OnUnput();
    }

    public interface IContentHolder<T> : IDropTarget
    {
        public T Content { get; }
        public bool HasContent { get; }
        void UnPut();
        event Action<ISlotContent> OnAccepted;
        event Action<ISlotContent> OnUnPut;
    }
}