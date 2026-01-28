namespace chava.domain
{
    public interface IEventDispatcher
    {
        void Subscribe<T>(SignalDelegate callback) where T : ISignal;
        void Unsubscribe<T>(SignalDelegate callback) where T : ISignal;
        void Dispatch<T>(T signal) where T : ISignal;
    }
}
