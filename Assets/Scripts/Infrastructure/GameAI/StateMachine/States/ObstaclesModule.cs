using System.Linq;
using Infrastructure.Factory.Pools;
using PlayerLogic;
using UnityEngine;

namespace Infrastructure.GameAI.StateMachine.States
{
    public class ObstaclesModule
    {
        private readonly Pool _pool;
        private readonly Camera _camera;
        private Hero _hero;

        public ObstaclesModule(Hero hero, Pool pool, Camera camera)
        {
            _hero = hero;
            _camera = camera;
            _pool = pool;

            _hero.ScoreChanged += Spawn;
            _hero.Died += Dispose;

            Spawn();
        }

        private void Spawn()
        {
            OnActiveObstacle();
            DisableObstacle();
        }

        private void OnActiveObstacle()
        {
            Vector3 currentPointSpawn = GetCurrentPointSpawn();
            _pool.TryGetObstacle().Active(currentPointSpawn);
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

        private void Dispose()
        {
            _hero.ScoreChanged -= Spawn;
            _hero.Died -= Dispose;
        }
    }
}