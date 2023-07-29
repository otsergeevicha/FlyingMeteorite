﻿using Agava.YandexGames;
using Infrastructure.LoadingLogic;
using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameOverScreen : MonoCache
    {
        private WindowHud _windowHud;
        private MenuScreen _menuScreen;
        private Hero _hero;
        private SoundOperator _soundOperator;

        public void Inject(Hero hero, WindowHud windowHud, MenuScreen menuScreen, SoundOperator soundOperator)
        {
            _soundOperator = soundOperator;
            _hero = hero;
            _menuScreen = menuScreen;
            _windowHud = windowHud;
        }

        public void SelectContinue()
        {
#if !UNITY_WEBGL || !UNITY_EDITOR

            VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
            return;
#endif
            OnRewardedCallback();
        }

        public void SelectRestart()
        {
            _hero.ResetPlayer();
            _windowHud.OnActive();
            _windowHud.Revival();
            _windowHud.RenderScore();
            _soundOperator.UnLockGame();
            InActive();
            _soundOperator.PlayMainSound();
        }

        public void SelectClose()
        {
            _windowHud.InActive();
            _menuScreen.OnActive();

            _soundOperator.UnLockGame();

            InActive();
            
            _soundOperator.PlayMainSound();
        }

        public void OnActive()
        {
            _soundOperator.PlayGameOverSound();
            gameObject.SetActive(true);
        }

        public void InActive() =>
            gameObject.SetActive(false);

        private void OnOpenCallback() => 
            _soundOperator.LockGame();

        private void OnRewardedCallback()
        {
            _windowHud.OnActive();
            _windowHud.Revival();
            _hero.ResetMovement();
            _soundOperator.UnLockGame();
            InActive();
        }

        private void OnCloseCallback() =>
            SelectClose();

        private void OnErrorCallback(string description) =>
            SelectClose();
    }
}