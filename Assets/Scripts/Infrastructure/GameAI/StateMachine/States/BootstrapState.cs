﻿using Infrastructure.Assets;
using Infrastructure.Factory;
using Infrastructure.LoadingLogic;
using SaveLoadLogic.Base;
using Services.Assets;
using Services.Factory;
using Services.Inputs;
using Services.SaveLoad;
using Services.ServiceLocator;
using Services.StateMachine;
using Services.Wallet;
using WalletLogic;

namespace Infrastructure.GameAI.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.LoadScene(Constants.InitialScene, EnterLoadLevel);

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(Constants.MainScene);

        public void Exit() {}

        private void RegisterServices()
        {
            ServiceLocator.Container.RegisterSingle<ISave>(new SaveLoad());
            ServiceLocator.Container.RegisterSingle<IInputService>(new InputService());
            ServiceLocator.Container.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            ServiceLocator.Container.RegisterSingle<IWallet>(new Wallet(ServiceLocator.Container.Single<ISave>()));
            ServiceLocator.Container.RegisterSingle<IGameFactory>(new GameFactory(ServiceLocator.Container.Single<IAssetsProvider>()));
        }
    }
}