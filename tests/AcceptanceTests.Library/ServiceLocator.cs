using System;
using System.Collections.Generic;

namespace Rock.StaticDependencyInjection.AcceptanceTests.Library
{
    public static class ServiceLocator
    {
        public const string ImportSingleIFoo = "ImportSingle<IFoo>";
        public const string ImportSingleIBarIBarFactory = "ImportSingle<IBar, IBarFactory>";
        public const string ImportSingleFooBase = "ImportSingle<FooBase>";
        public const string ImportSingleBarBaseBarFactoryBase = "ImportSingle<BarBase, BarFactoryBase>";

        public const string ImportSingleIBaz = "ImportSingle<IBaz>";
        public const string ImportSingleIQuxIQuxFactory = "ImportSingle<IQux, IQuxFactory>";
        public const string ImportSingleBazBase = "ImportSingle<BazBase>";
        public const string ImportSingleQuxBaseQuxFactoryBase = "ImportSingle<QuxBase, QuxFactoryBase>";

        public const string ImportFirstIFoo = "ImportFirst<IFoo>";
        public const string ImportFirstIBarIBarFactory = "ImportFirst<IBar, IBarFactory>";
        public const string ImportFirstFooBase = "ImportFirst<FooBase>";
        public const string ImportFirstBarBaseBarFactoryBase = "ImportFirst<BarBase, BarFactoryBase>";

        public const string ImportFirstIBaz = "ImportFirst<IBaz>";
        public const string ImportFirstIQuxIQuxFactory = "ImportFirst<IQux, IQuxFactory>";
        public const string ImportFirstBazBase = "ImportFirst<BazBase>";
        public const string ImportFirstQuxBaseQuxFactoryBase = "ImportFirst<QuxBase, QuxFactoryBase>";

        public const string ImportMultipleIFoo = "ImportMultiple<IFoo>";
        public const string ImportMultipleIBarIBarFactory = "ImportMultiple<IBar, IBarFactory>";
        public const string ImportMultipleFooBase = "ImportMultiple<FooBase>";
        public const string ImportMultipleBarBaseBarFactoryBase = "ImportMultiple<BarBase, BarFactoryBase>";

        public const string ImportMultipleIBaz = "ImportMultiple<IBaz>";
        public const string ImportMultipleIQuxIQuxFactory = "ImportMultiple<IQux, IQuxFactory>";
        public const string ImportMultipleBazBase = "ImportMultiple<BazBase>";
        public const string ImportMultipleQuxBaseQuxFactoryBase = "ImportMultiple<QuxBase, QuxFactoryBase>";

        private static readonly Dictionary<Tuple<Type, string>, object> _registeredInstances = new Dictionary<Tuple<Type, string>, object>(); 

        public static void Register<T>(T instance, string name)
            where T : class
        {
            if (instance == null)
            {
                return;
            }

            var key = Tuple.Create(typeof(T), name);

            _registeredInstances[key] = instance;
        }

        public static object Get(Type type, string name)
        {
            object instance;

            var key = Tuple.Create(type, name);

            if (_registeredInstances.TryGetValue(key, out instance))
            {
                return instance;
            }

            return null;
        }
    }
}
