using System;
using Services.ServiceLocator;

namespace Infrastructure.GameAI.StateMachine.States
{
    public interface IWallet : IService
    {
        event Action<int> Changed;
        
        bool Check(int pricePurchase);

        void Spend(int price);

        void Apply(int amountReplenishment);

    }
}