using chava.domain;
using chava.app.Server.DTO;
using System;
using System.Collections.Generic;

namespace chava.app.Server
{
    public class SaveFileGateway : LocalGateway
    {
        public SaveFileGateway(ISerializer serializer) : base(serializer)
        {
        }

        protected override void InitializeTypeToKey(out Dictionary<Type, string> typeToKey)
        {
            typeToKey = new Dictionary<Type, string>
            {
                {typeof(User), "UserData"}
                //{typeof(LeaderBoardData), "LeaderBoardData"}
            };
        }

    }
}
