using Model.Data;
using Nani.UI.Custom.HeroCards;
using Services.MessageBased;

namespace Services.Interactions
{
    public class CardSlotToCardSlotSwapInteraction : Interaction<HeroCardSlot, HeroCardSlot>
    {
        private readonly IGameplayMessagesService _gameplayMessages;

        public CardSlotToCardSlotSwapInteraction(IGameplayMessagesService gameplayMessages)
        {
            _gameplayMessages = gameplayMessages;
        }

        public override bool CanInteract(IInteractor source, IInteractor target)
        {
            return source is HeroCardSlot sourceSlot && sourceSlot.HasContent &&
                   target is HeroCardSlot targetSlot && targetSlot.HasContent;
        }

        protected override void Interact(HeroCardSlot source, HeroCardSlot target)
        {
            //_gameplayMessages.Send(new SwapMessage(source.Content as ISwapable, target.Content as ISwapable));
            var sourceFieldTileContent = source.Content;
            var targetFieldTileContent = target.Content;

            source.UnPut();
            target.UnPut();
            source.Accept(targetFieldTileContent as IDrop);
            target.Accept(sourceFieldTileContent as IDrop);
        }
    }
}