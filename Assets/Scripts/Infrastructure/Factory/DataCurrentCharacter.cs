using System;

namespace Infrastructure.Factory
{
    [Serializable]
    public class DataCurrentCharacter
    {
        public int IndexCharacter = 0;

        public int Read() =>
            IndexCharacter;

        public void Record(int indexCharacter) =>
            IndexCharacter = indexCharacter;
    }
}