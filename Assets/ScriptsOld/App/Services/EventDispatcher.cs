using chava.domain;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace chava.app.Services
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, SignalDelegate> _events = null;

        public EventDispatcher()
        {
            _events = new Dictionary<Type, SignalDelegate>();
        }
        public void Subscribe<T>(SignalDelegate callback) where T : ISignal
        {
            var type = typeof(T);
            if (_events.ContainsKey(type))
            {
                _events[type] += callback;
                return;
            }

            _events.Add(type, callback);
        }

        public void Unsubscribe<T>(SignalDelegate callback) where T : ISignal //<-----
        {
            var type = typeof(T);
            if (_events.ContainsKey(type)) _events[type] -= callback;

        }

        public void Dispatch<T>(T signal) where T : ISignal
        {
            var type = typeof(T);
            if (!_events.ContainsKey(type)) return;

            _events[type]?.Invoke(signal);
        }
    }
}
