using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace CameraLogic
{
    public class HeroTracker : MonoCache
    {
        private readonly float _xOffset = -1f;
        private Hero _hero;

        public void Construct(Hero hero) => 
            _hero = hero;

        protected override void UpdateCached() =>
            transform.position = new Vector3(_hero.transform.position.x - _xOffset, _hero.transform.position.y,
                _hero.transform.position.z);
    }
}