using Gameplay.HeroEvents;
using Infrastructure.States.StateInfrastructure;
using Infrastructure.States.StateMachine;
using Nani.UI.Custom.EventsDescription;
using Nani.UI.Custom.HeroCards;
using Nani.UI.Custom.HeroEvents;
using Services.ConstructNaniUI;

namespace Infrastructure.States.GameStates.Nani
{
    public class ConstructNaniUIState : SimplePayloadState<RegisterNaninovelPayload>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConstructNaniUIService _constructNaniUI;

        public ConstructNaniUIState(IGameStateMachine gameStateMachine, IConstructNaniUIService constructNaniUI)
        {
            _gameStateMachine = gameStateMachine;
            _constructNaniUI = constructNaniUI;
        }

        public override void Enter(RegisterNaninovelPayload payload)
        {
            base.Enter(payload);
            
            var uiManager = payload.UIManager;

            _constructNaniUI.To(uiManager.GetUI<HeroCardsUI>());
            _constructNaniUI.To(uiManager.GetUI<HeroEventsUI>());
            _constructNaniUI.To(uiManager.GetUI<EventsDescriptionUI>());
            _constructNaniUI.To(uiManager.GetUI<HeroEventResultUI>());
            
            OnConstructed();
        }

        private void OnConstructed() => _gameStateMachine.Enter<ResolveNonLazyState>();
    }
}