using Infrastructure.GameAI.StateMachine.States;
using PlayerLogic;
using Plugins.MonoCache;
using Services.ServiceLocator;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class ShopScreen : MonoCache
    {
        [SerializeField] private Sprite[] _characters;
        [SerializeField] private ContentView _content;
        [SerializeField] private Transform _container;

        private IWallet _wallet;
        private Hero _hero;

        private ContentView[] _contentViews;
        private ISave _save;
        private MenuScreen _menuScreen;

        public void Inject(IWallet wallet, Hero hero, ISave save, MenuScreen menuScreen)
        {
            _menuScreen = menuScreen;
            _save = save;
            _hero = hero;
            _wallet = wallet;
            
            ContentView contentView;
            
            _contentViews = new ContentView[_characters.Length];
            
            for (int i = 0; i < _characters.Length; i++)
            {
                contentView = Instantiate(_content, _container);
                contentView.Inject(_characters[i], i, save);

                _contentViews[i] = contentView;
            }
            
            hero.ChangeHeroIcon(_characters[save.AccessProgress().DataCurrentCharacter.Read()]);
        }

        public Sprite GetCurrentIcon() =>
            _characters[_save.AccessProgress().DataCurrentCharacter.Read()];

        public void CloseStore()
        {
            _menuScreen.OnActive();
            InActive();
        }
        
        public void OnActive()
        {
            UpdateShop();
            gameObject.SetActive(true);
        }

        public void InActive() => 
            gameObject.SetActive(false);

        public void UpdateCharacterIcon(int indexCharacter) => 
            _hero.ChangeHeroIcon(_characters[indexCharacter]);

        public void UpdateSelected(int indexCharacter)
        {
            for (int i = 0; i < _contentViews.Length; i++) 
                _contentViews[i].OffSelected();
            
            _contentViews[indexCharacter].OnSelected();
        }
        
        private void UpdateShop()
        {
            int savedScore = GetCurrentScore();
            int currentLevel;
            float currentValue;
            
            for (int i = 0; i < _contentViews.Length; i++)
            {
                currentLevel = GetCurrentLevel(i);
                currentValue = GetCurrentProgress(savedScore, currentLevel);
                
                _contentViews[i].SetData(currentLevel < savedScore, currentValue);
                _contentViews[i].OffSelected();
            }
            
            _contentViews[0].SetData(true, 1);
            _contentViews[_save.AccessProgress().DataCurrentCharacter.Read()].OnSelected();
        }

        private int GetCurrentScore() => 
            ServiceLocator.Container.Single<ISave>().AccessProgress().DataWallet.Read();

        private float GetCurrentProgress(int savedScore, int currentLevel) => 
            (float)savedScore / currentLevel;

        private int GetCurrentLevel(int indexCurrentIcon) => 
            (indexCurrentIcon + 1) * Constants.MultiplierValueLevel;
    }
}