using Gameplay.HeroBased;
using Model.Data;
using Nani.UI.Custom.HeroCards;

namespace Nani.UI.Custom.EventsDescription
{
    public class HeroEventsCardSlot : HeroCardSlot
    {
        public override bool CanAccept(IDrop drop)
        {
            var baseAccept = base.CanAccept(drop);

            if (drop is HeroCard heroCard && heroCard.IsOnEvent)
                return false;
            
            return baseAccept;
        }
    }
}