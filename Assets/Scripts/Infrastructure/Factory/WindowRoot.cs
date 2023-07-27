using Infrastructure.GameAI.StateMachine.States;
using PlayerLogic;
using Plugins.MonoCache;
using Services.Factory;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class WindowRoot : MonoCache
    {
        private WindowHud _windowHud;
        private GameOverScreen _gameOverScreen;
        private MenuScreen _menuScreen;
        private AuthorizationScreen _authorizationScreen;
        private LeaderboardScreen _leaderboardScreen;
        private ShopScreen _shopScreen;

        public void Construct(Hero hero, IWallet wallet, ISave save)
        {
            _windowHud = ChildrenGet<WindowHud>();
            _windowHud.Inject(hero);
            
            _leaderboardScreen = ChildrenGet<LeaderboardScreen>();
            
            _authorizationScreen = ChildrenGet<AuthorizationScreen>();
            
            _shopScreen = ChildrenGet<ShopScreen>();
            _shopScreen.Inject(wallet, hero, save);
            
            _menuScreen = ChildrenGet<MenuScreen>();
            _menuScreen.Inject(_windowHud, hero, _shopScreen, _leaderboardScreen);

            _gameOverScreen = ChildrenGet<GameOverScreen>();
            _gameOverScreen.Inject(hero, _windowHud, _menuScreen);
            
            hero.Died += HeroOnDied;

            FreezeTime();
            FirstConfigWindows();
        }

        private void FirstConfigWindows()
        {
            _menuScreen.OnActive();

            _windowHud.InActive();
            _gameOverScreen.InActive();
            _authorizationScreen.InActive();
            _leaderboardScreen.InActive();
            _shopScreen.InActive();
        }

        private void FreezeTime() =>
            Time.timeScale = 0;

        private void HeroOnDied() =>
            _gameOverScreen.OnActive();
    }
}