using Infrastructure.GameAI.StateMachine.States;
using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class MenuScreen : MonoCache
    {
        private WindowHud _windowHud;
        private Hero _hero;
        private ShopScreen _shopScreen;
        private LeaderboardScreen _leaderboardScreen;

        public void Inject(WindowHud windowHud, Hero hero, ShopScreen shopScreen, LeaderboardScreen leaderboardScreen)
        {
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
            print("тут пока что никого");
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
    }
}