using Agava.YandexGames;
using Lean.Localization;
using Plugins.MonoCache;

namespace Infrastructure.LoadingLogic
{
    public class LanguageModule : MonoCache
    {
        private const string RussianLanguage = "Russian";
        private const string EnglishLanguage = "English";

        private string _currentLanguage;

        private void Start() =>
            SetLanguage();

        private void SetLanguage()
        {
#if !UNITY_WEBGL || !UNITY_EDITOR
            switch (YandexGamesSdk.Environment.i18n.lang)
            {
                case "en":
                    LeanLocalization.SetCurrentLanguageAll(EnglishLanguage);
                    break;
                case "ru":
                    LeanLocalization.SetCurrentLanguageAll(RussianLanguage);
                    break;
                default:
                    LeanLocalization.SetCurrentLanguageAll(EnglishLanguage);
                    break;
            }
#endif
        }
    }
}