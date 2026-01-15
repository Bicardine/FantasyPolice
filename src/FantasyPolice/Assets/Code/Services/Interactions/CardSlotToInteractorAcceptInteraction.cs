using Model.Data;
using Nani.UI.Custom.HeroCards;
using Services.MessageBased;
using Services.MessageBased.Messages;

namespace Services.Interactions
{
    public class CardSlotToInteractorAcceptInteraction : Interaction<HeroCardSlot, IDropTarget>
    {
        private readonly IGameplayMessagesService _gameplayMessages;

        public CardSlotToInteractorAcceptInteraction(IGameplayMessagesService gameplayMessages)
        {
            _gameplayMessages = gameplayMessages;
        }

        public override bool CanInteract(IInteractor source, IInteractor target) =>
            source is HeroCardSlot sourceFieldTile && sourceFieldTile.Content is IDrop content &&
            target is IDropTarget dropTarget && dropTarget.CanAccept(content);

        protected override void Interact(HeroCardSlot source, IDropTarget target)
        {
            var sourceContent = source.Content;
            _gameplayMessages.Send(new InteractorAcceptMessage(source, target, sourceContent as IDrop));
            source.UnPut();
            target.Accept(sourceContent as IDrop);
        }
    }
}