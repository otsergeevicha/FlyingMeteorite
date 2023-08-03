using CanvasesLogic.ContentsFrames;
using CanvasesLogic.GameOver;
using CanvasesLogic.Shop;
using PlayerLogic;
using Plugins.MonoCache;
using TMPro;
using UnityEngine;

namespace CanvasesLogic.Hud
{
    public class WindowHud : MonoCache
    {
        [SerializeField] private Heart[] _hearts;
        [SerializeField] private TMP_Text _currentScore;

        [SerializeField] private LevelTextHolder _textHolder;

        private int _countHeart;
        private Hero _hero;
        private ShopScreen _shopScreen;
        private int _currentLevel = 1;

        public void Inject(Hero hero)
        {
            _hero = hero;

            _hero.Collided += HeroOnCollided;
            _hero.ScoreChanged += RenderScore;

            Revival();
        }

        public void Revival()
        {
            for (int i = 0; i < _hearts.Length; i++)
                _hearts[i].gameObject.SetActive(true);

            _countHeart = _hearts.Length;
        }

        public void OnActive()
        {
            gameObject.SetActive(true);
            _textHolder.OnActive(1);
        }

        public void InActive() =>
            gameObject.SetActive(false);

        private void HeroOnCollided()
        {
            _countHeart--;
            _hearts[_countHeart].gameObject.SetActive(false);

            if (_countHeart == 0)
            {
                _hero.Die();
                _currentLevel = 1;
            }
        }

        public void RenderScore()
        {
            _currentScore.text = _hero.CurrentScore.ToString();

            if (_hero.CurrentScore > Constants.MultiplierValueLevel * _currentLevel)
            {
                _currentLevel++;
                _textHolder.OnActive(_currentLevel);
            }
        }

        public void ResetScore() => 
            _currentScore.text = "0";
    }
}