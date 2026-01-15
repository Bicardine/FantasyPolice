using System;
using Gameplay.HeroBased;

namespace Gameplay.HeroEvents
{
    public interface IHeroEventsService
    {
        HeroEvent Start(string id);
        event Action<HeroEvent> OnHeroEvent;
        void GoToEvent(string eventId, HeroCard heroCard);
        HeroEvent GetEvent(string id);
        void FinishEvent(string id);
        event Action<string> OnEventTimesUp;
        event Action<string> OnEventFinished;
        void StartRandom();
        event Action<HeroCard> OnHeroGoToOnEvent;
    }
}