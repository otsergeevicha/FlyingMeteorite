using System;
using Infrastructure.GameAI.StateMachine.States;
using PlayerLogic.Move;
using Plugins.MonoCache;
using Services.Inputs;
using Services.Wallet;
using UnityEngine;
using UnityEngine.Playables;

namespace PlayerLogic
{
    [RequireComponent(typeof(HeroMovement))]
    [RequireComponent(typeof(HeroCollisionHandler))]
    public class Hero : MonoCache
    {
        [SerializeField] private SpriteRenderer _iconCharacter;

        private HeroMovement _movement;
        private IWallet _wallet;
        private IInputService _input;
        public event Action ScoreChanged;
        public event Action Collided;
        public event Action Died;

        private void Start()
        {
            _movement = Get<HeroMovement>();
            _input = _movement.GetInputService();
            Active();
        }

        public void Construct(IWallet wallet) =>
            _wallet = wallet;

        public int CurrentScore { get; private set; }

        public void Active()
        {
            gameObject.SetActive(true);
            _input.OnControls();
        }

        public void InActive()
        {
            _input.OffControls();
            gameObject.SetActive(false);
        }

        public void ResetPlayer()
        {
            CurrentScore = 0;
            _movement.ResetHero();
        }

        public void ResetMovement() =>
            _movement.ResetHero();

        public void Collision() =>
            Collided?.Invoke();

        public void Die()
        {
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