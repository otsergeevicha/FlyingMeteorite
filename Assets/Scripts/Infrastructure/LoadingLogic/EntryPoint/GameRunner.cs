using System.Collections;
using Agava.YandexGames;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.LoadingLogic.EntryPoint
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

            if (PlayerAccount.IsAuthorized)
                PlayerAccount.GetCloudSaveData(OnSuccessCallback, OnErrorCallback);
            else
                StartGame();
            
#endif
            yield return new WaitForSeconds(0f);
            StartGame();
        }

        private void OnSuccessCallback(string data)
        {
            PlayerPrefs.SetString(Constants.Progress, data);
            PlayerPrefs.Save();
            
            StartGame();
        }

        private void OnErrorCallback(string _) => 
            StartGame();

        private void StartGame() =>
            Instantiate(_gameBootstrapper);
    }
}