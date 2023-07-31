using CanvasesLogic.Shop;
using Plugins.MonoCache;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasesLogic.ContentsFrames
{
    public class ContentView : MonoCache
    {
        [SerializeField] private Image _onActiveIcon;
        [SerializeField] private Image _inActiveIcon;
        [SerializeField] private Image _iconSelected;
        [SerializeField] private Scrollbar _scrollbarProgress;

        private int _currentIndexCharacter;
        private bool _isActive;
        private ISave _save;
        private ShopScreen _shopScreen;

        public void Inject(Sprite activeIcon, int indexCharacter, ISave save)
        {
            _save = save;
            _onActiveIcon.sprite = activeIcon;
            _currentIndexCharacter = indexCharacter;

            _shopScreen = ParentGet<ShopScreen>();
        }

        public void SetData(bool isActiveIcon, float valueProgress)
        {
            _isActive = isActiveIcon;

            _onActiveIcon.gameObject.SetActive(isActiveIcon);
            _inActiveIcon.gameObject.SetActive(!isActiveIcon);
            _scrollbarProgress.size = valueProgress;
        }

        public void SelectCharacter()
        {
            if (_isActive)
            {
                _save.AccessProgress().DataCurrentCharacter.Record(_currentIndexCharacter);
                _save.Save();
                _shopScreen.UpdateCharacterIcon(_currentIndexCharacter);
                _shopScreen.UpdateSelected(_currentIndexCharacter);
            }
        }

        public void OnSelected() => 
            _iconSelected.gameObject.SetActive(true);
        
        public void OffSelected() => 
            _iconSelected.gameObject.SetActive(false);
    }
}