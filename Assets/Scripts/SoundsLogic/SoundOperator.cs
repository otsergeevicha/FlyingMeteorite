﻿using Agava.WebUtility;
using Agava.YandexGames;
using Plugins.MonoCache;
using UnityEngine;

namespace SoundsLogic
{
    public class SoundOperator : MonoCache
    {
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private AudioSource _playSound;
        [SerializeField] private AudioSource _gameOverSound;

        public bool IsSoundStatus { get; private set; } = true;

        protected override void OnEnabled()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
            PlayMainSound();
        }

        protected override void OnDisabled() => 
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

        public void Mute()
        {
            _audioListener.enabled = false;
            _playSound.volume = 0;
            _gameOverSound.volume = 0;
            IsSoundStatus = false;
        }

        public void UnMute()
        {
            _audioListener.enabled = true;
            _playSound.volume = 1;
            _gameOverSound.volume = 1;
            IsSoundStatus = true;

        }

        public void LockGame()
        {
            Time.timeScale = 0;
            Mute();
        }
        
        public void UnLockGame()
        {
            Time.timeScale = 1;
            UnMute();
        }

        public void PlayMainSound()
        {
            _gameOverSound.Stop();
            _gameOverSound.volume = 0;
            
            _playSound.Play();
            _playSound.volume = 1;
        }

        public void PlayGameOverSound()
        {
            _playSound.Stop();
            _playSound.volume = 0;
            
            _gameOverSound.Play();
            _gameOverSound.volume = 1;
        }
        
        private void OnInBackgroundChange(bool inBackground)
        {
            if (inBackground)
                OnOpenCallback();
            
            if (!inBackground) 
                InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
        }

        private void OnOpenCallback()
        {
            Mute();
            Time.timeScale = 0;
        }

        private void OnCloseCallback(bool obj) => 
            UnLockGame();

        private void OnErrorCallback(string obj) => 
            UnLockGame();
    }
}