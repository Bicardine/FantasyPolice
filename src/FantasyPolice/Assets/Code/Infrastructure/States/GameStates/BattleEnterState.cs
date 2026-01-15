using Gameplay.HeroBased;
using Gameplay.StaticData;
using Infrastructure.States.StateInfrastructure;
using Infrastructure.States.StateMachine;

namespace Infrastructure.States.GameStates
{
    public class BattleEnterState : SimpleState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IHeroesService _heroesService;
        private readonly IStaticDataService _staticData;

        public BattleEnterState(
            IGameStateMachine stateMachine,
            IHeroesService heroesService,
            IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _heroesService = heroesService;
            _staticData = staticData;
        }

        public override void Enter()
        {
            foreach (var startHeroId in _staticData.StartHeroesIds())
                _heroesService.MarkAsAvailable(startHeroId);
            
            _stateMachine.Enter<BattleLoopState>();
        }
    }
}