using System;

namespace chava.utilities
{
    public static class InjectionHelpers
    {
        public static Type[] GetInterfaces<TBaseType>()
        {
            return typeof(TBaseType).GetInterfaces();
        }

        public static Type[] GetInterfaces(Type type)
        {
            return type.GetInterfaces();
        }
    }
}
