using chava.domain;
using System;

namespace chava.domain
{
    public class AppInitialized : ISignal
    {
        public string Status { get; }

        public AppInitialized(string status)
        {
            Status = status;
        }
    }
    //public record AppInitialized(string Status) : ISignal;

}
