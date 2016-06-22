using System;
using System.CodeDom;
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

        [TestCase(typeof(IBaz), null, DiscoveredDependency.ImportSingleIBaz, typeof(BazImplementation1), typeof(BazImplementation2), TestName = DiscoveredDependency.ImportSingleIBaz)]
        [TestCase(typeof(IQux), typeof(IQuxFactory), DiscoveredDependency.ImportSingleIQuxIQuxFactory, typeof(QuxFactoryImplementation1), typeof(QuxFactoryImplementation2), TestName = DiscoveredDependency.ImportSingleIQuxIQuxFactory)]
        [TestCase(typeof(BazBase), null, DiscoveredDependency.ImportSingleBazBase, typeof(BazInheritor1), typeof(BazInheritor2), TestName = DiscoveredDependency.ImportSingleBazBase)]
        [TestCase(typeof(QuxBase), typeof(QuxFactoryBase), DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBase, typeof(QuxFactoryInheritor1), typeof(QuxFactoryInheritor2), TestName = DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBase)]
        public void GivenMultipleImplementationsForTheAbstraction_ThenOnErrorIsCalled(Type serviceAbstractionType, Type factoryType, string serviceName, Type expectedDuplicate1, Type expectedDuplicate2)
        {
            var t = HandledErrors.Find(null, serviceAbstractionType, factoryType);

            var expectedMessage = string.Format(
                "Unable to import a single instance of type '{0}' - more than one export with " +
                "the same highest priority was discovered: '{1}', '{2}'.", serviceAbstractionType,
                expectedDuplicate1, expectedDuplicate2);

            Assert.That(t.Item1, Is.EqualTo(expectedMessage));
            Assert.That(t.Item2, Is.Null);
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

        [TestCase(typeof(IBaz), null, DiscoveredDependency.ImportSingleIBazNamed, typeof(NamedBazImplementation1), typeof(NamedBazImplementation2), TestName = DiscoveredDependency.ImportSingleIBazNamed)]
        [TestCase(typeof(IQux), typeof(IQuxFactory), DiscoveredDependency.ImportSingleIQuxIQuxFactoryNamed, typeof(NamedQuxFactoryImplementation1), typeof(NamedQuxFactoryImplementation2), TestName = DiscoveredDependency.ImportSingleIQuxIQuxFactoryNamed)]
        [TestCase(typeof(BazBase), null, DiscoveredDependency.ImportSingleBazBaseNamed, typeof(NamedBazInheritor1), typeof(NamedBazInheritor2), TestName = DiscoveredDependency.ImportSingleBazBaseNamed)]
        [TestCase(typeof(QuxBase), typeof(QuxFactoryBase), DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBaseNamed, typeof(NamedQuxFactoryInheritor1), typeof(NamedQuxFactoryInheritor2), TestName = DiscoveredDependency.ImportSingleQuxBaseQuxFactoryBaseNamed)]
        public void GivenMultipleNamedImplementationsForTheAbstraction_ThenOnErrorIsCalled(Type serviceAbstractionType, Type factoryType, string serviceName, Type expectedDuplicate1, Type expectedDuplicate2)
        {
            var t = HandledErrors.Find(DiscoveredDependency.MyName, serviceAbstractionType, factoryType);

            var expectedMessage = string.Format(
                "Unable to import a single instance of type '{0}' - more than one export with " +
                "the same highest priority was discovered: '{1}', '{2}'.", serviceAbstractionType,
                expectedDuplicate1, expectedDuplicate2);

            Assert.That(t.Item1, Is.EqualTo(expectedMessage));
            Assert.That(t.Item2, Is.Null);
        }
    }
}
