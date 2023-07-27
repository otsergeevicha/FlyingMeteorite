using System;

namespace Infrastructure.GameAI.StateMachine.States
{
    [Serializable]
    public class DataWallet
    {
        public int Score = 0;

        public int Read() =>
            Score;

        public void Record(int amountScore) =>
            Score = amountScore;
    }
}