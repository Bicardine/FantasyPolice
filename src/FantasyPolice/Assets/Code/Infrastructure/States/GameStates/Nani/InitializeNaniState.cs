using Infrastructure.States.StateInfrastructure;
using Infrastructure.States.StateMachine;
using Naninovel;
using UnityEngine;

namespace Infrastructure.States.GameStates.Nani
{
    public class InitializeNaniState : SimpleState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public InitializeNaniState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public override async void Enter()
        {
            base.Enter();
            
            if (Engine.Initialized == false)
                await RuntimeInitializer.Initialize();
            
            _gameStateMachine.Enter<RegisterNaniState, RegisterNaninovelPayload>(
                new RegisterNaninovelPayload(Engine.GetService<IUIManager>()));
        }
    }
}