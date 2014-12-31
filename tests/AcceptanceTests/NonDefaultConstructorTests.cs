using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class NonDefaultConstructorTests
    {
        [TestCase(typeof(IFoo), typeof(NonDefaultConstructorFoo), DiscoveredDependency.ImportSingleIFooNonDefaultConstructor, TestName = DiscoveredDependency.ImportSingleIFooNonDefaultConstructor)]
        [TestCase(typeof(IBar), typeof(NonDefaultConstructorBar), DiscoveredDependency.ImportSingleIBarIBarFactoryNonDefaultConstructor, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryNonDefaultConstructor)]
        public void ImportSingleCanInvokeNonDefaultPublicConstructorWithAllOptionalParameters(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(NonDefaultConstructorFoo), DiscoveredDependency.ImportFirstIFooNonDefaultConstructor, TestName = DiscoveredDependency.ImportFirstIFooNonDefaultConstructor)]
        [TestCase(typeof(IBar), typeof(NonDefaultConstructorBar), DiscoveredDependency.ImportFirstIBarIBarFactoryNonDefaultConstructor, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryNonDefaultConstructor)]
        public void ImportFirstCanInvokeNonDefaultPublicConstructorWithAllOptionalParameters(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(NonDefaultConstructorFoo) }, DiscoveredDependency.ImportMultipleIFooNonDefaultConstructor, TestName = DiscoveredDependency.ImportMultipleIFooNonDefaultConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(NonDefaultConstructorBar) }, DiscoveredDependency.ImportMultipleIBarIBarFactoryNonDefaultConstructor, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryNonDefaultConstructor)]
        public void ImportMultipleCanInvokeNonDefaultPublicConstructorWithAllOptionalParameters(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }
    }
}