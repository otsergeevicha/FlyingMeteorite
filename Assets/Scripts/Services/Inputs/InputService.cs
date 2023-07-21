using System;

namespace Services.Inputs
{
    public class InputService : IInputService
    {
        private readonly MapInputs _input = new ();
        public void Tap(Action onMove) =>
            _input.Hero.Move.performed += _ =>
                onMove?.Invoke();
    }
}