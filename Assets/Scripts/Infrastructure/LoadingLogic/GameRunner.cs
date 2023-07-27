using System.Collections;
using Agava.YandexGames;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.LoadingLogic
{
    public class GameRunner : MonoCache
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        private void Awake()
        {
            var bootstrapper = Find<GameBootstrapper>();

            if (bootstrapper != null)
                return;

            StartCoroutine(LaunchSDK());
        }

        private IEnumerator LaunchSDK()
        {
#if !UNITY_WEBGL || !UNITY_EDITOR

            while (!YandexGamesSdk.IsInitialized)
                yield return YandexGamesSdk.Initialize();

            StartGame();
#endif
            yield return new WaitForSeconds(0f);
            StartGame();
        }

        private void StartGame() =>
            Instantiate(_gameBootstrapper);
    }
}