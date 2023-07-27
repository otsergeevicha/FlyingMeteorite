using System;
using Infrastructure.GameAI.StateMachine.States;
using Plugins.MonoCache;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(HeroMovement))]
    [RequireComponent(typeof(HeroCollisionHandler))]
    public class Hero : MonoCache
    {
        [SerializeField] private SpriteRenderer _iconCharacter;
        
        private HeroMovement _movement;
        private IWallet _wallet;
        public event Action ScoreChanged;
        public event Action Collided;
        public event Action Died;

        private void Start() => 
            _movement = Get<HeroMovement>();

        public void Construct(IWallet wallet) => 
            _wallet = wallet;

        public int CurrentScore { get; private set; }

        public void ResetPlayer()
        {
            CurrentScore = 0;
            _movement.ResetHero();
        }

        public void Collision()
        {
            print("Collision");
            Collided?.Invoke();
        }

        public void Die()
        {
            print("Died");
            _wallet.Apply(CurrentScore);
            Time.timeScale = 0;
            Died?.Invoke();
        }

        public void IncreaseScore()
        {
            CurrentScore++;
            _movement.IncreaseSpeed(CurrentScore);
            ScoreChanged?.Invoke();
        }

        public void ChangeHeroIcon(Sprite character) => 
            _iconCharacter.sprite = character;
    }
}