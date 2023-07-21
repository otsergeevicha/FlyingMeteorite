using System;
using Services.ServiceLocator;

namespace Services.Inputs
{
    public interface IInputService : IService
    {
        void Tap(Action onMove);
        void OnControls();
    }
}