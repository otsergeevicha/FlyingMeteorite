using PlayerLogic;
using Services.ServiceLocator;
using UnityEngine;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        Hero CreateHero();
        void CreateHud();
        Camera CreateCamera();
    }
}