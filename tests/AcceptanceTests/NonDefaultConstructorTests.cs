using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class NonDefaultConstructorTests
    {
        [TestCase(typeof(IFoo), typeof(NonDefaultConstructorFoo), ServiceLocator.ImportSingleIFooNonDefaultConstructor, TestName = ServiceLocator.ImportSingleIFooNonDefaultConstructor)]
        [TestCase(typeof(IBar), typeof(NonDefaultConstructorBar), ServiceLocator.ImportSingleIBarIBarFactoryNonDefaultConstructor, TestName = ServiceLocator.ImportSingleIBarIBarFactoryNonDefaultConstructor)]
        public void ImportSingleCanInvokeNonDefaultPublicConstructorWithAllOptionalParameters(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(NonDefaultConstructorFoo), ServiceLocator.ImportFirstIFooNonDefaultConstructor, TestName = ServiceLocator.ImportFirstIFooNonDefaultConstructor)]
        [TestCase(typeof(IBar), typeof(NonDefaultConstructorBar), ServiceLocator.ImportFirstIBarIBarFactoryNonDefaultConstructor, TestName = ServiceLocator.ImportFirstIBarIBarFactoryNonDefaultConstructor)]
        public void ImportFirstCanInvokeNonDefaultPublicConstructorWithAllOptionalParameters(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(NonDefaultConstructorFoo) }, ServiceLocator.ImportMultipleIFooNonDefaultConstructor, TestName = ServiceLocator.ImportMultipleIFooNonDefaultConstructor)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(NonDefaultConstructorBar) }, ServiceLocator.ImportMultipleIBarIBarFactoryNonDefaultConstructor, TestName = ServiceLocator.ImportMultipleIBarIBarFactoryNonDefaultConstructor)]
        public void ImportMultipleCanInvokeNonDefaultPublicConstructorWithAllOptionalParameters(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }
    }
}