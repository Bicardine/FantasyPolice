using System.Collections.Generic;
using Infrastructure.States.StateInfrastructure;
using Infrastructure.States.StateMachine;
using Model.Data;
using Zenject;

namespace Infrastructure.States.GameStates
{
    public class ResolveNonLazyState : SimpleState
    {
        private readonly DiContainer _container;
        private readonly IGameStateMachine _stateMachine;
        
        private const string VillageSceneName = "Village";

        public ResolveNonLazyState(DiContainer container, IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public override void Enter()
        {
            _container.Resolve<IEnumerable<INonLazyResolveable>>();
            
            _stateMachine.Enter<LoadingBattleState, string>(VillageSceneName);
        }
    }
}