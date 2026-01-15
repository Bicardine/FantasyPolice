namespace Services.Interactions
{
    public abstract class Interaction<TSource, TTarget> : IInteraction<TSource, TTarget>
        where TSource : IInteractor
        where TTarget : IInteractor
    {
        private IInteraction _next;

        public IInteraction SetNext(IInteraction interaction)
        {
            _next = interaction;
            return interaction;
        }
        
        public abstract bool CanInteract(IInteractor source, IInteractor target);
        
        bool IInteraction.TryInteract(IInteractor sourceInteractor, IInteractor targetInteractor)
        {
            if (sourceInteractor is TSource typedSource && targetInteractor is TTarget typedTarget)
            {
                if (CanInteract(typedSource, typedTarget))
                {
                    Interact(typedSource, typedTarget);
                    return true;
                }
            }
            return _next?.TryInteract(sourceInteractor, targetInteractor) ?? false;
        }
        
        protected abstract void Interact(TSource source, TTarget target);
    }
}