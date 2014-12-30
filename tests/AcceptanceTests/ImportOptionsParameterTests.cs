using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class ImportOptionsParameterTests
    {
        [TestCase(typeof(IFoo), ServiceLocator.ImportSingleIFooAllowNonPublicClasses, TestName = ServiceLocator.ImportSingleIFooAllowNonPublicClasses)]
        [TestCase(typeof(IBar), ServiceLocator.ImportSingleIBarIBarFactoryAllowNonPublicClasses, TestName = ServiceLocator.ImportSingleIBarIBarFactoryAllowNonPublicClasses)]
        [TestCase(typeof(IFoo), ServiceLocator.ImportFirstIFooAllowNonPublicClasses, typeof(NonPublicFoo), TestName = ServiceLocator.ImportFirstIFooAllowNonPublicClasses)]
        [TestCase(typeof(IBar), ServiceLocator.ImportFirstIBarIBarFactoryAllowNonPublicClasses, typeof(NonPublicBar), TestName = ServiceLocator.ImportFirstIBarIBarFactoryAllowNonPublicClasses)]
        [TestCase(typeof(IEnumerable<IFoo>), ServiceLocator.ImportMultipleIFooAllowNonPublicClasses, typeof(NonPublicFoo), typeof(PublicFoo), TestName = ServiceLocator.ImportMultipleIFooAllowNonPublicClasses)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.ImportMultipleIBarIBarFactoryAllowNonPublicClasses, typeof(NonPublicBar), typeof(PublicBar), TestName = ServiceLocator.ImportMultipleIBarIBarFactoryAllowNonPublicClasses)]
        public void HandlesAllowNonPublicClasses(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IFoo), ServiceLocator.ImportSingleIFooIncludeNamedExportsFromUnnamedImports, TestName = ServiceLocator.ImportSingleIFooIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IBar), ServiceLocator.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, TestName = ServiceLocator.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IFoo), ServiceLocator.ImportFirstIFooIncludeNamedExportsFromUnnamedImports, typeof(FooExportComparer1), TestName = ServiceLocator.ImportFirstIFooIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IBar), ServiceLocator.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, typeof(BarExportComparer1), TestName = ServiceLocator.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IEnumerable<IFoo>), ServiceLocator.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports, typeof(FooExportComparer1), typeof(FooExportComparer2), typeof(FooExportComparer3), typeof(NamedFooImplementation), typeof(PublicFoo), typeof(XyzFoo), typeof(FooImplementation), typeof(FooSingleHighestPriority3), typeof(FooSingleHighestPriority2), typeof(FooSingleHighestPriority1), typeof(FooMultipleHighestPriority1), typeof(FooMultipleHighestPriority2), TestName = ServiceLocator.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, typeof(BarExportComparer1), typeof(BarExportComparer2), typeof(BarExportComparer3), typeof(FactoryTypeBarFactory.FactoryTypeBar), typeof(NamedBarImplementation), typeof(PublicBar), typeof(XyzBar), typeof(TargetTypeBar), typeof(BarImplementation), typeof(BarSingleHighestPriority3), typeof(BarSingleHighestPriority2), typeof(BarSingleHighestPriority1), typeof(BarMultipleHighestPriority1), typeof(BarMultipleHighestPriority2), TestName = ServiceLocator.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        public void HandlesIncludeNamedExportsFromUnnamedImports(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IBar), ServiceLocator.ImportSingleIBarIBarFactoryPreferTTargetType, typeof(TargetTypeBar), TestName = ServiceLocator.ImportSingleIBarIBarFactoryPreferTTargetType)]
        [TestCase(typeof(IBar), ServiceLocator.ImportFirstIBarIBarFactoryPreferTTargetType, typeof(TargetTypeBar), TestName = ServiceLocator.ImportFirstIBarIBarFactoryPreferTTargetType)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.ImportMultipleIBarIBarFactoryPreferTTargetType, typeof(TargetTypeBar), typeof(FactoryTypeBarFactory.FactoryTypeBar), TestName = ServiceLocator.ImportMultipleIBarIBarFactoryPreferTTargetType)]
        public void HandlesPreferTTargetType(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IFoo), ServiceLocator.ImportSingleIFooIncludeTypesFromThisAssembly, TestName = ServiceLocator.ImportSingleIFooIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IBar), ServiceLocator.ImportSingleIBarIBarFactoryIncludeTypesFromThisAssembly, TestName = ServiceLocator.ImportSingleIBarIBarFactoryIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IFoo), ServiceLocator.ImportFirstIFooIncludeTypesFromThisAssembly, typeof(AbcFoo), TestName = ServiceLocator.ImportFirstIFooIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IBar), ServiceLocator.ImportFirstIBarIBarFactoryIncludeTypesFromThisAssembly, typeof(AbcBar), TestName = ServiceLocator.ImportFirstIBarIBarFactoryIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IEnumerable<IFoo>), ServiceLocator.ImportMultipleIFooIncludeTypesFromThisAssembly, typeof(AbcFoo), typeof(XyzFoo), TestName = ServiceLocator.ImportMultipleIFooIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.ImportMultipleIBarIBarFactoryIncludeTypesFromThisAssembly, typeof(AbcBar), typeof(XyzBar), TestName = ServiceLocator.ImportMultipleIBarIBarFactoryIncludeTypesFromThisAssembly)]
        public void HandlesIncludeTypesFromThisAssembly(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IFoo), ServiceLocator.ImportFirstIFooExportComparer, typeof(FooExportComparer3), TestName = ServiceLocator.ImportFirstIFooExportComparer)]
        [TestCase(typeof(IBar), ServiceLocator.ImportFirstIBarIBarFactoryExportComparer, typeof(BarExportComparer3), TestName = ServiceLocator.ImportFirstIBarIBarFactoryExportComparer)]
        [TestCase(typeof(IEnumerable<IFoo>), ServiceLocator.ImportMultipleIFooExportComparer, typeof(FooExportComparer3), typeof(FooExportComparer2), typeof(FooExportComparer1), TestName = ServiceLocator.ImportMultipleIFooExportComparer)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.ImportMultipleIBarIBarFactoryExportComparer, typeof(BarExportComparer3), typeof(BarExportComparer2), typeof(BarExportComparer1), TestName = ServiceLocator.ImportMultipleIBarIBarFactoryExportComparer)]
        public void HandlesExportComparer(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        private static void RunTest(Type serviceAbstractionType, string serviceName, Type[] expectedServiceTypes)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);

            switch (expectedServiceTypes.Length)
            {
                case 0:
                    Assert.That(registeredInstance, Is.Null);
                    break;
                case 1:
                    Assert.That(registeredInstance, Is.InstanceOf(expectedServiceTypes[0]));
                    break;
                default:
                    ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
                    break;
            }
        }
    }
}
