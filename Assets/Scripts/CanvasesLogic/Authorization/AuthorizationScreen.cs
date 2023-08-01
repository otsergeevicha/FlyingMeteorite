using Agava.YandexGames;
using CanvasesLogic.LeaderboardModule;
using CanvasesLogic.Menu;
using Plugins.MonoCache;

namespace CanvasesLogic.Authorization
{
    public class AuthorizationScreen : MonoCache
    {
        private MenuScreen _menuScreen;
        private LeaderboardScreen _leaderboardScreen;

        public void Inject(MenuScreen menuScreen, LeaderboardScreen leaderboardScreen)
        {
            _leaderboardScreen = leaderboardScreen;
            _menuScreen = menuScreen;
        }

        public void Yes() =>
            PlayerAccount.Authorize(OnSuccessCallback, OnErrorCallback);

        public void No() =>
            CloseWindow();

        public void OnActive() =>
            gameObject.SetActive(true);

        public void InActive() =>
            gameObject.SetActive(false);

        private void OnSuccessCallback()
        {
            _leaderboardScreen.OnActive();
            InActive();
        }

        private void OnErrorCallback(string obj) =>
            CloseWindow();

        private void CloseWindow()
        {
            _menuScreen.OnActive();
            InActive();
        }
    }
}