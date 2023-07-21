using System;
using Plugins.MonoCache;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(HeroMovement))]
    public class Hero : MonoCache
    {
        private HeroMovement _movement;
        private int _score;
        
        private void Start()
        {
            _movement = Get<HeroMovement>();
        }

        public void ResetPlayer()
        {
            _score = 0;
            _movement.ResetHero();
        }

        public void Die()
        {
            print("Died");
            Time.timeScale = 0;
        }
    }

    [RequireComponent(typeof(Hero))]
    public class HeroCollisionHandler : MonoCache
    {
        private Hero _hero;

        private void Start() => 
            _hero = Get<Hero>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}