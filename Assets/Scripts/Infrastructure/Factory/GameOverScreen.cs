using Agava.YandexGames;
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

        public void Inject(Hero hero, WindowHud windowHud, MenuScreen menuScreen)
        {
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
            Time.timeScale = 1;
            InActive();
        }

        public void SelectClose()
        {
            _windowHud.InActive();
            _menuScreen.OnActive();

            Time.timeScale = 0;

            InActive();
        }

        public void OnActive() =>
            gameObject.SetActive(true);

        public void InActive() =>
            gameObject.SetActive(false);

        private void OnOpenCallback() {}

        private void OnRewardedCallback()
        {
            _windowHud.OnActive();
            _windowHud.Revival();
            _hero.ResetMovement();
            Time.timeScale = 1;
            InActive();
        }

        private void OnCloseCallback() =>
            SelectClose();

        private void OnErrorCallback(string description) =>
            SelectClose();
    }
}