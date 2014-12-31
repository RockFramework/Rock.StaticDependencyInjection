using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class ImportOptionsParameterTests
    {
        [TestCase(typeof(IFoo), DiscoveredDependency.ImportSingleIFooAllowNonPublicClasses, TestName = DiscoveredDependency.ImportSingleIFooAllowNonPublicClasses)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportSingleIBarIBarFactoryAllowNonPublicClasses, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryAllowNonPublicClasses)]
        [TestCase(typeof(IFoo), DiscoveredDependency.ImportFirstIFooAllowNonPublicClasses, typeof(NonPublicFoo), TestName = DiscoveredDependency.ImportFirstIFooAllowNonPublicClasses)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportFirstIBarIBarFactoryAllowNonPublicClasses, typeof(NonPublicBar), TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryAllowNonPublicClasses)]
        [TestCase(typeof(IEnumerable<IFoo>), DiscoveredDependency.ImportMultipleIFooAllowNonPublicClasses, typeof(NonPublicFoo), typeof(PublicFoo), TestName = DiscoveredDependency.ImportMultipleIFooAllowNonPublicClasses)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.ImportMultipleIBarIBarFactoryAllowNonPublicClasses, typeof(NonPublicBar), typeof(PublicBar), TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryAllowNonPublicClasses)]
        public void HandlesAllowNonPublicClasses(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IFoo), DiscoveredDependency.ImportSingleIFooIncludeNamedExportsFromUnnamedImports, TestName = DiscoveredDependency.ImportSingleIFooIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IFoo), DiscoveredDependency.ImportFirstIFooIncludeNamedExportsFromUnnamedImports, typeof(FooExportComparer1), TestName = DiscoveredDependency.ImportFirstIFooIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, typeof(BarExportComparer1), TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IEnumerable<IFoo>), DiscoveredDependency.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports, typeof(FooExportComparer1), typeof(FooExportComparer2), typeof(FooExportComparer3), typeof(NamedFooImplementation), typeof(PublicFoo), typeof(XyzFoo), typeof(FooImplementation), typeof(FooSingleHighestPriority3), typeof(FooSingleHighestPriority2), typeof(FooSingleHighestPriority1), typeof(FooMultipleHighestPriority1), typeof(FooMultipleHighestPriority2), typeof(DuplicateExportFoo), typeof(NonDefaultConstructorFoo), TestName = DiscoveredDependency.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, typeof(BarExportComparer1), typeof(BarExportComparer2), typeof(BarExportComparer3), typeof(FactoryTypeBarFactory.FactoryTypeBar), typeof(NamedBarImplementation), typeof(PublicBar), typeof(XyzBar), typeof(TargetTypeBar), typeof(BarImplementation), typeof(BarSingleHighestPriority3), typeof(BarSingleHighestPriority2), typeof(BarSingleHighestPriority1), typeof(BarMultipleHighestPriority1), typeof(BarMultipleHighestPriority2), typeof(DuplicateExportBar), typeof(NonDefaultConstructorBar), TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        public void HandlesIncludeNamedExportsFromUnnamedImports(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IBar), DiscoveredDependency.ImportSingleIBarIBarFactoryPreferTTargetType, typeof(TargetTypeBar), TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryPreferTTargetType)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportFirstIBarIBarFactoryPreferTTargetType, typeof(TargetTypeBar), TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryPreferTTargetType)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.ImportMultipleIBarIBarFactoryPreferTTargetType, typeof(TargetTypeBar), typeof(FactoryTypeBarFactory.FactoryTypeBar), TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryPreferTTargetType)]
        public void HandlesPreferTTargetType(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IFoo), DiscoveredDependency.ImportSingleIFooIncludeTypesFromThisAssembly, TestName = DiscoveredDependency.ImportSingleIFooIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportSingleIBarIBarFactoryIncludeTypesFromThisAssembly, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IFoo), DiscoveredDependency.ImportFirstIFooIncludeTypesFromThisAssembly, typeof(AbcFoo), TestName = DiscoveredDependency.ImportFirstIFooIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportFirstIBarIBarFactoryIncludeTypesFromThisAssembly, typeof(AbcBar), TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IEnumerable<IFoo>), DiscoveredDependency.ImportMultipleIFooIncludeTypesFromThisAssembly, typeof(AbcFoo), typeof(XyzFoo), TestName = DiscoveredDependency.ImportMultipleIFooIncludeTypesFromThisAssembly)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.ImportMultipleIBarIBarFactoryIncludeTypesFromThisAssembly, typeof(AbcBar), typeof(XyzBar), TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryIncludeTypesFromThisAssembly)]
        public void HandlesIncludeTypesFromThisAssembly(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        [TestCase(typeof(IFoo), DiscoveredDependency.ImportFirstIFooExportComparer, typeof(FooExportComparer3), TestName = DiscoveredDependency.ImportFirstIFooExportComparer)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportFirstIBarIBarFactoryExportComparer, typeof(BarExportComparer3), TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryExportComparer)]
        [TestCase(typeof(IEnumerable<IFoo>), DiscoveredDependency.ImportMultipleIFooExportComparer, typeof(FooExportComparer3), typeof(FooExportComparer2), typeof(FooExportComparer1), TestName = DiscoveredDependency.ImportMultipleIFooExportComparer)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.ImportMultipleIBarIBarFactoryExportComparer, typeof(BarExportComparer3), typeof(BarExportComparer2), typeof(BarExportComparer1), TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryExportComparer)]
        public void HandlesExportComparer(Type serviceAbstractionType, string serviceName, params Type[] expectedServiceTypes)
        {
            RunTest(serviceAbstractionType, serviceName, expectedServiceTypes);
        }

        private static void RunTest(Type serviceAbstractionType, string serviceName, Type[] expectedServiceTypes)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);

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
