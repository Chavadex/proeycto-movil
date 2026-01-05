using chava.app.Server;
using chava.domain;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace chava.app.Server
{
    public abstract class LocalGateway : IGateWay, IPreloadObject
    {
        private Dictionary<Type, string> _typeToKey = null;
        private Dictionary<string, string> _rawData = null;
        private Dictionary<Type, IDataTransferObject> _dataTransferObjects = null;
        private Dictionary<string, string> _dirtyData = null;
        private readonly ISerializer _serializer = null;
        private bool _isDirty = false;

        public LocalGateway(ISerializer serializer)
        {
            _serializer = serializer;
            _dataTransferObjects = new Dictionary<Type, IDataTransferObject>();
            _dirtyData = new Dictionary<string, string>();
        }

        protected abstract void InitializeTypeToKey(out Dictionary<Type, string> typeToKey);
        public UniTask Preload()
        {
            InitializeTypeToKey(out _typeToKey);
            _rawData = new Dictionary<string, string>();
            foreach (var pair in _typeToKey)
            {
                var key = pair.Value;
                if (PlayerPrefs.HasKey(key))
                {
                    _rawData.Add(key, PlayerPrefs.GetString(key));
                }
            }
            return UniTask.CompletedTask;
        }
        public T Get<T>() where T : IDataTransferObject
        {
            var type = typeof(T);
            if (_dataTransferObjects.ContainsKey(type))
                return (T)_dataTransferObjects[type];

            var key = _typeToKey[type];
            if (_rawData.ContainsKey(key))
            {
                var data = _serializer.Deserialize<T>(_rawData[key]);
                _dataTransferObjects.Add(type, data);
                return data;
            }

            var newData = Activator.CreateInstance<T>();
            _dataTransferObjects.Add(type, newData);
            return newData;
        }

        public bool Contains<T>() where T : IDataTransferObject
        {
            var type = typeof(T);
            if (_dataTransferObjects.ContainsKey(type))
                return true;
            var key = _typeToKey[type];
            if (_rawData.ContainsKey(key))
            {
                var data = _serializer.Deserialize<T>(_rawData[key]);
                _dataTransferObjects.Add(type, data);
                return true;
            }
            return false;
        }

        public void Set<T>(T data) where T : IDataTransferObject
        {
            var type = typeof(T);
            var key = _typeToKey[type];
            var json = _serializer.Serialize(data);

            if (_dirtyData.ContainsKey(key))
            {
                _dirtyData[key] = json;
                DataTransferObjectsAdd(data, type);
                //_isDirty = true;
                return;
            }

            Debug.Log($"key {key} json {json}");
            _dirtyData.Add(key, json);
            // _isDirty = true;
            DataTransferObjectsAdd(data, type);
        }

        private void DataTransferObjectsAdd<T>(T data, Type type) where T : IDataTransferObject
        {
            if (_dataTransferObjects.ContainsKey(type))
            {
                _dataTransferObjects[type] = data;
            }
            else
            {
                _dataTransferObjects.Add(type, data);
            }
        }

        public UniTask Save()
        {
            // if (!_isDirty) return UniTask.CompletedTask;
            foreach (var data in _dirtyData)
            {
                PlayerPrefs.SetString(data.Key, data.Value);
            }
            _dirtyData.Clear();
            // _isDirty = false;
            //return await peticionalserver;
            return UniTask.CompletedTask;
        }


    }
}
