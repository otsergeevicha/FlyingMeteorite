using Infrastructure.GameAI.StateMachine.States;
using PlayerLogic;
using Plugins.MonoCache;
using Services.Factory;
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

        public void Inject(IWallet wallet, Hero hero)
        {
            _hero = hero;
            _wallet = wallet;
            
            ContentView contentView;
            
            int savedScore = ServiceLocator.Container.Single<ISave>().AccessProgress().DataWallet.Read();
            int currentLevel;
            float currentValue;
            
            
            for (int i = 0; i < _characters.Length; i++)
            {
                contentView = Instantiate(_content, _container);
                contentView.InjectIcon(_characters[i]);

                currentLevel = GetCurrentLevel(i);
                currentValue = GetCurrentProgress(savedScore, currentLevel);
                
                contentView.SetData(currentLevel > savedScore, currentValue);
            }
        }

        public void OnActive() => 
            gameObject.SetActive(true);

        public void InActive() => 
            gameObject.SetActive(false);

        private float GetCurrentProgress(int savedScore, int currentLevel) => 
            (float)savedScore / currentLevel;

        private int GetCurrentLevel(int indexCurrentIcon) => 
            (indexCurrentIcon + 1) * Constants.MultiplierValueLevel;
    }
}