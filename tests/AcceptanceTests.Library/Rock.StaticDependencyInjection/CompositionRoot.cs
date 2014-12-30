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
            // ImportSingleTests.GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed

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

            // ImportSingleTests.GivenMultipleImplementationsForTheAbstraction_ThenNoImplementationIsUsed

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

            // ImportFirstTests.GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed

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

            // ImportFirstTests.GivenASingleImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed

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

            // 

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

            // 

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

        protected override ExportInfo GetExportInfo(Type type)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(ExportAttribute)) as ExportAttribute;

            if (attribute == null)
            {
                return base.GetExportInfo(type);
            }

            return
                new ExportInfo(type, attribute.Priority)
                {
                    Disabled = attribute.Disabled,
                    Name = attribute.Name
                };
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
    }
}
