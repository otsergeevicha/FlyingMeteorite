using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure.Factory.Pools;
using PlayerLogic;
using UnityEngine;

namespace Infrastructure.GameAI.StateMachine.States
{
    public class ObstaclesModule
    {
        private readonly Pool _pool;
        private readonly Camera _camera;
        private readonly Hero _hero;
        private float _currentSpawnPosition;
        private bool _isAlive;

        public ObstaclesModule(Hero hero, Pool pool, Camera camera)
        {
            _hero = hero;
            _camera = camera;
            _pool = pool;
            
            
        }

        public void ResetObstacles()
        {
            foreach (Obstacle obstacle in _pool.GetObstaclePool().Get()) 
                obstacle.InActive();

            _isAlive = false;
        }

        public void Launch()
        {
            _isAlive = true;
            _ = SpawnObstacles();
        }
        
        private void OnActiveObstacle()
        {
            Vector3 currentPointSpawn = GetCurrentPointSpawn();
            _pool.TryGetObstacle().Active(currentPointSpawn);
        }

        private async UniTaskVoid SpawnObstacles()
        {
            while (_isAlive)
            {
                _currentSpawnPosition = _hero.transform.position.x + Constants.OffSetXSpawn;
                Spawn();
                await UniTask.Delay(Constants.SpawnInterval);
            }
        }

        private Vector3 GetCurrentPointSpawn() =>
            new (_camera.transform.position.x + Constants.OffSetXSpawn, GetRandomPositionY(),
                _camera.transform.position.z - Constants.OffSetZSpawn);

        private void DisableObstacle()
        {
            Vector3 disablePoint = GetViewportToWorldPoint();
            
            _pool.GetObstaclePool().Get().FirstOrDefault(obstacle =>
                obstacle.gameObject.activeSelf && obstacle.transform.position.x < disablePoint.x)?.InActive();
        }

        private Vector3 GetViewportToWorldPoint() => 
            _camera.ViewportToWorldPoint(new Vector2(0, .5f));

        private float GetRandomPositionY() =>
            _camera.transform.position.y + Random.Range(Constants.MinRandomPositionY, Constants.MaxRandomPositionY);


        private void Spawn()
        {
            OnActiveObstacle();
            DisableObstacle();
        }
    }
}