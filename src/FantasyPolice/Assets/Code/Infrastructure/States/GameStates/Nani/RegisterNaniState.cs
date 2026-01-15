using System.Diagnostics;
using Gameplay.HeroEvents;
using Infrastructure.States.StateInfrastructure;
using Infrastructure.States.StateMachine;
using Model.Data;
using Nani.Cameras;
using Nani.UI.Custom.EventsDescription;
using Naninovel;
using Zenject;

namespace Infrastructure.States.GameStates.Nani
{
    public class RegisterNaniState : SimplePayloadState<RegisterNaninovelPayload>
    {
        private readonly IGameStateMachine _gameState;
        private readonly DiContainer _container;

        public RegisterNaniState(IGameStateMachine gameState, DiContainer container)
        {
            _gameState = gameState;
            _container = container;
        }

        public override void Enter(RegisterNaninovelPayload payload)
        {
            base.Enter(payload);
            
            RegisterNaninovelServices(payload.UIManager);
            RegisterUI(payload.UIManager);
            RegisterOther();

            _gameState.Enter<ConstructNaniUIState, RegisterNaninovelPayload>(payload);
        }


        private void RegisterNaninovelServices(IUIManager uiManager)
        {
            _container.Bind<IUIManager>().FromInstance(uiManager).AsSingle();
            _container.Bind<IScriptPlayer>().FromInstance(Engine.GetService<IScriptPlayer>()).AsSingle();
            _container.Bind<ICameraManager>().FromInstance(Engine.GetService<ICameraManager>()).AsSingle();
            _container.Bind<IStateManager>().FromInstance(Engine.GetService<IStateManager>()).AsSingle();
            _container.Bind<ICustomVariableManager>().FromInstance(Engine.GetService<ICustomVariableManager>()).AsSingle();
        }

        private void RegisterOther()
        {
            var cameraManager = _container.Resolve<ICameraManager>();
            _container.Bind<ICamera>().FromInstance(new Camera(cameraManager.Camera));
            _container.Bind<IUICamera>().FromInstance(new UICamera(cameraManager.UICamera));
        }

        private void RegisterUI(IUIManager uiManager)
        {
            _container.Bind<EventsDescriptionUI>().FromInstance(uiManager.GetUI<EventsDescriptionUI>()).AsSingle();
            _container.Bind<HeroEventResultUI>().FromInstance(uiManager.GetUI<HeroEventResultUI>()).AsSingle();
        }
    }

    public class RegisterNaninovelPayload
    {
        public IUIManager UIManager { get; }

        public RegisterNaninovelPayload(IUIManager uiManager)
        {
            UIManager = uiManager;
        }
    }
}