using Cysharp.Threading.Tasks;
using Plugins.MonoCache;
using TMPro;
using UnityEngine;

namespace CanvasesLogic.Hud
{
    public class LevelTextHolder : MonoCache
    {
        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void OnActive(int currentLevel)
        {
            _levelNumber.text = currentLevel.ToString();
            
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
            
            _ = FadeIn();
        }

        private async UniTaskVoid FadeIn()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= .025f;
                await UniTask.Delay(40);
            }

            gameObject.SetActive(false);
        }
    }
}