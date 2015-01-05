using System;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class ImportFirstTests
    {
        [TestCase(typeof(IFoo), typeof(FooImplementation), DiscoveredDependency.ImportFirstIFoo, TestName = DiscoveredDependency.ImportFirstIFoo)]
        [TestCase(typeof(IBar), typeof(BarImplementation), DiscoveredDependency.ImportFirstIBarIBarFactory, TestName = DiscoveredDependency.ImportFirstIBarIBarFactory)]
        [TestCase(typeof(FooBase), typeof(FooInheritor), DiscoveredDependency.ImportFirstFooBase, TestName = DiscoveredDependency.ImportFirstFooBase)]
        [TestCase(typeof(BarBase), typeof(BarInheritor), DiscoveredDependency.ImportFirstBarBaseBarFactoryBase, TestName = DiscoveredDependency.ImportFirstBarBaseBarFactoryBase)]
        public void GivenASingleImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IBaz), typeof(BazImplementation1), DiscoveredDependency.ImportFirstIBaz, TestName = DiscoveredDependency.ImportFirstIBaz)]
        [TestCase(typeof(IQux), typeof(QuxImplementation1), DiscoveredDependency.ImportFirstIQuxIQuxFactory, TestName = DiscoveredDependency.ImportFirstIQuxIQuxFactory)]
        [TestCase(typeof(BazBase), typeof(BazInheritor1), DiscoveredDependency.ImportFirstBazBase, TestName = DiscoveredDependency.ImportFirstBazBase)]
        [TestCase(typeof(QuxBase), typeof(QuxInheritor1), DiscoveredDependency.ImportFirstQuxBaseQuxFactoryBase, TestName = DiscoveredDependency.ImportFirstQuxBaseQuxFactoryBase)]
        public void GivenMultipleImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(NamedFooImplementation), DiscoveredDependency.ImportFirstIFooNamed, TestName = DiscoveredDependency.ImportFirstIFooNamed)]
        [TestCase(typeof(IBar), typeof(NamedBarImplementation), DiscoveredDependency.ImportFirstIBarIBarFactoryNamed, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryNamed)]
        [TestCase(typeof(FooBase), typeof(NamedFooInheritor), DiscoveredDependency.ImportFirstFooBaseNamed, TestName = DiscoveredDependency.ImportFirstFooBaseNamed)]
        [TestCase(typeof(BarBase), typeof(NamedBarInheritor), DiscoveredDependency.ImportFirstBarBaseBarFactoryBaseNamed, TestName = DiscoveredDependency.ImportFirstBarBaseBarFactoryBaseNamed)]
        public void GivenASingleNamedImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IBaz), typeof(NamedBazImplementation1), DiscoveredDependency.ImportFirstIBazNamed, TestName = DiscoveredDependency.ImportFirstIBazNamed)]
        [TestCase(typeof(IQux), typeof(NamedQuxImplementation1), DiscoveredDependency.ImportFirstIQuxIQuxFactoryNamed, TestName = DiscoveredDependency.ImportFirstIQuxIQuxFactoryNamed)]
        [TestCase(typeof(BazBase), typeof(NamedBazInheritor1), DiscoveredDependency.ImportFirstBazBaseNamed, TestName = DiscoveredDependency.ImportFirstBazBaseNamed)]
        [TestCase(typeof(QuxBase), typeof(NamedQuxInheritor1), DiscoveredDependency.ImportFirstQuxBaseQuxFactoryBaseNamed, TestName = DiscoveredDependency.ImportFirstQuxBaseQuxFactoryBaseNamed)]
        public void GivenMultipleNamedImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }
    }
}
