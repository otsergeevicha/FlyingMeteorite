using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.Factory.Pools
{
    public class Obstacle : MonoCache
    {
        public void Active(Vector3 currentPointSpawn)
        {
            transform.position = currentPointSpawn;
            gameObject.SetActive(true);
        }

        public void InActive() => 
            gameObject.SetActive(false);
    }
}