using CanvasesLogic;
using Infrastructure.Factory.Pools;
using ObstaclesLogic;
using PlayerLogic;
using Services.Assets;
using Services.Factory;
using SoundsLogic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public GameFactory(IAssetsProvider assetsProvider) => 
            _assetsProvider = assetsProvider;

        public Hero CreateHero() => 
            _assetsProvider.InstantiateEntity(Constants.PlayerPath).GetComponent<Hero>();

        public WindowRoot CreateWindowRoot() => 
            _assetsProvider.InstantiateEntity(Constants.WindowRootPath).GetComponent<WindowRoot>();

        public Camera CreateCamera() => 
            _assetsProvider.InstantiateEntity(Constants.CameraPath).GetComponent<Camera>();

        public Obstacle CreateObstacle() => 
            _assetsProvider.InstantiateEntity(Constants.ObstaclePath).GetComponent<Obstacle>();

        public Pool CreatePool() => 
            _assetsProvider.InstantiateEntity(Constants.PoolPath).GetComponent<Pool>();

        public ObstaclesModule CreateObstacleModule() => 
            _assetsProvider.InstantiateEntity(Constants.ObstaclesModulePath).GetComponent<ObstaclesModule>();

        public SoundOperator CreateSoundOperator() => 
            _assetsProvider.InstantiateEntity(Constants.SoundOperatorPath).GetComponent<SoundOperator>();
    }
}