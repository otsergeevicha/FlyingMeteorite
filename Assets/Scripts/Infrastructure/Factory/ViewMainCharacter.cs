﻿using Plugins.MonoCache;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factory
{
    public class ViewMainCharacter : MonoCache
    {
        [SerializeField] private Image _iconCharacter;
        private ShopScreen _shopScreen;

        public void Inject(ShopScreen shopScreen)
        {
            _shopScreen = shopScreen;
            UpdateMainIcon();
        }

        public void UpdateMainIcon() => 
            _iconCharacter.sprite = _shopScreen.GetCurrentIcon();
    }
}