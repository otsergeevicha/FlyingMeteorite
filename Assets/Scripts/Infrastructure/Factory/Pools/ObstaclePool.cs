using Services.Factory;

namespace Infrastructure.Factory.Pools
{
    public class ObstaclePool
    {
        private readonly Obstacle[] _pipes = new Obstacle[Constants.CountSpawnPipe];
        
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