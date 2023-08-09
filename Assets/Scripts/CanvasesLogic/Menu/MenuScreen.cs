using CanvasesLogic.ContentsFrames;
using CanvasesLogic.Hud;
using CanvasesLogic.LeaderboardModule;
using CanvasesLogic.Shop;
using ObstaclesLogic;
using PlayerLogic;
using Plugins.MonoCache;
using SoundsLogic;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasesLogic.Menu
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
        private ViewMainCharacter _viewMainCharacter;
        private ObstaclesModule _obstaclesModule;
        
        public void Inject(WindowHud windowHud, Hero hero, ShopScreen shopScreen, LeaderboardScreen leaderboardScreen,
            SoundOperator soundOperator, ViewMainCharacter viewMainCharacter, ObstaclesModule obstaclesModule)
        {
            _obstaclesModule = obstaclesModule;
            _viewMainCharacter = viewMainCharacter;
            _soundOperator = soundOperator;
            _leaderboardScreen = leaderboardScreen;
            _shopScreen = shopScreen;
            _hero = hero;
            _windowHud = windowHud;

            Time.timeScale = 0;
        }

        public void SelectPlay()
        {
            _hero.ResetPlayer();
            _windowHud.OnActive();
            _windowHud.Revival();
            _windowHud.RenderScore();
            _obstaclesModule.ResetObstacles();
            _obstaclesModule.Launch();
            
            _hero.Active();
            
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

        public void OnActive()
        {
            gameObject.SetActive(true);
            _viewMainCharacter.UpdateMainIcon();
        }

        public void InActive() => 
            gameObject.SetActive(false);

        private void ChangeIconSound(bool flag)
        {
            _activeSoundIcon.gameObject.SetActive(flag);
            _inActiveSoundIcon.gameObject.SetActive(!flag);
        }
    }
}