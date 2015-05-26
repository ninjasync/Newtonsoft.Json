using System;
using System.Diagnostics.CodeAnalysis;
using Java.Util.Concurrent;
using Newtonsoft.Json.Utilities;


namespace Newtonsoft.Json.Serialization
{
    // Dot42 doesn't support generic arguments an class(static) constructors 
    internal static class CachedAttributeGetter<T> where T : Attribute
    {
        [SuppressMessage("dot42", "StaticFieldInGenericType")]
        private static readonly object NULL = new object();

        [SuppressMessage("dot42", "StaticFieldInGenericType")]
        private static readonly ConcurrentHashMap<Tuple<Type,object>, object> TypeAttributeCache = new ConcurrentHashMap<Tuple<Type, object>, object>();

        public static T GetAttribute(object type)
        {
            var key = Tuple.Create(typeof (T), type);

            var val = TypeAttributeCache.Get(key);

            if (val == null)
            {
                val = JsonTypeReflector.GetAttribute<T>(type)  ?? NULL;
                val = TypeAttributeCache.PutIfAbsent(key, val) ?? val;
            }
            
            return (T)(val == NULL ? null : val);
        }
    }
}