using System;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class ImportFirstTests
    {
        [TestCase(typeof(IFoo), typeof(FooImplementation), ServiceLocator.ImportFirstIFoo, TestName = ServiceLocator.ImportFirstIFoo)]
        [TestCase(typeof(IBar), typeof(BarImplementation), ServiceLocator.ImportFirstIBarIBarFactory, TestName = ServiceLocator.ImportFirstIBarIBarFactory)]
        [TestCase(typeof(FooBase), typeof(FooInheritor), ServiceLocator.ImportFirstFooBase, TestName = ServiceLocator.ImportFirstFooBase)]
        [TestCase(typeof(BarBase), typeof(BarInheritor), ServiceLocator.ImportFirstBarBaseBarFactoryBase, TestName = ServiceLocator.ImportFirstBarBaseBarFactoryBase)]
        public void GivenASingleImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IBaz), typeof(BazImplementation1), ServiceLocator.ImportFirstIBaz, TestName = ServiceLocator.ImportFirstIBaz)]
        [TestCase(typeof(IQux), typeof(QuxImplementation1), ServiceLocator.ImportFirstIQuxIQuxFactory, TestName = ServiceLocator.ImportFirstIQuxIQuxFactory)]
        [TestCase(typeof(BazBase), typeof(BazInheritor1), ServiceLocator.ImportFirstBazBase, TestName = ServiceLocator.ImportFirstBazBase)]
        [TestCase(typeof(QuxBase), typeof(QuxInheritor1), ServiceLocator.ImportFirstQuxBaseQuxFactoryBase, TestName = ServiceLocator.ImportFirstQuxBaseQuxFactoryBase)]
        public void GivenMultipleImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(NamedFooImplementation), ServiceLocator.ImportFirstIFooNamed, TestName = ServiceLocator.ImportFirstIFooNamed)]
        [TestCase(typeof(IBar), typeof(NamedBarImplementation), ServiceLocator.ImportFirstIBarIBarFactoryNamed, TestName = ServiceLocator.ImportFirstIBarIBarFactoryNamed)]
        [TestCase(typeof(FooBase), typeof(NamedFooInheritor), ServiceLocator.ImportFirstFooBaseNamed, TestName = ServiceLocator.ImportFirstFooBaseNamed)]
        [TestCase(typeof(BarBase), typeof(NamedBarInheritor), ServiceLocator.ImportFirstBarBaseBarFactoryBaseNamed, TestName = ServiceLocator.ImportFirstBarBaseBarFactoryBaseNamed)]
        public void GivenASingleNamedImplementationForTheAbstraction_ThenThatSingleImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IBaz), typeof(NamedBazImplementation1), ServiceLocator.ImportFirstIBazNamed, TestName = ServiceLocator.ImportFirstIBazNamed)]
        [TestCase(typeof(IQux), typeof(NamedQuxImplementation1), ServiceLocator.ImportFirstIQuxIQuxFactoryNamed, TestName = ServiceLocator.ImportFirstIQuxIQuxFactoryNamed)]
        [TestCase(typeof(BazBase), typeof(NamedBazInheritor1), ServiceLocator.ImportFirstBazBaseNamed, TestName = ServiceLocator.ImportFirstBazBaseNamed)]
        [TestCase(typeof(QuxBase), typeof(NamedQuxInheritor1), ServiceLocator.ImportFirstQuxBaseQuxFactoryBaseNamed, TestName = ServiceLocator.ImportFirstQuxBaseQuxFactoryBaseNamed)]
        public void GivenMultipleNamedImplementationForTheAbstraction_ThenTheFirstAlphabeticalImplementationIsUsed(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }
    }
}
