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

        [TestCase(typeof(IFoo), ServiceLocator.ImportSingleIFooIncludeNamedExportsFromUnnamedImports, TestName = ServiceLocator.ImportSingleIFooIncludeNamedExportsFromUnnamedImports)]
        [TestCase(typeof(IBar), ServiceLocator.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, TestName = ServiceLocator.ImportSingleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        //[TestCase(typeof(IFoo), ServiceLocator.ImportFirstIFooIncludeNamedExportsFromUnnamedImports, typeof(Nothing), TestName = ServiceLocator.ImportFirstIFooIncludeNamedExportsFromUnnamedImports)]
        //[TestCase(typeof(IBar), ServiceLocator.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, typeof(Nothing), TestName = ServiceLocator.ImportFirstIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        //[TestCase(typeof(IFoo), ServiceLocator.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports, typeof(Nothing), typeof(Nothing), TestName = ServiceLocator.ImportMultipleIFooIncludeNamedExportsFromUnnamedImports)]
        //[TestCase(typeof(IBar), ServiceLocator.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports, typeof(Nothing), typeof(Nothing), TestName = ServiceLocator.ImportMultipleIBarIBarFactoryIncludeNamedExportsFromUnnamedImports)]
        public void HandlesIncludeNamedExportsFromUnnamedImports(Type importType, string serviceName, params Type[] expectedServiceTypes)
        {
            
        }

        public void HandlesPreferTTargetType()
        {
            
        }

        public void HandlesIncludeTypesFromThisAssembly()
        {
            
        }

        public void HandlesExportComparer()
        {
            
        }
    }
}
