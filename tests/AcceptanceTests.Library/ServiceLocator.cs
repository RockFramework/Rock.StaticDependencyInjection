using System;
using System.Collections.Generic;

namespace Rock.StaticDependencyInjection.AcceptanceTests.Library
{
    public static class ServiceLocator
    {
        public const string MyName = "MyName";

        public const string ImportSingleIFoo = "ImportSingle<IFoo>";
        public const string ImportSingleIBarIBarFactory = "ImportSingle<IBar, IBarFactory>";
        public const string ImportSingleFooBase = "ImportSingle<FooBase>";
        public const string ImportSingleBarBaseBarFactoryBase = "ImportSingle<BarBase, BarFactoryBase>";

        public const string ImportSingleIFooNamed = "ImportSingle<IFoo>(MyName)";
        public const string ImportSingleIBarIBarFactoryNamed = "ImportSingle<IBar, IBarFactory>(MyName)";
        public const string ImportSingleFooBaseNamed = "ImportSingle<FooBase>(MyName)";
        public const string ImportSingleBarBaseBarFactoryBaseNamed = "ImportSingle<BarBase, BarFactoryBase>(MyName)";

        public const string ImportSingleIBaz = "ImportSingle<IBaz>";
        public const string ImportSingleIQuxIQuxFactory = "ImportSingle<IQux, IQuxFactory>";
        public const string ImportSingleBazBase = "ImportSingle<BazBase>";
        public const string ImportSingleQuxBaseQuxFactoryBase = "ImportSingle<QuxBase, QuxFactoryBase>";

        public const string ImportSingleIBazNamed = "ImportSingle<IBaz>(MyName)";
        public const string ImportSingleIQuxIQuxFactoryNamed = "ImportSingle<IQux, IQuxFactory>(MyName)";
        public const string ImportSingleBazBaseNamed = "ImportSingle<BazBase>(MyName)";
        public const string ImportSingleQuxBaseQuxFactoryBaseNamed = "ImportSingle<QuxBase, QuxFactoryBase>(MyName)";

        public const string ImportFirstIFoo = "ImportFirst<IFoo>";
        public const string ImportFirstIBarIBarFactory = "ImportFirst<IBar, IBarFactory>";
        public const string ImportFirstFooBase = "ImportFirst<FooBase>";
        public const string ImportFirstBarBaseBarFactoryBase = "ImportFirst<BarBase, BarFactoryBase>";

        public const string ImportFirstIFooNamed = "ImportFirst<IFoo>(MyName)";
        public const string ImportFirstIBarIBarFactoryNamed = "ImportFirst<IBar, IBarFactory>(MyName)";
        public const string ImportFirstFooBaseNamed = "ImportFirst<FooBase>(MyName)";
        public const string ImportFirstBarBaseBarFactoryBaseNamed = "ImportFirst<BarBase, BarFactoryBase>(MyName)";

        public const string ImportFirstIBaz = "ImportFirst<IBaz>";
        public const string ImportFirstIQuxIQuxFactory = "ImportFirst<IQux, IQuxFactory>";
        public const string ImportFirstBazBase = "ImportFirst<BazBase>";
        public const string ImportFirstQuxBaseQuxFactoryBase = "ImportFirst<QuxBase, QuxFactoryBase>";

        public const string ImportFirstIBazNamed = "ImportFirst<IBaz>(MyName)";
        public const string ImportFirstIQuxIQuxFactoryNamed = "ImportFirst<IQux, IQuxFactory>(MyName)";
        public const string ImportFirstBazBaseNamed = "ImportFirst<BazBase>(MyName)";
        public const string ImportFirstQuxBaseQuxFactoryBaseNamed = "ImportFirst<QuxBase, QuxFactoryBase>(MyName)";

        public const string ImportMultipleIFoo = "ImportMultiple<IFoo>";
        public const string ImportMultipleIBarIBarFactory = "ImportMultiple<IBar, IBarFactory>";
        public const string ImportMultipleFooBase = "ImportMultiple<FooBase>";
        public const string ImportMultipleBarBaseBarFactoryBase = "ImportMultiple<BarBase, BarFactoryBase>";

        public const string ImportMultipleIFooNamed = "ImportMultiple<IFoo>(MyName)";
        public const string ImportMultipleIBarIBarFactoryNamed = "ImportMultiple<IBar, IBarFactory>(MyName)";
        public const string ImportMultipleFooBaseNamed = "ImportMultiple<FooBase>(MyName)";
        public const string ImportMultipleBarBaseBarFactoryBaseNamed = "ImportMultiple<BarBase, BarFactoryBase>(MyName)";

        public const string ImportMultipleIBaz = "ImportMultiple<IBaz>";
        public const string ImportMultipleIQuxIQuxFactory = "ImportMultiple<IQux, IQuxFactory>";
        public const string ImportMultipleBazBase = "ImportMultiple<BazBase>";
        public const string ImportMultipleQuxBaseQuxFactoryBase = "ImportMultiple<QuxBase, QuxFactoryBase>";

        public const string ImportMultipleIBazNamed = "ImportMultiple<IBaz>(MyName)";
        public const string ImportMultipleIQuxIQuxFactoryNamed = "ImportMultiple<IQux, IQuxFactory>(MyName)";
        public const string ImportMultipleBazBaseNamed = "ImportMultiple<BazBase>(MyName)";
        public const string ImportMultipleQuxBaseQuxFactoryBaseNamed = "ImportMultiple<QuxBase, QuxFactoryBase>(MyName)";

        public const string AllowNonPublicClasses = "AllowNonPublicClasses";
        public const string ImportSingleIFooAllowNonPublicClasses = "ImportSingle<IFoo>(AllowNonPublicClasses)";
        public const string ImportSingleIBarIBarFactoryAllowNonPublicClasses = "ImportSingle<IBar, IBarFactory>(AllowNonPublicClasses)";
        public const string ImportFirstIFooAllowNonPublicClasses = "ImportFirst<IFoo>(AllowNonPublicClasses)";
        public const string ImportFirstIBarIBarFactoryAllowNonPublicClasses = "ImportFirst<IBar, IBarFactory>(AllowNonPublicClasses)";
        public const string ImportMultipleIFooAllowNonPublicClasses = "ImportMultiple<IFoo>(AllowNonPublicClasses)";
        public const string ImportMultipleIBarIBarFactoryAllowNonPublicClasses = "ImportMultiple<IBar, IBarFactory>(AllowNonPublicClasses)";

        public const string ImportSingleIFooIncludeNamedExportsFromUnnamedImports = "ImportSingle<IFoo>(IncludeNamedExportsFromUnnamedImports)";
        public const string ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports = "ImportSingle<IBar, IBarFactory>(IncludeNamedExportsFromUnnamedImports)";
        public const string ImportFirstIFooIncludeNamedExportsFromUnnamedImports = "ImportFirst<IFoo>(IncludeNamedExportsFromUnnamedImports)";
        public const string ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports = "ImportFirst<IBar, IBarFactory>(IncludeNamedExportsFromUnnamedImports)";
        public const string ImportMultipleIFooIncludeNamedExportsFromUnnamedImports = "ImportMultiple<IFoo>(IncludeNamedExportsFromUnnamedImports)";
        public const string ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports = "ImportMultiple<IBar, IBarFactory>(IncludeNamedExportsFromUnnamedImports)";

        public const string PreferTTargetType = "PreferTTargetType";
        public const string ImportSingleIBarIBarFactoryPreferTTargetType = "ImportSingle<IBar, IBarFactory>(PreferTTargetType)";
        public const string ImportFirstIBarIBarFactoryPreferTTargetType = "ImportFirst<IBar, IBarFactory>(PreferTTargetType)";
        public const string ImportMultipleIBarIBarFactoryPreferTTargetType = "ImportMultiple<IBar, IBarFactory>(PreferTTargetType)";

        public const string IncludeTypesFromThisAssembly = "IncludeTypesFromThisAssembly";
        public const string ImportSingleIFooIncludeTypesFromThisAssembly = "ImportSingle<IFoo>(IncludeTypesFromThisAssembly)";
        public const string ImportSingleIBarIBarFactoryIncludeTypesFromThisAssembly = "ImportSingle<IBar, IBarFactory>(InclSingleHighestPriorityudeTypesFromThisAssembly)";
        public const string ImportFirstIFooIncludeTypesFromThisAssembly = "ImportFirst<IFoo>(IncludeTypesFromThisAssembly)";
        public const string ImportFirstIBarIBarFactoryIncludeTypesFromThisAssembly = "ImportFirst<IBar, IBarFactory>(IncludeTypesFromThisAssembly)";
        public const string ImportMultipleIFooIncludeTypesFromThisAssembly = "ImportMultiple<IFoo>(IncludeTypesFromThisAssembly)";
        public const string ImportMultipleIBarIBarFactoryIncludeTypesFromThisAssembly = "ImportMultiple<IBar, IBarFactory>(IncludeTypesFromThisAssembly)";

        public const string ExportComparer = "ExportComparer";
        public const string ImportFirstIFooExportComparer = "ImportFirst<IFoo>(ExportComparer)";
        public const string ImportFirstIBarIBarFactoryExportComparer = "ImportFirst<IBar, IBarFactory>(ExportComparer)";
        public const string ImportMultipleIFooExportComparer = "ImportMultiple<IFoo>(ExportComparer)";
        public const string ImportMultipleIBarIBarFactoryExportComparer = "ImportMultiple<IBar, IBarFactory>(ExportComparer)";

        public const string SingleHighestPriority = "SingleHighestPriority";
        public const string ImportSingleIFooSingleHighestPriority = "ImportSingle<IFoo>(SingleHighestPriority)";
        public const string ImportSingleIBarIBarFactorySingleHighestPriority = "ImportSingle<IBar, IBarFactory>(SingleHighestPriority)";
        public const string ImportFirstIFooSingleHighestPriority = "ImportFirst<IFoo>(SingleHighestPriority)";
        public const string ImportFirstIBarIBarFactorySingleHighestPriority = "ImportFirst<IBar, IBarFactory>(SingleHighestPriority)";
        public const string ImportMultipleIFooSingleHighestPriority = "ImportMultiple<IFoo>(SingleHighestPriority)";
        public const string ImportMultipleIBarIBarFactorySingleHighestPriority = "ImportMultiple<IBar, IBarFactory>(SingleHighestPriority)";

        public const string MultipleHighestPriority = "MultipleHighestPriority";
        public const string ImportSingleIFooMultipleHighestPriority = "ImportSingle<IFoo>(MultipleHighestPriority)";
        public const string ImportSingleIBarIBarFactoryMultipleHighestPriority = "ImportSingle<IBar, IBarFactory>(MultipleHighestPriority)";
        public const string ImportFirstIFooMultipleHighestPriority = "ImportFirst<IFoo>(MultipleHighestPriority)";
        public const string ImportFirstIBarIBarFactoryMultipleHighestPriority = "ImportFirst<IBar, IBarFactory>(MultipleHighestPriority)";
        public const string ImportMultipleIFooMultipleHighestPriority = "ImportMultiple<IFoo>(MultipleHighestPriority)";
        public const string ImportMultipleIBarIBarFactoryMultipleHighestPriority = "ImportMultiple<IBar, IBarFactory>(MultipleHighestPriority)";

        public const string DuplicateExport = "DuplicateExport";
        public const string ImportSingleIFooDuplicateExport = "ImportSingle<IFoo>(DuplicateExport)";
        public const string ImportSingleIBarIBarFactoryDuplicateExport = "ImportSingle<IBar, IBarFactory>(DuplicateExport)";
        public const string ImportFirstIFooDuplicateExport = "ImportFirst<IFoo>(DuplicateExport)";
        public const string ImportFirstIBarIBarFactoryDuplicateExport = "ImportFirst<IBar, IBarFactory>(DuplicateExport)";
        public const string ImportMultipleIFooDuplicateExport = "ImportMultiple<IFoo>(DuplicateExport)";
        public const string ImportMultipleIBarIBarFactoryDuplicateExport = "ImportMultiple<IBar, IBarFactory>(DuplicateExport)";

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
