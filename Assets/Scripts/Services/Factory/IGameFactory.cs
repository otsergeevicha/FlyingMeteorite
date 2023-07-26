using Infrastructure.Factory;
using Infrastructure.Factory.Pools;
using PlayerLogic;
using Services.ServiceLocator;
using UnityEngine;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        Hero CreateHero();
        WindowRoot CreateWindowRoot();
        Camera CreateCamera();
        Obstacle CreateObstacle();
        Pool CreatePool();
    }
}