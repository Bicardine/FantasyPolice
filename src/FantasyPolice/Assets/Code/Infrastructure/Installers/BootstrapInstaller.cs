using Gameplay.HeroBased;
using Gameplay.HeroBased.HeroProgressBased;
using Gameplay.HeroEvents;
using Gameplay.StaticData;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Loading;
using Infrastructure.States.Factory;
using Infrastructure.States.GameStates;
using Infrastructure.States.GameStates.Nani;
using Infrastructure.States.StateMachine;
using RSG;
using Services.ConstructNaniUI;
using Services.InputService;
using Services.Interaction;
using Services.LogVersion;
using Services.MessageBased;
using Services.Positions;
using Services.Render;
using Services.SlotsDragDrop;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR == false
            BindLogVersion();
#endif
            BindInfrastructureServices();
            BindAssetManagementServices();
            BindCommonServices();
            BindGameplayNonLazyServices();
            BindGameplayServices();
            BindGameplayFactories();
            BindStateMachine();
            BindStateFactory();
            BindGameStates();
            BindNaniRelativeServices();
        }

        public void Initialize()
        {
#if UNITY_EDITOR == false
            LogVersion();
#endif
            Promise.UnhandledException += LogPromiseException;
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }


#if UNITY_EDITOR == false
        private void BindLogVersion()
        {
            Container.BindInterfacesAndSelfTo<LogVersionService>().AsSingle();
        }
#endif

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();

            Container.BindInterfacesAndSelfTo<InitializeNaniState>().AsSingle();
            Container.BindInterfacesAndSelfTo<RegisterNaniState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstructNaniUIState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResolveNonLazyState>().AsSingle();

            Container.BindInterfacesAndSelfTo<LoadingBattleState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleEnterState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleLoopState>().AsSingle();
        }

        private void BindNaniRelativeServices()
        {
            Container.BindInterfacesAndSelfTo<ConstructNaniUIService>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IGameplayMessagesService>().To<GameplayMessagesService>().AsSingle();
            Container.Bind<ISlotsInteractionService>().To<SlotsInteractionService>().AsSingle();
            Container.Bind<IInteractorService>().To<InteractorService>().AsSingle();
            Container.Bind<IPositionsService>().To<PositionsService>().AsSingle();

            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IHeroesService>().To<HeroesService>().AsSingle();
            Container.Bind<IHeroEventsService>().To<HeroEventsService>().AsSingle();
        }

        private void BindGameplayNonLazyServices()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SlotsDragDropService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelUpService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardsSortingRenderService>().AsSingle();
        }

        private void BindGameplayFactories()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindAssetManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void LogPromiseException(object sender, ExceptionEventArgs e)
        {
            Debug.LogError(e.Exception);
        }


#if UNITY_EDITOR == false
        private void LogVersion()
        {
            Container.Resolve<ILogVersionService>().Do();
        }
#endif
    }
}