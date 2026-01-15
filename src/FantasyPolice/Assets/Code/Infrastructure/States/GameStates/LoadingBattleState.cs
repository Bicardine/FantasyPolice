using Infrastructure.Loading;
using Infrastructure.States.StateInfrastructure;
using Infrastructure.States.StateMachine;

namespace Infrastructure.States.GameStates
{
    public class LoadingBattleState : SimplePayloadState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadingBattleState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public override void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName, EnterBattleLoopState);
        }

        private void EnterBattleLoopState()
        {
            _stateMachine.Enter<BattleEnterState>();
        }
    }
}