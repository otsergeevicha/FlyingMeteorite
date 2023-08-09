using Agava.YandexGames;
using CanvasesLogic.Authorization;
using CanvasesLogic.ContentsFrames;
using CanvasesLogic.Menu;
using Plugins.MonoCache;
using Services.SaveLoad;
using UnityEngine;

namespace CanvasesLogic.LeaderboardModule
{
    public class LeaderboardScreen : MonoCache
    {
        [SerializeField] private ContentMember _contentMember;
        [SerializeField] private Transform _container;

        private ISave _save;
        private AuthorizationScreen _authorizationScreen;
        private MenuScreen _menuScreen;

        private ContentMember[] _members;

        public void Inject(ISave save, AuthorizationScreen authorizationScreen, MenuScreen menuScreen)
        {
            _menuScreen = menuScreen;
            _authorizationScreen = authorizationScreen;
            _save = save;

            _members = new ContentMember[Constants.TopPlayersCount];

            for (int i = 0; i < _members.Length; i++)
                _members[i] = Instantiate(_contentMember, _container);
        }

        public void OnActive()
        {
#if !UNITY_WEBGL || !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
            {
                Leaderboard.SetScore(Constants.Leaderboard, _save.AccessProgress().DataWallet.Read());
                GetLeaderboardEntries();
                return;
            }

            if (!PlayerAccount.IsAuthorized)
                _authorizationScreen.OnActive();

#endif
            gameObject.SetActive(true);
        }

        public void CloseWindow()
        {
            _menuScreen.OnActive();
            InActive();
        }

        public void GetLeaderboardEntries()
        {
            Agava.YandexGames.Leaderboard.GetEntries(Constants.Leaderboard, (result) =>
            {
                for (int i = 0; i < result.entries.Length; i++)
                    _members[i].InitData(result.entries[i].rank, NameCorrector(result.entries[i].player.publicName),
                        result.entries[i].score);
            }, null, Constants.TopPlayersCount, Constants.CompletingPlayersCount);

            gameObject.SetActive(true);
        }

        public void InActive() =>
            gameObject.SetActive(false);

        private string NameCorrector(string nameMember)
        {
            if (string.IsNullOrEmpty(nameMember))
                nameMember = Constants.Anonymous;

            return nameMember;
        }
    }
}