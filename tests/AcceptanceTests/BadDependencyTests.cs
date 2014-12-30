using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class BadDependencyTests
    {
        [TestCase(typeof(IFoo), ServiceLocator.FooBadConstructor, ServiceLocator.ImportSingleIFooBadDependency, TestName = ServiceLocator.ImportSingleIFooBadDependency + ":" + ServiceLocator.FooBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadConstructor, ServiceLocator.ImportSingleIBarIBarFactoryBadDependency, TestName = ServiceLocator.ImportSingleIBarIBarFactoryBadDependency + ":" + ServiceLocator.BarFactoryBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadMethod, ServiceLocator.ImportSingleIBarIBarFactoryBadDependency, TestName = ServiceLocator.ImportSingleIBarIBarFactoryBadDependency + ":" + ServiceLocator.BarFactoryBadMethod)]
        [TestCase(typeof(IFoo), ServiceLocator.FooBadConstructor, ServiceLocator.ImportFirstIFooBadDependency, TestName = ServiceLocator.ImportFirstIFooBadDependency + ":" + ServiceLocator.FooBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadConstructor, ServiceLocator.ImportFirstIBarIBarFactoryBadDependency, TestName = ServiceLocator.ImportFirstIBarIBarFactoryBadDependency + ":" + ServiceLocator.BarFactoryBadConstructor)]
        [TestCase(typeof(IBar), ServiceLocator.BarFactoryBadMethod, ServiceLocator.ImportFirstIBarIBarFactoryBadDependency, TestName = ServiceLocator.ImportFirstIBarIBarFactoryBadDependency + ":" + ServiceLocator.BarFactoryBadMethod)]
        [TestCase(typeof(IEnumerable<IFoo>), ServiceLocator.FooBadConstructor, ServiceLocator.ImportMultipleIFooBadDependency, TestName = ServiceLocator.ImportMultipleIFooBadDependency + ":" + ServiceLocator.FooBadConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.BarFactoryBadConstructor, ServiceLocator.ImportMultipleIBarIBarFactoryBadDependency, TestName = ServiceLocator.ImportMultipleIBarIBarFactoryBadDependency + ":" + ServiceLocator.BarFactoryBadConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), ServiceLocator.BarFactoryBadMethod, ServiceLocator.ImportMultipleIBarIBarFactoryBadDependency, TestName = ServiceLocator.ImportMultipleIBarIBarFactoryBadDependency + ":" + ServiceLocator.BarFactoryBadMethod)]
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