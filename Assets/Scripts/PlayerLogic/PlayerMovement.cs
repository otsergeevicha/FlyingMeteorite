using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoCache
    {
        private Rigidbody2D _rigidbody;
        private IInputService _input;
        
        private Vector3 _startPosition;
        private float _speed;
        private float _tapForce;

        protected override void OnEnabled()
        {
            _rigidbody = Get<Rigidbody2D>();
            _input = ServiceLocator.Container.Single<IInputService>();

            _speed = Constants.SpeedHero;
            _tapForce = Constants.TapForce;
            _rigidbody.velocity = Vector2.zero;
            
            _input.Tap(OnMove);
        }

        private void OnMove()
        {
            _rigidbody.velocity = new Vector2(_speed, 0);
            _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode2D.Force);
        }
    }
}