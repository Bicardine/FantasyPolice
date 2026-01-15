using Model.Data;
using Nani.UI.Custom.HeroCards;

namespace Services.MessageBased.Messages
{
    public class InteractorAcceptMessage : IInteractionMessage
    {
        public HeroCardSlot SourceTile { get; }
        public IDropTarget DropTarget { get; }
        public IDrop Drop { get; }

        public InteractorAcceptMessage(HeroCardSlot sourceTile, IDropTarget dropTarget, IDrop drop)
        {
            SourceTile = sourceTile;
            DropTarget = dropTarget;
            Drop = drop;
        }
    }
}