using Model.Data;

namespace Services.Interactions
{
    public interface IInteraction : IChainable<IInteraction>
    {
        bool CanInteract(IInteractor source, IInteractor target);

        bool TryInteract(IInteractor sourceInteractor,
            IInteractor targetFieldTile);
    }

    public interface IInteraction<TSource, TTarget> : IInteraction
        where TSource : IInteractor
        where TTarget : IInteractor
    {
    }

    /// <summary>
    /// Allow to use as generic in InteractionService.
    /// Also checked as collider in DragDropService to invoke TryInteract.
    /// </summary>>
    public interface IInteractor
    {
    }
}