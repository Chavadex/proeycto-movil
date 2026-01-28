using chava.domain;
using UnityEngine;

namespace chava.app.Services
{
    public class UnitySerializer : ISerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}
