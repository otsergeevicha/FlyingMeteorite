using SaveLoadLogic.Base;
using Services.ServiceLocator;

namespace Services.SaveLoad
{
    public interface ISave : IService
    {
        Progress AccessProgress();
        void Save();
    }
}