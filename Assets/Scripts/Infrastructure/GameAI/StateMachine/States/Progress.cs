using System;
using Infrastructure.Factory;

namespace Infrastructure.GameAI.StateMachine.States
{
    [Serializable]
    public class Progress
    {
        public DataWallet DataWallet;
        public DataCurrentCharacter DataCurrentCharacter;

        public Progress()
        {
            DataWallet = new DataWallet();
            DataCurrentCharacter = new DataCurrentCharacter();
        }
    }
}