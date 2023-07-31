using System.Linq;
using Infrastructure.Factory.Pools.Obstacles;
using ObstaclesLogic;
using Plugins.MonoCache;
using Services.Factory;
using Services.ServiceLocator;

namespace Infrastructure.Factory.Pools
{
    public class Pool : MonoCache
    {
        private ObstaclePool _obstaclePool;

        private void Awake() => 
            _obstaclePool = new ObstaclePool(ServiceLocator.Container.Single<IGameFactory>());
        
        public Obstacle TryGetObstacle() =>
        _obstaclePool.Get().FirstOrDefault(obstacle =>
            obstacle.isActiveAndEnabled == false);

        public ObstaclePool GetObstaclePool() =>
            _obstaclePool;
    }
}