using System;
using Plugins.MonoCache;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(HeroMovement))]
    [RequireComponent(typeof(HeroCollisionHandler))]
    public class Hero : MonoCache
    {
        private HeroMovement _movement;
        private int _score;
        public event Action ScoreChanged;
        public event Action Died;

        private void Start() => 
            _movement = Get<HeroMovement>();

        public void ResetPlayer()
        {
            _score = 0;
            _movement.ResetHero();
            Died?.Invoke();
        }

        public void Die()
        {
            print("Died");
            Time.timeScale = 0;
            Died?.Invoke();
        }

        public void IncreaseScore()
        {
            _score++;
            _movement.IncreaseSpeed(_score);
            ScoreChanged?.Invoke();
        }
    }
}