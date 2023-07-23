using Plugins.MonoCache;
using UnityEngine;

namespace PlayerLogic
{
    public class HeroCollisionHandler : MonoCache
    {
        private Hero _hero;

        private void Start() => 
            _hero = Get<Hero>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ScoreZone _)) 
                _hero.IncreaseScore();
            // else
            //     _hero.Die();
        }
    }
}