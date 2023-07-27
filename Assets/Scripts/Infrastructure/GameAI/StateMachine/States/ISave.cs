using Services.ServiceLocator;

namespace Infrastructure.GameAI.StateMachine.States
{
    public interface ISave : IService
    {
        Progress AccessProgress();
        void Save();
    }
}