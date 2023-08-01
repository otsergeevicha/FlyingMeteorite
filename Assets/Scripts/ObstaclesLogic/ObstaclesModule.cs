using System.Linq;
using Infrastructure.Factory.Pools;
using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace ObstaclesLogic
{
    public class ObstaclesModule : MonoCache
    {
        private Pool _pool;
        private Camera _camera;
        private Hero _hero;
        private float _currentSpawnPosition;
        private bool _isAlive;
        private float _elapsedTime;

        protected override void UpdateCached()
        {
            if (!_isAlive)
                return;

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > Constants.SpawnInterval)
            {
                _elapsedTime = 0;
                Spawn();
            }
        }

        public void Inject(Hero hero, Pool pool, Camera ourCamera)
        {
            _hero = hero;
            _camera = ourCamera;
            _pool = pool;
        }

        public void ResetObstacles()
        {
            StopSpawn();
            
            foreach (Obstacle obstacle in _pool.GetObstaclePool().Get())
                obstacle.InActive();
        }
        
        public void Launch() => 
            _isAlive = true;

        public void StopSpawn() => 
            _isAlive = false;

        private void Spawn()
        {
            OnActiveObstacle();
            DisableObstacle();
        }

        private void DisableObstacle()
        {
            Vector3 disablePoint = GetViewportToWorldPoint();

            _pool.GetObstaclePool().Get().FirstOrDefault(obstacle =>
                obstacle.gameObject.activeSelf && obstacle.transform.position.x < disablePoint.x)?.InActive();
        }

        private void OnActiveObstacle()
        {
            Vector3 currentPointSpawn = GetCurrentPointSpawn();
            _pool.TryGetObstacle().Active(currentPointSpawn);
        }

        private Vector3 GetCurrentPointSpawn() =>
            new(_hero.transform.position.x + Constants.OffSetXSpawn, GetRandomPositionY(),
                _camera.transform.position.z - Constants.OffSetZSpawn);

        private float GetRandomPositionY() =>
            _camera.transform.position.y + Random.Range(Constants.MinRandomPositionY, Constants.MaxRandomPositionY);

        private Vector3 GetViewportToWorldPoint() =>
            _camera.ViewportToWorldPoint(new Vector2(0, .5f));
    }
}