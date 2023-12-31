﻿using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic.Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HeroCollisionHandler))]
    public class HeroMovement : MonoCache
    {
        private Rigidbody2D _rigidbody;
        private IInputService _input;

        private float _speed;
        private float _tapForce;
        private int _counter;
        private int _progressIndex = 5;
        private Quaternion _maxRotation;
        private Quaternion _minRotation;

        private void Awake()
        {
            _rigidbody = Get<Rigidbody2D>();
            _input = ServiceLocator.Container.Single<IInputService>();

            _speed = Constants.SpeedHero;
            _tapForce = Constants.TapForce;
            _rigidbody.velocity = Vector2.zero;

            transform.position = Vector3.zero;
            
            _maxRotation = Quaternion.Euler(0, 0, Constants.MaxRotationZ);
            _minRotation = Quaternion.Euler(0, 0, Constants.MinRotationZ);
            
            _input.Tap(OnUp);
        }

        protected override void UpdateCached()
        {
            if (_rigidbody.drag <= float.Epsilon)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation,
                    Constants.RotationSpeed * Time.deltaTime);
            }
        }

        private void OnUp()
        {
            _rigidbody.velocity = new Vector2(_speed, 0);
            transform.rotation = _maxRotation;
            _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode2D.Force);
        }

        public void ResetHero()
        {
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.Euler(0,0,0);
            _rigidbody.inertia = 0;
            _rigidbody.rotation = 0;
            
            _speed = Constants.SpeedHero;
        }

        public void IncreaseSpeed(int score)
        {
            _counter++;
            
            if (_counter < _progressIndex)
                return;
            
            _counter = 0;
            _progressIndex++;
            _speed++;
        }

        public IInputService GetInputService() => 
            _input;
    }
}