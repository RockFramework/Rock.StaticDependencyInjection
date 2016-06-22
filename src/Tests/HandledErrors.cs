using System;
using System.Collections.Generic;

namespace Rock.StaticDependencyInjection.Tests
{
    public static class HandledErrors
    {
        private static readonly Dictionary<Tuple<string, Type, Type>, Tuple<string, Exception>> _handledErrors =
            new Dictionary<Tuple<string, Type, Type>, Tuple<string, Exception>>(); 

        internal static void Add(string message, Exception exception, ImportInfo import)
        {
            _handledErrors.Add(Tuple.Create(import.Name, import.TargetType, import.FactoryType),
                Tuple.Create(message, exception));
        }

        public static Tuple<string, Exception> Find(string importName, Type targetType, Type factoryType)
        {
            Tuple<string, Exception> handledError;

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                targetType = targetType.GetGenericArguments()[0];
            }

            if (_handledErrors.TryGetValue(Tuple.Create(importName, targetType, factoryType), out handledError))
            {
                return handledError;
            }

            return null;
        }
    }
}