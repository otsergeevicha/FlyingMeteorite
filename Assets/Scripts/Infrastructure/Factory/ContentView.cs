using Plugins.MonoCache;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factory
{
    public class ContentView : MonoCache
    {
        [SerializeField] private Image _onActiveIcon;
        [SerializeField] private Image _inActiveIcon;
        [SerializeField] private Scrollbar _scrollbarProgress;

        public void InjectIcon(Sprite activeIcon) => 
            _onActiveIcon.sprite = activeIcon;

        public void SetData(bool isActiveIcon, float valueProgress)
        {
            _onActiveIcon.gameObject.SetActive(isActiveIcon);
            _inActiveIcon.gameObject.SetActive(!isActiveIcon);
            _scrollbarProgress.size = valueProgress;
        }
    }
}