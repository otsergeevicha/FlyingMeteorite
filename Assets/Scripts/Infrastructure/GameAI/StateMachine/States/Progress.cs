using System;

namespace Infrastructure.GameAI.StateMachine.States
{
    [Serializable]
    public class Progress
    {
        public DataWallet DataWallet;

        public Progress() => 
            DataWallet = new DataWallet();
    }
}