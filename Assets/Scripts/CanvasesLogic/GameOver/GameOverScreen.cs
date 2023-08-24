using CanvasesLogic.Hud;
using CanvasesLogic.Menu;
using ObstaclesLogic;
using PlayerLogic;
using Plugins.MonoCache;
using SoundsLogic;
using UnityEngine;

namespace CanvasesLogic.GameOver
{
    public class GameOverScreen : MonoCache
    {
        private WindowHud _windowHud;
        private MenuScreen _menuScreen;
        private Hero _hero;
        private SoundOperator _soundOperator;
        private ObstaclesModule _obstaclesModule;

        public void Inject(Hero hero, WindowHud windowHud, MenuScreen menuScreen, SoundOperator soundOperator,
            ObstaclesModule obstaclesModule)
        {
            _obstaclesModule = obstaclesModule;
            _soundOperator = soundOperator;
            _hero = hero;
            _menuScreen = menuScreen;
            _windowHud = windowHud;
        }

        public void SelectRestart()
        {
            _windowHud.OnActive();
            _windowHud.Revival();
            _windowHud.ResetScore();
            _hero.ResetPlayer();

            Time.timeScale = 1;

            _obstaclesModule.ResetObstacles();
            _obstaclesModule.Launch();

            InActive();
            _hero.Active();

            PlaySound();
        }

        public void SelectClose()
        {
            _windowHud.InActive();
            _menuScreen.OnActive();

            Time.timeScale = 0;
            _hero.ResetPlayer();
            InActive();

            PlaySound();
        }

        public void OnActive()
        {
            _hero.InActive();
            
            if (_soundOperator.IsSoundStatus)
                _soundOperator.PlayGameOverSound();

            gameObject.SetActive(true);
        }

        public void InActive() =>
            gameObject.SetActive(false);

        private void PlaySound()
        {
            if (_soundOperator.IsSoundStatus)
                _soundOperator.PlayMainSound();
        }
    }
}