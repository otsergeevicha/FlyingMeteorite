using System;

namespace Services.Inputs
{
    public class InputService : IInputService
    {
        private readonly MapInputs _input = new ();
        
        public void Tap(Action onMove) =>
            _input.Player.Tap.performed += _ =>
                onMove?.Invoke();

        public void OnControls() =>
            _input.Player.Enable();
    }
}