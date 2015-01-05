using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
// Rock.StaticDependencyInjection: BEGIN EXAMPLE
using Rock.StaticDependencyInjection.Tests;
// Rock.StaticDependencyInjection: END EXAMPLE

namespace Rock.StaticDependencyInjection
{
    internal partial class CompositionRoot : CompositionRootBase
    {
        public override void Bootstrap()
        {
            // This method is used to bootstrap the static dependencies of your
            // library. To do this, call the various import methods of this class:
            //  - ImportSingle
            //  - ImportFirst
            //  - ImportMultiple
            // Rock.StaticDependencyInjection: BEGIN EXAMPLE
            ImportForAcceptanceTests();
            // Rock.StaticDependencyInjection: END EXAMPLE
        }

        /// <summary>
        /// Gets a value indicating whether static dependency injection is enabled.
        /// </summary>
        public override bool IsEnabled
        {
            get
            {
                const string key = "Rock.StaticDependencyInjection.Enabled";
                var enabledValue = ConfigurationManager.AppSettings.Get(key) ?? "true";
                return enabledValue.ToLower() != "false";
            }
        }

        /// <summary>
        /// Return a collection of metadata objects that describe the export operations for a type.
        /// </summary>
        /// <param name="type">The type to get export metadata.</param>
        /// <returns>A collection of metadata objects that describe export operations.</returns>
        protected override IEnumerable<ExportInfo> GetExportInfos(Type type)
        {
            // Modify this method if your library needs to support a different
            // export mechanism (possibly a different attribute) that inspects
            // the type of a class.
            //
            // Remove this method if your library should not support any advanced
            // export mechanisms based on the type of a class.

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
        // Rock.StaticDependencyInjection: BEGIN EXAMPLE
        private void ImportForAcceptanceTests()
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
            NonDefaultConstructorTests();
            BadDependencyTests();
            DisabledExportTests();
        }

        private void ImportSingleTests_GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFoo));

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactory),
                factory => factory.GetBar());

            ImportSingle<FooBase>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleFooBase));

            ImportSingle<BarBase, BarFactoryBase>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleBarBaseBarFactoryBase),
                factory => factory.GetBar());
        }

        private void ImportSingleTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooNamed),
                DiscoveredDependency.MyName);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryNamed),
                factory => factory.GetBar(),
                DiscoveredDependency.MyName);

            ImportSingle<FooBase>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleFooBaseNamed),
                DiscoveredDependency.MyName);

            ImportSingle<BarBase, BarFactoryBase>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleBarBaseBarFactoryBaseNamed),
                factory => factory.GetBar(),
                DiscoveredDependency.MyName);
        }

        private void ImportSingleTests_GivenMultipleImplementationsForTheAbstraction_ThenNoImplementationIsUsed()
        {
            ImportSingle<IBaz>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportSingleIBaz));

            ImportSingle<IQux, IQuxFactory>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportSingleIQuxIQuxFactory),
                factory => factory.GetQux());

            ImportSingle<BazBase>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportSingleBazBase));

            ImportSingle<QuxBase, QuxFactoryBase>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBase),
                factory => factory.GetQux());
        }

        private void ImportSingleTests_GivenMultipleNamedImplementationsForTheAbstraction_ThenNoImplementationIsUsed()
        {
            ImportSingle<IBaz>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportSingleIBazNamed),
                DiscoveredDependency.MyName);

            ImportSingle<IQux, IQuxFactory>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportSingleIQuxIQuxFactoryNamed),
                factory => factory.GetQux(),
                DiscoveredDependency.MyName);

            ImportSingle<BazBase>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportSingleBazBaseNamed),
                DiscoveredDependency.MyName);

            ImportSingle<QuxBase, QuxFactoryBase>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBaseNamed),
                factory => factory.GetQux(),
                DiscoveredDependency.MyName);
        }

        private void ImportFirstTests_GivenASingleImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed()
        {
            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFoo));

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactory),
                factory => factory.GetBar());

            ImportFirst<FooBase>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstFooBase));

            ImportFirst<BarBase, BarFactoryBase>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstBarBaseBarFactoryBase),
                factory => factory.GetBar());
        }

        private void ImportFirstTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed()
        {
            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooNamed),
                DiscoveredDependency.MyName);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryNamed),
                factory => factory.GetBar(),
                DiscoveredDependency.MyName);

            ImportFirst<FooBase>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstFooBaseNamed),
                DiscoveredDependency.MyName);

            ImportFirst<BarBase, BarFactoryBase>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstBarBaseBarFactoryBaseNamed),
                factory => factory.GetBar(),
                DiscoveredDependency.MyName);
        }

        private void ImportFirstTests_GivenMultipleImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed()
        {
            ImportFirst<IBaz>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportFirstIBaz));

            ImportFirst<IQux, IQuxFactory>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportFirstIQuxIQuxFactory),
                factory => factory.GetQux());

            ImportFirst<BazBase>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportFirstBazBase));

            ImportFirst<QuxBase, QuxFactoryBase>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportFirstQuxBaseQuxFactoryBase),
                factory => factory.GetQux());
        }

        private void ImportFirstTests_GivenMultipleNamedImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed()
        {
            ImportFirst<IBaz>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportFirstIBazNamed),
                DiscoveredDependency.MyName);

            ImportFirst<IQux, IQuxFactory>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportFirstIQuxIQuxFactoryNamed),
                factory => factory.GetQux(),
                DiscoveredDependency.MyName);

            ImportFirst<BazBase>(
                baz => DiscoveredDependency.Register(baz, DiscoveredDependency.ImportFirstBazBaseNamed),
                DiscoveredDependency.MyName);

            ImportFirst<QuxBase, QuxFactoryBase>(
                qux => DiscoveredDependency.Register(qux, DiscoveredDependency.ImportFirstQuxBaseQuxFactoryBaseNamed),
                factory => factory.GetQux(),
                DiscoveredDependency.MyName);
        }

        private void ImportMultipleTests_GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFoo));

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactory),
                factory => factory.GetBar());

            ImportMultiple<FooBase>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleFooBase));

            ImportMultiple<BarBase, BarFactoryBase>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleBarBaseBarFactoryBase),
                factory => factory.GetBar());
        }

        private void ImportMultipleTests_GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed()
        {
            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooNamed),
                DiscoveredDependency.MyName);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryNamed),
                factory => factory.GetBar(),
                DiscoveredDependency.MyName);

            ImportMultiple<FooBase>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleFooBaseNamed),
                DiscoveredDependency.MyName);

            ImportMultiple<BarBase, BarFactoryBase>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleBarBaseBarFactoryBaseNamed),
                factory => factory.GetBar(),
                DiscoveredDependency.MyName);
        }

        private void ImportMultipleTests_GivenMultipleImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically()
        {
            ImportMultiple<IBaz>(
                bazes => DiscoveredDependency.Register(bazes, DiscoveredDependency.ImportMultipleIBaz));

            ImportMultiple<IQux, IQuxFactory>(
                quxes => DiscoveredDependency.Register(quxes, DiscoveredDependency.ImportMultipleIQuxIQuxFactory),
                factory => factory.GetQux());

            ImportMultiple<BazBase>(
                bazes => DiscoveredDependency.Register(bazes, DiscoveredDependency.ImportMultipleBazBase));

            ImportMultiple<QuxBase, QuxFactoryBase>(
                quxes => DiscoveredDependency.Register(quxes, DiscoveredDependency.ImportMultipleQuxBaseQuxFactoryBase),
                factory => factory.GetQux());
        }

        private void ImportMultipleTests_GivenMultipleNamedImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically()
        {
            ImportMultiple<IBaz>(
                bazes => DiscoveredDependency.Register(bazes, DiscoveredDependency.ImportMultipleIBazNamed),
                DiscoveredDependency.MyName);

            ImportMultiple<IQux, IQuxFactory>(
                quxes => DiscoveredDependency.Register(quxes, DiscoveredDependency.ImportMultipleIQuxIQuxFactoryNamed),
                factory => factory.GetQux(),
                DiscoveredDependency.MyName);

            ImportMultiple<BazBase>(
                bazes => DiscoveredDependency.Register(bazes, DiscoveredDependency.ImportMultipleBazBaseNamed),
                DiscoveredDependency.MyName);

            ImportMultiple<QuxBase, QuxFactoryBase>(
                quxes => DiscoveredDependency.Register(quxes, DiscoveredDependency.ImportMultipleQuxBaseQuxFactoryBaseNamed),
                factory => factory.GetQux(),
                DiscoveredDependency.MyName);
        }

        private void ImportOptionsParameterTests_HandlesAllowNonPublicClasses()
        {
            var importOptions = new ImportOptions { AllowNonPublicClasses = true };

            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooAllowNonPublicClasses),
                DiscoveredDependency.AllowNonPublicClasses,
                importOptions);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryAllowNonPublicClasses),
                factory => factory.GetBar(),
                DiscoveredDependency.AllowNonPublicClasses,
                importOptions);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooAllowNonPublicClasses),
                DiscoveredDependency.AllowNonPublicClasses,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryAllowNonPublicClasses),
                factory => factory.GetBar(),
                DiscoveredDependency.AllowNonPublicClasses,
                importOptions);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooAllowNonPublicClasses),
                DiscoveredDependency.AllowNonPublicClasses,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryAllowNonPublicClasses),
                factory => factory.GetBar(),
                DiscoveredDependency.AllowNonPublicClasses,
                importOptions);
        }

        private void ImportOptionsParameterTests_HandlesIncludeNamedExportsFromUnnamedImports()
        {
            var importOptions = new ImportOptions { IncludeNamedExportsFromUnnamedImports = true };

            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooIncludeNamedExportsFromUnnamedImports),
                options: importOptions);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports),
                factory => factory.GetBar(),
                options: importOptions);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooIncludeNamedExportsFromUnnamedImports),
                options: importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports),
                factory => factory.GetBar(),
                options: importOptions);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports),
                options: importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports),
                factory => factory.GetBar(),
                options: importOptions);
        }

        private void ImportOptionsParameterTests_HandlesPreferTTargetType()
        {
            var importOptions = new ImportOptions { PreferTTargetType = true };

            // PreferTTargetType has no effect on the non-factory import operations.

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryPreferTTargetType),
                factory => factory.GetBar(),
                DiscoveredDependency.PreferTTargetType,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryPreferTTargetType),
                factory => factory.GetBar(),
                DiscoveredDependency.PreferTTargetType,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryPreferTTargetType),
                factory => factory.GetBar(),
                DiscoveredDependency.PreferTTargetType,
                importOptions);
        }

        private void ImportOptionsParameterTests_HandlesIncludeTypesFromThisAssembly()
        {
            var importOptions = new ImportOptions { IncludeTypesFromThisAssembly = true };

            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooIncludeTypesFromThisAssembly),
                DiscoveredDependency.IncludeTypesFromThisAssembly,
                importOptions);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryIncludeTypesFromThisAssembly),
                factory => factory.GetBar(),
                DiscoveredDependency.IncludeTypesFromThisAssembly,
                importOptions);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooIncludeTypesFromThisAssembly),
                DiscoveredDependency.IncludeTypesFromThisAssembly,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryIncludeTypesFromThisAssembly),
                factory => factory.GetBar(),
                DiscoveredDependency.IncludeTypesFromThisAssembly,
                importOptions);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooIncludeTypesFromThisAssembly),
                DiscoveredDependency.IncludeTypesFromThisAssembly,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryIncludeTypesFromThisAssembly),
                factory => factory.GetBar(),
                DiscoveredDependency.IncludeTypesFromThisAssembly,
                importOptions);
        }

        private void ImportOptionsParameterTests_HandlesExportComparer()
        {
            var importOptions = new ImportOptions { ExportComparer = new ReversedTargetClassAssemblyQualifiedNameComparer() };

            // ExportComparer has no effect on ImportSingle operations.

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooExportComparer),
                DiscoveredDependency.ExportComparer,
                importOptions);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryExportComparer),
                factory => factory.GetBar(),
                DiscoveredDependency.ExportComparer,
                importOptions);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooExportComparer),
                DiscoveredDependency.ExportComparer,
                importOptions);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryExportComparer),
                factory => factory.GetBar(),
                DiscoveredDependency.ExportComparer,
                importOptions);
        }

        private void PriorityTests_SingleHighestPriority()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooSingleHighestPriority),
                DiscoveredDependency.SingleHighestPriority);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactorySingleHighestPriority),
                factory => factory.GetBar(),
                DiscoveredDependency.SingleHighestPriority);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooSingleHighestPriority),
                DiscoveredDependency.SingleHighestPriority);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactorySingleHighestPriority),
                factory => factory.GetBar(),
                DiscoveredDependency.SingleHighestPriority);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooSingleHighestPriority),
                DiscoveredDependency.SingleHighestPriority);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactorySingleHighestPriority),
                factory => factory.GetBar(),
                DiscoveredDependency.SingleHighestPriority);
        }

        private void PriorityTests_MultipleHighestPriority()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooMultipleHighestPriority),
                DiscoveredDependency.MultipleHighestPriority);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryMultipleHighestPriority),
                factory => factory.GetBar(),
                DiscoveredDependency.MultipleHighestPriority);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooMultipleHighestPriority),
                DiscoveredDependency.MultipleHighestPriority);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryMultipleHighestPriority),
                factory => factory.GetBar(),
                DiscoveredDependency.MultipleHighestPriority);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooMultipleHighestPriority),
                DiscoveredDependency.MultipleHighestPriority);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryMultipleHighestPriority),
                factory => factory.GetBar(),
                DiscoveredDependency.MultipleHighestPriority);
        }

        private void DuplicateExportTests()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooDuplicateExport),
                DiscoveredDependency.DuplicateExport);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryDuplicateExport),
                factory => factory.GetBar(),
                DiscoveredDependency.DuplicateExport);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooDuplicateExport),
                DiscoveredDependency.DuplicateExport);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryDuplicateExport),
                factory => factory.GetBar(),
                DiscoveredDependency.DuplicateExport);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooDuplicateExport),
                DiscoveredDependency.DuplicateExport);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryDuplicateExport),
                factory => factory.GetBar(),
                DiscoveredDependency.DuplicateExport);
        }

        private void NonDefaultConstructorTests()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooNonDefaultConstructor),
                DiscoveredDependency.NonDefaultConstructor);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryNonDefaultConstructor),
                factory => factory.GetBar(),
                DiscoveredDependency.NonDefaultConstructor);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooNonDefaultConstructor),
                DiscoveredDependency.NonDefaultConstructor);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryNonDefaultConstructor),
                factory => factory.GetBar(),
                DiscoveredDependency.NonDefaultConstructor);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooNonDefaultConstructor),
                DiscoveredDependency.NonDefaultConstructor);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryNonDefaultConstructor),
                factory => factory.GetBar(),
                DiscoveredDependency.NonDefaultConstructor);
        }

        private void BadDependencyTests()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor),
                DiscoveredDependency.FooBadConstructor);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor),
                factory => factory.GetBar(),
                DiscoveredDependency.BarFactoryBadConstructor);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod),
                factory => factory.GetBar(),
                DiscoveredDependency.BarFactoryBadMethod);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor),
                DiscoveredDependency.FooBadConstructor);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor),
                factory => factory.GetBar(),
                DiscoveredDependency.BarFactoryBadConstructor);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod),
                factory => factory.GetBar(),
                DiscoveredDependency.BarFactoryBadMethod);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor),
                DiscoveredDependency.FooBadConstructor);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor),
                factory => factory.GetBar(),
                DiscoveredDependency.BarFactoryBadConstructor);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod),
                factory => factory.GetBar(),
                DiscoveredDependency.BarFactoryBadMethod);
        }

        private void DisabledExportTests()
        {
            ImportSingle<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportSingleIFooDisabled),
                DiscoveredDependency.Disabled);

            ImportSingle<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportSingleIBarIBarFactoryDisabled),
                factory => factory.GetBar(),
                DiscoveredDependency.Disabled);

            ImportFirst<IFoo>(
                foo => DiscoveredDependency.Register(foo, DiscoveredDependency.ImportFirstIFooDisabled),
                DiscoveredDependency.Disabled);

            ImportFirst<IBar, IBarFactory>(
                bar => DiscoveredDependency.Register(bar, DiscoveredDependency.ImportFirstIBarIBarFactoryDisabled),
                factory => factory.GetBar(),
                DiscoveredDependency.Disabled);

            ImportMultiple<IFoo>(
                foos => DiscoveredDependency.Register(foos, DiscoveredDependency.ImportMultipleIFooDisabled),
                DiscoveredDependency.Disabled);

            ImportMultiple<IBar, IBarFactory>(
                bars => DiscoveredDependency.Register(bars, DiscoveredDependency.ImportMultipleIBarIBarFactoryDisabled),
                factory => factory.GetBar(),
                DiscoveredDependency.Disabled);
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
        // Rock.StaticDependencyInjection: END EXAMPLE
    }
}