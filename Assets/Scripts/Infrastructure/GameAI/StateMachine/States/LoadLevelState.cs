using System.Linq;
using CameraLogic;
using Infrastructure.Factory.Pools;
using Infrastructure.LoadingLogic;
using Infrastructure.LoadingLogic.ScreenLoading;
using PlayerLogic;
using Services.Factory;
using Services.StateMachine;
using UnityEngine;

namespace Infrastructure.GameAI.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private ObstaclesModule _obstaclesModule;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            Camera camera = _gameFactory.CreateCamera();
            Hero hero = _gameFactory.CreateHero();
            camera.GetComponent<HeroTracker>().Construct(hero);
            _gameFactory.CreateHud();
            
            Pool pool = _gameFactory.CreatePool();
            _obstaclesModule = new ObstaclesModule(hero, pool, camera);

            _stateMachine.Enter<GameLoopState>();
        }
    }
}