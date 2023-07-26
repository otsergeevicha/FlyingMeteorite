using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class WindowHud : MonoCache
    {
        [SerializeField] private Heart[] _hearts;

        private int _countHeart;
        private Hero _hero;

        public void Inject(Hero hero)
        {
            _hero = hero;
            
            _hero.Collided += HeroOnCollided;

            Revival();
        }

        public void Revival()
        {
            for (int i = 0; i < _hearts.Length; i++)
                _hearts[i].gameObject.SetActive(true);

            _countHeart = _hearts.Length;
        }

        public void OnActive() => 
            gameObject.SetActive(true);

        public void InActive() => 
            gameObject.SetActive(false);

        private void HeroOnCollided()
        {
            _countHeart--;
            _hearts[_countHeart].gameObject.SetActive(false);

            if (_countHeart == 0) 
                _hero.Die();
        }
    }
}