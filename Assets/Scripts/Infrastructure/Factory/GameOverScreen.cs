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

        public void Inject(Hero hero, WindowHud windowHud, MenuScreen menuScreen)
        {
            _menuScreen = menuScreen;
            _windowHud = windowHud;
        }

        public void SelectContinue() => 
            VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);

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

        private void OnRewardedCallback() => 
            _windowHud.Revival();

        private void OnCloseCallback() => 
            SelectClose();

        private void OnErrorCallback(string description) => 
            SelectClose();
    }
}