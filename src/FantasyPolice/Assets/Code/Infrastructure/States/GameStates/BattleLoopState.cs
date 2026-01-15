using Gameplay.HeroEvents;
using Gameplay.StaticData;
using Infrastructure.States.StateInfrastructure;

namespace Infrastructure.States.GameStates
{
    public class BattleLoopState : SimpleState
    {
        private readonly IHeroEventsService _heroEvents;

        private const string EvilWizardsId = "EvilWizards";

        public BattleLoopState(IHeroEventsService heroEvents)
        {
            _heroEvents = heroEvents;
        }

        public override void Enter()
        {
            _heroEvents.OnEventFinished += OnEventFinished;

            _heroEvents.Start(EvilWizardsId);
            _heroEvents.StartRandom();
        }

        protected override void Exit()
        {
            _heroEvents.OnEventFinished -= OnEventFinished;
        }

        private void OnEventFinished(string id)
        {
            if (id == EvilWizardsId)
                _heroEvents.Start(EvilWizardsId);
            else
                _heroEvents.StartRandom();
        }
    }
}