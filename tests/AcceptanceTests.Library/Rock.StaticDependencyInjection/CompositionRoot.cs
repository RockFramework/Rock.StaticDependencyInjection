using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rock.StaticDependencyInjection.AcceptanceTests.Library.Rock.StaticDependencyInjection
{
    internal partial class CompositionRoot
    {
        public override void Bootstrap()
        {
            ImportSingleTests_GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed();
            ImportSingleTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed();
            ImportSingleTests_GivenMultipleImplementationsForTheAbstraction_ThenNoImplementationIsUsed();
            ImportSingleTests_GivenMultipleNamedImplementationsForTheAbstraction_ThenNoImplementationIsUsed();
            ImportFirstTests_GivenASingleImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed();
            ImportFirstTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed();
            ImportFirstTests_GivenMultipleImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed();
            ImportFirstTests_GivenMultipleNamedImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed();
            ImportMultipleTests_GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed();
            ImportMultipleTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed();
            ImportMultipleTests_GivenMultipleImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically();
            ImportMultipleTests_GivenMultipleNamedImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically();

            ImportOptionsParameterTests_HandlesAllowNonPublicClasses();
            ImportOptionsParameterTests_HandlesIncludeNamedExportsFromUnnamedImports();
            ImportOptionsParameterTests_HandlesPreferTTargetType();
            ImportOptionsParameterTests_HandlesIncludeTypesFromThisAssembly();
            ImportOptionsParameterTests_HandlesExportComparer();

            PriorityTests_SingleHighestPriority();
            PriorityTests_MultipleHighestPriority();

            DuplicateExportTests();
        }

        private void ImportSingleTests_GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFoo));

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactory),
                factory => factory.GetBar());

            ImportSingle<FooBase>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleFooBase));

            ImportSingle<BarBase, BarFactoryBase>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleBarBaseBarFactoryBase),
                factory => factory.GetBar());
        }

        private void ImportSingleTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFooNamed),
                ServiceLocator.MyName);

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactoryNamed),
                factory => factory.GetBar(),
                ServiceLocator.MyName);

            ImportSingle<FooBase>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleFooBaseNamed),
                ServiceLocator.MyName);

            ImportSingle<BarBase, BarFactoryBase>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleBarBaseBarFactoryBaseNamed),
                factory => factory.GetBar(),
                ServiceLocator.MyName);
        }

        private void ImportSingleTests_GivenMultipleImplementationsForTheAbstraction_ThenNoImplementationIsUsed()
        {
            ImportSingle<IBaz>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportSingleIBaz));

            ImportSingle<IQux, IQuxFactory>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportSingleIQuxIQuxFactory),
                factory => factory.GetQux());

            ImportSingle<BazBase>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportSingleBazBase));

            ImportSingle<QuxBase, QuxFactoryBase>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportSingleQuxBaseQuxFactoryBase),
                factory => factory.GetQux());
        }

        private void ImportSingleTests_GivenMultipleNamedImplementationsForTheAbstraction_ThenNoImplementationIsUsed()
        {
            ImportSingle<IBaz>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportSingleIBazNamed),
                ServiceLocator.MyName);

            ImportSingle<IQux, IQuxFactory>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportSingleIQuxIQuxFactoryNamed),
                factory => factory.GetQux(),
                ServiceLocator.MyName);

            ImportSingle<BazBase>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportSingleBazBaseNamed),
                ServiceLocator.MyName);

            ImportSingle<QuxBase, QuxFactoryBase>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportSingleQuxBaseQuxFactoryBaseNamed),
                factory => factory.GetQux(),
                ServiceLocator.MyName);
        }

        private void ImportFirstTests_GivenASingleImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed()
        {
            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFoo));

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactory),
                factory => factory.GetBar());

            ImportFirst<FooBase>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstFooBase));

            ImportFirst<BarBase, BarFactoryBase>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstBarBaseBarFactoryBase),
                factory => factory.GetBar());
        }

        private void ImportFirstTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed()
        {
            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooNamed),
                ServiceLocator.MyName);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryNamed),
                factory => factory.GetBar(),
                ServiceLocator.MyName);

            ImportFirst<FooBase>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstFooBaseNamed),
                ServiceLocator.MyName);

            ImportFirst<BarBase, BarFactoryBase>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstBarBaseBarFactoryBaseNamed),
                factory => factory.GetBar(),
                ServiceLocator.MyName);
        }

        private void ImportFirstTests_GivenMultipleImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed()
        {
            ImportFirst<IBaz>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportFirstIBaz));

            ImportFirst<IQux, IQuxFactory>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportFirstIQuxIQuxFactory),
                factory => factory.GetQux());

            ImportFirst<BazBase>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportFirstBazBase));

            ImportFirst<QuxBase, QuxFactoryBase>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportFirstQuxBaseQuxFactoryBase),
                factory => factory.GetQux());
        }

        private void ImportFirstTests_GivenMultipleNamedImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed()
        {
            ImportFirst<IBaz>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportFirstIBazNamed),
                ServiceLocator.MyName);

            ImportFirst<IQux, IQuxFactory>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportFirstIQuxIQuxFactoryNamed),
                factory => factory.GetQux(),
                ServiceLocator.MyName);

            ImportFirst<BazBase>(
                baz => ServiceLocator.Register(baz, ServiceLocator.ImportFirstBazBaseNamed),
                ServiceLocator.MyName);

            ImportFirst<QuxBase, QuxFactoryBase>(
                qux => ServiceLocator.Register(qux, ServiceLocator.ImportFirstQuxBaseQuxFactoryBaseNamed),
                factory => factory.GetQux(),
                ServiceLocator.MyName);
        }

        private void ImportMultipleTests_GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFoo));

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactory),
                factory => factory.GetBar());

            ImportMultiple<FooBase>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleFooBase));

            ImportMultiple<BarBase, BarFactoryBase>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleBarBaseBarFactoryBase),
                factory => factory.GetBar());
        }

        private void ImportMultipleTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooNamed),
                ServiceLocator.MyName);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryNamed),
                factory => factory.GetBar(),
                ServiceLocator.MyName);

            ImportMultiple<FooBase>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleFooBaseNamed),
                ServiceLocator.MyName);

            ImportMultiple<BarBase, BarFactoryBase>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleBarBaseBarFactoryBaseNamed),
                factory => factory.GetBar(),
                ServiceLocator.MyName);
        }

        private void ImportMultipleTests_GivenMultipleImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically()
        {
            ImportMultiple<IBaz>(
                bazes => ServiceLocator.Register(bazes, ServiceLocator.ImportMultipleIBaz));

            ImportMultiple<IQux, IQuxFactory>(
                quxes => ServiceLocator.Register(quxes, ServiceLocator.ImportMultipleIQuxIQuxFactory),
                factory => factory.GetQux());

            ImportMultiple<BazBase>(
                bazes => ServiceLocator.Register(bazes, ServiceLocator.ImportMultipleBazBase));

            ImportMultiple<QuxBase, QuxFactoryBase>(
                quxes => ServiceLocator.Register(quxes, ServiceLocator.ImportMultipleQuxBaseQuxFactoryBase),
                factory => factory.GetQux());
        }

        private void ImportMultipleTests_GivenMultipleNamedImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically()
        {
            ImportMultiple<IBaz>(
                bazes => ServiceLocator.Register(bazes, ServiceLocator.ImportMultipleIBazNamed),
                ServiceLocator.MyName);

            ImportMultiple<IQux, IQuxFactory>(
                quxes => ServiceLocator.Register(quxes, ServiceLocator.ImportMultipleIQuxIQuxFactoryNamed),
                factory => factory.GetQux(),
                ServiceLocator.MyName);

            ImportMultiple<BazBase>(
                bazes => ServiceLocator.Register(bazes, ServiceLocator.ImportMultipleBazBaseNamed),
                ServiceLocator.MyName);

            ImportMultiple<QuxBase, QuxFactoryBase>(
                quxes => ServiceLocator.Register(quxes, ServiceLocator.ImportMultipleQuxBaseQuxFactoryBaseNamed),
                factory => factory.GetQux(),
                ServiceLocator.MyName);
        }

        private void ImportOptionsParameterTests_HandlesAllowNonPublicClasses()
        {
            var importOptions = new ImportOptions { AllowNonPublicClasses = true };

            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFooAllowNonPublicClasses),
                ServiceLocator.AllowNonPublicClasses,
                importOptions);

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactoryAllowNonPublicClasses),
                factory => factory.GetBar(),
                ServiceLocator.AllowNonPublicClasses,
                importOptions);

            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooAllowNonPublicClasses),
                ServiceLocator.AllowNonPublicClasses,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryAllowNonPublicClasses),
                factory => factory.GetBar(),
                ServiceLocator.AllowNonPublicClasses,
                importOptions);

            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooAllowNonPublicClasses),
                ServiceLocator.AllowNonPublicClasses,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryAllowNonPublicClasses),
                factory => factory.GetBar(),
                ServiceLocator.AllowNonPublicClasses,
                importOptions);
        }

        private void ImportOptionsParameterTests_HandlesIncludeNamedExportsFromUnnamedImports()
        {
            var importOptions = new ImportOptions { IncludeNamedExportsFromUnnamedImports = true };

            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFooIncludeNamedExportsFromUnnamedImports),
                options:importOptions);

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports),
                factory => factory.GetBar(),
                options:importOptions);

            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooIncludeNamedExportsFromUnnamedImports),
                options:importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports),
                factory => factory.GetBar(),
                options:importOptions);

            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports),
                options:importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports),
                factory => factory.GetBar(),
                options:importOptions);
        }

        private void ImportOptionsParameterTests_HandlesPreferTTargetType()
        {
            var importOptions = new ImportOptions { PreferTTargetType = true };

            // PreferTTargetType has no effect on the non-factory import operations.

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactoryPreferTTargetType),
                factory => factory.GetBar(),
                ServiceLocator.PreferTTargetType,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryPreferTTargetType),
                factory => factory.GetBar(),
                ServiceLocator.PreferTTargetType,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryPreferTTargetType),
                factory => factory.GetBar(),
                ServiceLocator.PreferTTargetType,
                importOptions);
        }

        private void ImportOptionsParameterTests_HandlesIncludeTypesFromThisAssembly()
        {
            var importOptions = new ImportOptions { IncludeTypesFromThisAssembly = true };

            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFooIncludeTypesFromThisAssembly),
                ServiceLocator.IncludeTypesFromThisAssembly,
                importOptions);

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactoryIncludeTypesFromThisAssembly),
                factory => factory.GetBar(),
                ServiceLocator.IncludeTypesFromThisAssembly,
                importOptions);

            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooIncludeTypesFromThisAssembly),
                ServiceLocator.IncludeTypesFromThisAssembly,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryIncludeTypesFromThisAssembly),
                factory => factory.GetBar(),
                ServiceLocator.IncludeTypesFromThisAssembly,
                importOptions);

            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooIncludeTypesFromThisAssembly),
                ServiceLocator.IncludeTypesFromThisAssembly,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryIncludeTypesFromThisAssembly),
                factory => factory.GetBar(),
                ServiceLocator.IncludeTypesFromThisAssembly,
                importOptions);
        }

        private void ImportOptionsParameterTests_HandlesExportComparer()
        {
            var importOptions = new ImportOptions { ExportComparer = new ReversedTargetClassAssemblyQualifiedNameComparer() };

            // ExportComparer has no effect on ImportSingle operations.

            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooExportComparer),
                ServiceLocator.ExportComparer,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryExportComparer),
                factory => factory.GetBar(),
                ServiceLocator.ExportComparer,
                importOptions);

            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooExportComparer),
                ServiceLocator.ExportComparer,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryExportComparer),
                factory => factory.GetBar(),
                ServiceLocator.ExportComparer,
                importOptions);
        }

        private void PriorityTests_SingleHighestPriority()
        {
            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFooSingleHighestPriority),
                ServiceLocator.SingleHighestPriority);

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactorySingleHighestPriority),
                factory => factory.GetBar(),
                ServiceLocator.SingleHighestPriority);

            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooSingleHighestPriority),
                ServiceLocator.SingleHighestPriority);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactorySingleHighestPriority),
                factory => factory.GetBar(),
                ServiceLocator.SingleHighestPriority);

            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooSingleHighestPriority),
                ServiceLocator.SingleHighestPriority);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactorySingleHighestPriority),
                factory => factory.GetBar(),
                ServiceLocator.SingleHighestPriority);
        }

        private void PriorityTests_MultipleHighestPriority()
        {
            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFooMultipleHighestPriority),
                ServiceLocator.MultipleHighestPriority);

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactoryMultipleHighestPriority),
                factory => factory.GetBar(),
                ServiceLocator.MultipleHighestPriority);

            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooMultipleHighestPriority),
                ServiceLocator.MultipleHighestPriority);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryMultipleHighestPriority),
                factory => factory.GetBar(),
                ServiceLocator.MultipleHighestPriority);

            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooMultipleHighestPriority),
                ServiceLocator.MultipleHighestPriority);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryMultipleHighestPriority),
                factory => factory.GetBar(),
                ServiceLocator.MultipleHighestPriority);
        }

        private void DuplicateExportTests()
        {
            ImportSingle<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportSingleIFooDuplicateExport),
                ServiceLocator.DuplicateExport);

            ImportSingle<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportSingleIBarIBarFactoryDuplicateExport),
                factory => factory.GetBar(),
                ServiceLocator.DuplicateExport);

            ImportFirst<IFoo>(
                foo => ServiceLocator.Register(foo, ServiceLocator.ImportFirstIFooDuplicateExport),
                ServiceLocator.DuplicateExport);

            ImportFirst<IBar, IBarFactory>(
                bar => ServiceLocator.Register(bar, ServiceLocator.ImportFirstIBarIBarFactoryDuplicateExport),
                factory => factory.GetBar(),
                ServiceLocator.DuplicateExport);

            ImportMultiple<IFoo>(
                foos => ServiceLocator.Register(foos, ServiceLocator.ImportMultipleIFooDuplicateExport),
                ServiceLocator.DuplicateExport);

            ImportMultiple<IBar, IBarFactory>(
                bars => ServiceLocator.Register(bars, ServiceLocator.ImportMultipleIBarIBarFactoryDuplicateExport),
                factory => factory.GetBar(),
                ServiceLocator.DuplicateExport);
        }

        protected override IEnumerable<ExportInfo> GetExportInfos(Type type)
        {
            var attributes = Attribute.GetCustomAttributes(type, typeof(ExportAttribute));

            if (attributes.Length == 0)
            {
                return base.GetExportInfos(type);
            }

            return
                attributes.Cast<ExportAttribute>()
                .Select(attribute =>
                    new ExportInfo(type, attribute.Priority)
                    {
                        Disabled = attribute.Disabled,
                        Name = attribute.Name
                    });
        }

        protected override IEnumerable<ExportInfo> GetExportInfos(
            IEnumerable<CustomAttributeData> assemblyAttributeDataCollection)
        {
            return
                assemblyAttributeDataCollection.AsAttributes<ExportExternalAttribute>()
                    .Where(attribute => attribute.IsValidForAssemblyAttribute)
                    .Select(attribute =>
                        new ExportInfo(attribute.ClassType, attribute.Priority)
                        {
                            Disabled = attribute.Disabled,
                            Name = attribute.Name
                        });
        }

        private class ReversedTargetClassAssemblyQualifiedNameComparer : IComparer<ExportInfo>
        {
            public int Compare(ExportInfo lhs, ExportInfo rhs)
            {
                var lhsString = lhs.TargetClass.AssemblyQualifiedName ?? lhs.TargetClass.ToString();
                var rhsString = rhs.TargetClass.AssemblyQualifiedName ?? rhs.TargetClass.ToString();

                // ReSharper disable once StringCompareToIsCultureSpecific
                return rhsString.CompareTo(lhsString);
            }
        }
    }
}
