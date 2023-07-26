using Infrastructure.Factory.Pools;
using PlayerLogic;
using Services.Assets;
using Services.Factory;
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
    }
}