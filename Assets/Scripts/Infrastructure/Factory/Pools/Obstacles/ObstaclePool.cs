using ObstaclesLogic;
using Services.Factory;

namespace Infrastructure.Factory.Pools.Obstacles
{
    public class ObstaclePool
    {
        private readonly Obstacle[] _pipes = new Obstacle[Constants.CountSpawnObstacle];
        
        public ObstaclePool(IGameFactory factory)
        {
            for (int i = 0; i < _pipes.Length; i++)
            {
                _pipes[i] = factory.CreateObstacle();
                _pipes[i].gameObject.SetActive(false);
            }
        }

        public Obstacle[] Get() =>
            _pipes;
    }
}