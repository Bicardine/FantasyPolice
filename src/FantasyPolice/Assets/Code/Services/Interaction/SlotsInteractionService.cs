using Model.Data;
using Services.Interactions;
using Services.MessageBased;

namespace Services.Interaction
{
    public class SlotsInteractionService : ISlotsInteractionService
    {
        private readonly Chain<IInteraction> _interactionChain;

        public SlotsInteractionService(IGameplayMessagesService gameplayMessages)
        {
            // The chain is perfect for such types games there can be interactions depending on conditions.
            // So far, it has not shown itself so strongly coz not many gameplay features.
            // But there could be a Swap interaction here, for example as next chain part.
            _interactionChain = new Chain<IInteraction>()
                .SetNext(new CardSlotToInteractorAcceptInteraction(gameplayMessages))
                .SetNext(new CardSlotToCardSlotSwapInteraction(gameplayMessages));
        }

        public bool TryInteract(IInteractor sourceFieldTile, IInteractor targetFieldTile)
        {
            return _interactionChain.First().TryInteract(sourceFieldTile, targetFieldTile);
        }
    }
}