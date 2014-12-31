using System;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class ImportSingleTests
    {
        [TestCase(typeof(IFoo), typeof(FooImplementation), DiscoveredDependency.ImportSingleIFoo, TestName = DiscoveredDependency.ImportSingleIFoo)]
        [TestCase(typeof(IBar), typeof(BarImplementation), DiscoveredDependency.ImportSingleIBarIBarFactory, TestName = DiscoveredDependency.ImportSingleIBarIBarFactory)]
        [TestCase(typeof(FooBase), typeof(FooInheritor), DiscoveredDependency.ImportSingleFooBase, TestName = DiscoveredDependency.ImportSingleFooBase)]
        [TestCase(typeof(BarBase), typeof(BarInheritor), DiscoveredDependency.ImportSingleBarBaseBarFactoryBase, TestName = DiscoveredDependency.ImportSingleBarBaseBarFactoryBase)]
        public void GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IBaz), DiscoveredDependency.ImportSingleIBaz, TestName = DiscoveredDependency.ImportSingleIBaz)]
        [TestCase(typeof(IQux), DiscoveredDependency.ImportSingleIQuxIQuxFactory, TestName = DiscoveredDependency.ImportSingleIQuxIQuxFactory)]
        [TestCase(typeof(BazBase), DiscoveredDependency.ImportSingleBazBase, TestName = DiscoveredDependency.ImportSingleBazBase)]
        [TestCase(typeof(QuxBase), DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBase, TestName = DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBase)]
        public void GivenMultipleImplementationsForTheAbstraction_ThenNoImplementationIsUsed(Type serviceAbstractionType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.Null);
        }

        [TestCase(typeof(IFoo), typeof(NamedFooImplementation), DiscoveredDependency.ImportSingleIFooNamed, TestName = DiscoveredDependency.ImportSingleIFooNamed)]
        [TestCase(typeof(IBar), typeof(NamedBarImplementation), DiscoveredDependency.ImportSingleIBarIBarFactoryNamed, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryNamed)]
        [TestCase(typeof(FooBase), typeof(NamedFooInheritor), DiscoveredDependency.ImportSingleFooBaseNamed, TestName = DiscoveredDependency.ImportSingleFooBaseNamed)]
        [TestCase(typeof(BarBase), typeof(NamedBarInheritor), DiscoveredDependency.ImportSingleBarBaseBarFactoryBaseNamed, TestName = DiscoveredDependency.ImportSingleBarBaseBarFactoryBaseNamed)]
        public void GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IBaz), DiscoveredDependency.ImportSingleIBazNamed, TestName = DiscoveredDependency.ImportSingleIBazNamed)]
        [TestCase(typeof(IQux), DiscoveredDependency.ImportSingleIQuxIQuxFactoryNamed, TestName = DiscoveredDependency.ImportSingleIQuxIQuxFactoryNamed)]
        [TestCase(typeof(BazBase), DiscoveredDependency.ImportSingleBazBaseNamed, TestName = DiscoveredDependency.ImportSingleBazBaseNamed)]
        [TestCase(typeof(QuxBase), DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBaseNamed, TestName = DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBaseNamed)]
        public void GivenMultipleNamedImplementationsForTheAbstraction_ThenNoImplementationIsUsed(Type serviceAbstractionType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.Null);
        }
    }
}
