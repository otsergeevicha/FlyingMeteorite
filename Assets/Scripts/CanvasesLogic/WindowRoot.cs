using CanvasesLogic.Authorization;
using CanvasesLogic.ContentsFrames;
using CanvasesLogic.GameOver;
using CanvasesLogic.Hud;
using CanvasesLogic.LeaderboardModule;
using CanvasesLogic.Menu;
using CanvasesLogic.Shop;
using ObstaclesLogic;
using PlayerLogic;
using Plugins.MonoCache;
using Services.SaveLoad;
using Services.Wallet;
using SoundsLogic;
using UnityEngine;

namespace CanvasesLogic
{
    [RequireComponent(typeof(Canvas))]
    public class WindowRoot : MonoCache
    {
        private WindowHud _windowHud;
        private GameOverScreen _gameOverScreen;
        private MenuScreen _menuScreen;
        private AuthorizationScreen _authorizationScreen;
        private LeaderboardScreen _leaderboardScreen;
        private ShopScreen _shopScreen;
        private ViewMainCharacter _viewMainCharacter;

        public void Construct(Hero hero, ISave save, SoundOperator soundOperator,
            ObstaclesModule obstaclesModule)
        {
            _windowHud = ChildrenGet<WindowHud>();
            _leaderboardScreen = ChildrenGet<LeaderboardScreen>();
            _authorizationScreen = ChildrenGet<AuthorizationScreen>();
            _shopScreen = ChildrenGet<ShopScreen>();
            _menuScreen = ChildrenGet<MenuScreen>();
            _gameOverScreen = ChildrenGet<GameOverScreen>();
            _viewMainCharacter = ChildrenGet<ViewMainCharacter>();
            
            _windowHud.Inject(hero);
            _shopScreen.Inject(hero, save, _menuScreen);
            _gameOverScreen.Inject(hero, _windowHud, _menuScreen, soundOperator, obstaclesModule);
            _viewMainCharacter.Inject(_shopScreen);
            _menuScreen.Inject(_windowHud, hero, _shopScreen, _leaderboardScreen, soundOperator, _viewMainCharacter, obstaclesModule);
            _leaderboardScreen.Inject(save, _authorizationScreen, _menuScreen);
            _authorizationScreen.Inject(_menuScreen, _leaderboardScreen);
            
            hero.Died += HeroOnDied;

            Time.timeScale = 0;
            FirstConfigWindows();
            
            soundOperator.PlayMainSound();
        }

        public void FirstConfigWindows()
        {
            _menuScreen.OnActive();

            _windowHud.InActive();
            _gameOverScreen.InActive();
            _authorizationScreen.InActive();
            _leaderboardScreen.InActive();
            _shopScreen.InActive();
        }
        
        private void HeroOnDied() =>
            _gameOverScreen.OnActive();
    }
}