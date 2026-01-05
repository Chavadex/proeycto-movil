using chava.app.Server;
using System;

namespace chava.app.Server.DTO
{
    [Serializable]
    public class User : IDataTransferObject
    {
        public bool IsNewUser;

        public User(bool isNewUser)
        {
            IsNewUser = isNewUser;
        }
    }
}
