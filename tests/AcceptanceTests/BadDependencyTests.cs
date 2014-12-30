using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class BadDependencyTests
    {
        [TestCase(typeof(IFoo), ServiceLocator.FooBadConstructor, ServiceLocator.ImportSingleIFooBadConstructor, TestName = ServiceLocator.ImportSingleIFooBadConstructor + ":" + ServiceLocator.FooBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadConstructor, ServiceLocator.ImportSingleIBarIBarFactoryBadConstructor, TestName = ServiceLocator.ImportSingleIBarIBarFactoryBadConstructor + ":" + ServiceLocator.BarFactoryBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadMethod, ServiceLocator.ImportSingleIBarIBarFactoryBadConstructor, TestName = ServiceLocator.ImportSingleIBarIBarFactoryBadConstructor + ":" + ServiceLocator.BarFactoryBadMethod)]
        [TestCase(typeof(IFoo), ServiceLocator.FooBadConstructor, ServiceLocator.ImportFirstIFooBadConstructor, TestName = ServiceLocator.ImportFirstIFooBadConstructor + ":" + ServiceLocator.FooBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadConstructor, ServiceLocator.ImportFirstIBarIBarFactoryBadConstructor, TestName = ServiceLocator.ImportFirstIBarIBarFactoryBadConstructor + ":" + ServiceLocator.BarFactoryBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadMethod, ServiceLocator.ImportFirstIBarIBarFactoryBadConstructor, TestName = ServiceLocator.ImportFirstIBarIBarFactoryBadConstructor + ":" + ServiceLocator.BarFactoryBadMethod)]
        [TestCase(typeof(IEnumerable<IFoo>), ServiceLocator.FooBadConstructor, ServiceLocator.ImportMultipleIFooBadConstructor, TestName = ServiceLocator.ImportMultipleIFooBadConstructor + ":" + ServiceLocator.FooBadConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.BarFactoryBadConstructor, ServiceLocator.ImportMultipleIBarIBarFactoryBadConstructor, TestName = ServiceLocator.ImportMultipleIBarIBarFactoryBadConstructor + ":" + ServiceLocator.BarFactoryBadConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.BarFactoryBadMethod, ServiceLocator.ImportMultipleIBarIBarFactoryBadConstructor, TestName = ServiceLocator.ImportMultipleIBarIBarFactoryBadConstructor + ":" + ServiceLocator.BarFactoryBadMethod)]
        public void DependenciesThatThrowExceptionsDuringCreationAreIgnored(Type serviceAbstractionType, string importExportName, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName + ":" + importExportName);

            if (registeredInstance is IEnumerable)
            {
                Assert.That(registeredInstance, Is.Empty);
            }
            else
            {
                Assert.That(registeredInstance, Is.Null);
            }
        }
    }
}