using System;

namespace Services.Inputs
{
    public class InputService : IInputService
    {
        private readonly MapInputs _input = new ();
        
        public void Tap(Action onUp) =>
            _input.Player.Tap.performed += _ =>
                onUp?.Invoke();

        public void OnControls() =>
            _input.Player.Enable();

        public void OffControls() =>
            _input.Player.Disable();
    }
}