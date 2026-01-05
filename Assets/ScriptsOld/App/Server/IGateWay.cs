using chava.app.Server;
using Cysharp.Threading.Tasks;

namespace chava.app.Server
{
    public interface IGateWay
    {
        T Get<T>() where T : IDataTransferObject;
        bool Contains<T>() where T : IDataTransferObject;
        void Set<T>(T data) where T : IDataTransferObject;
        UniTask Save();
    }
}
