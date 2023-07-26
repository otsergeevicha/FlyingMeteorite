using PlayerLogic;
using Plugins.MonoCache;

namespace Infrastructure.Factory
{
    public class WindowRoot : MonoCache
    {
        private WindowHud _windowHud;
        private GameOverScreen _gameOverScreen;
        private MenuScreen _menuScreen;

        public void Construct(Hero hero)
        {
            _windowHud = GetComponentInChildren<WindowHud>();
            _windowHud.Inject(hero);

            _menuScreen = GetComponentInChildren<MenuScreen>();
            _menuScreen.Inject();
            
            _gameOverScreen = GetComponentInChildren<GameOverScreen>();
            _gameOverScreen.Inject(hero, _windowHud, _menuScreen);
            
            hero.Died += HeroOnDied;
        }

        private void HeroOnDied() =>
            _gameOverScreen.OnActive();
    }

    public class AuthorizationScreen : MonoCache {}
    public class LeaderboardScreen : MonoCache {}
}