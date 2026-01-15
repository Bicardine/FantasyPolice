using Gameplay.StaticData;
using Infrastructure.States.GameStates.Nani;
using Infrastructure.States.StateInfrastructure;
using Infrastructure.States.StateMachine;

namespace Infrastructure.States.GameStates
{
    public class BootstrapState : SimpleState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IGameStateMachine stateMachine, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }

        public override void Enter()
        {
            _staticDataService.LoadAll();

            _stateMachine.Enter<InitializeNaniState>();
        }
    }
}