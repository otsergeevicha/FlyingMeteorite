using System.Net;
using Infrastructure.LoadingLogic;
using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factory
{
    public class MenuScreen : MonoCache
    {
        [SerializeField] private Image _activeSoundIcon;
        [SerializeField] private Image _inActiveSoundIcon;
        [SerializeField] private Toggle _toggleSound;
        
        private WindowHud _windowHud;
        private Hero _hero;
        private ShopScreen _shopScreen;
        private LeaderboardScreen _leaderboardScreen;
        private SoundOperator _soundOperator;

        public void Inject(WindowHud windowHud, Hero hero, ShopScreen shopScreen, LeaderboardScreen leaderboardScreen,
            SoundOperator soundOperator)
        {
            _soundOperator = soundOperator;
            _leaderboardScreen = leaderboardScreen;
            _shopScreen = shopScreen;
            _hero = hero;
            _windowHud = windowHud;
        }

        public void SelectPlay()
        {
            _hero.ResetPlayer();
            _windowHud.OnActive();
            _windowHud.Revival();
            Time.timeScale = 1;
            InActive();
        }

        public void SelectSound()
        {
            if (_toggleSound.isOn) 
                _soundOperator.UnMute();

            if (!_toggleSound.isOn) 
                _soundOperator.Mute();
            
            ChangeIconSound(_toggleSound.isOn);
        }

        public void SelectLeaderBoard()
        {
            _leaderboardScreen.OnActive();
            InActive();
        }

        public void SelectShop()
        {
            _shopScreen.OnActive();
            InActive();
        }

        public void OnActive() => 
            gameObject.SetActive(true);

        public void InActive() => 
            gameObject.SetActive(false);

        private void ChangeIconSound(bool flag)
        {
            _activeSoundIcon.gameObject.SetActive(flag);
            _inActiveSoundIcon.gameObject.SetActive(!flag);
        }
    }
}