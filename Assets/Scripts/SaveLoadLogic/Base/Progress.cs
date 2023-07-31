using System;

namespace SaveLoadLogic.Base
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