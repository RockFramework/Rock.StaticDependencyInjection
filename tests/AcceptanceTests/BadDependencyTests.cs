using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class BadDependencyTests
    {
        [TestCase(typeof(IFoo), DiscoveredDependency.FooBadConstructor, DiscoveredDependency.ImportSingleIFooBadDependency, TestName = DiscoveredDependency.ImportSingleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadConstructor, DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadMethod, DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod)]
        [TestCase(typeof(IFoo), DiscoveredDependency.FooBadConstructor, DiscoveredDependency.ImportFirstIFooBadDependency, TestName = DiscoveredDependency.ImportFirstIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadConstructor, DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadMethod, DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod)]
        [TestCase(typeof(IEnumerable<IFoo>), DiscoveredDependency.FooBadConstructor, DiscoveredDependency.ImportMultipleIFooBadDependency, TestName = DiscoveredDependency.ImportMultipleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.BarFactoryBadConstructor, DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.BarFactoryBadMethod, DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod)]
        public void DependenciesThatThrowExceptionsDuringCreationAreIgnored(Type serviceAbstractionType, string importExportName, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName + ":" + importExportName);

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