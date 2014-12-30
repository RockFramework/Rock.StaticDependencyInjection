using System;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class ImportSingleTests
    {
        [TestCase(typeof(IFoo), typeof(FooImplementation), ServiceLocator.ImportSingleIFoo, TestName = ServiceLocator.ImportSingleIFoo)]
        [TestCase(typeof(IBar), typeof(BarImplementation), ServiceLocator.ImportSingleIBarIBarFactory, TestName = ServiceLocator.ImportSingleIBarIBarFactory)]
        [TestCase(typeof(FooBase), typeof(FooInheritor), ServiceLocator.ImportSingleFooBase, TestName = ServiceLocator.ImportSingleFooBase)]
        [TestCase(typeof(BarBase), typeof(BarInheritor), ServiceLocator.ImportSingleBarBaseBarFactoryBase, TestName = ServiceLocator.ImportSingleBarBaseBarFactoryBase)]
        public void GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IBaz), ServiceLocator.ImportSingleIBaz, TestName = ServiceLocator.ImportSingleIBaz)]
        [TestCase(typeof(IQux), ServiceLocator.ImportSingleIQuxIQuxFactory, TestName = ServiceLocator.ImportSingleIQuxIQuxFactory)]
        [TestCase(typeof(BazBase), ServiceLocator.ImportSingleBazBase, TestName = ServiceLocator.ImportSingleBazBase)]
        [TestCase(typeof(QuxBase), ServiceLocator.ImportSingleQuxBaseQuxFactoryBase, TestName = ServiceLocator.ImportSingleQuxBaseQuxFactoryBase)]
        public void GivenMultipleImplementationsForTheAbstraction_ThenNoImplementationIsUsed(Type serviceAbstractionType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.Null);
        }
    }
}
