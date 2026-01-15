using Services.Interactions;

namespace Services.Interaction
{
    public interface ISlotsInteractionService
    {
        bool TryInteract(IInteractor sourceFieldTile, IInteractor targetFieldTile);
    }
}