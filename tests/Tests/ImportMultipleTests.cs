using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class ImportMultipleTests
    {
        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(FooImplementation) }, DiscoveredDependency.ImportMultipleIFoo, TestName = DiscoveredDependency.ImportMultipleIFoo)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(BarImplementation) }, DiscoveredDependency.ImportMultipleIBarIBarFactory, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactory)]
        [TestCase(typeof(IEnumerable<FooBase>), new[] { typeof(FooInheritor) }, DiscoveredDependency.ImportMultipleFooBase, TestName = DiscoveredDependency.ImportMultipleFooBase)]
        [TestCase(typeof(IEnumerable<BarBase>), new[] { typeof(BarInheritor) }, DiscoveredDependency.ImportMultipleBarBaseBarFactoryBase, TestName = DiscoveredDependency.ImportMultipleBarBaseBarFactoryBase)]
        public void GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        [TestCase(typeof(IEnumerable<IBaz>), new[] { typeof(BazImplementation1), typeof(BazImplementation2) }, DiscoveredDependency.ImportMultipleIBaz, TestName = DiscoveredDependency.ImportMultipleIBaz)]
        [TestCase(typeof(IEnumerable<IQux>), new[] { typeof(QuxImplementation1), typeof(QuxImplementation2) }, DiscoveredDependency.ImportMultipleIQuxIQuxFactory, TestName = DiscoveredDependency.ImportMultipleIQuxIQuxFactory)]
        [TestCase(typeof(IEnumerable<BazBase>), new[] { typeof(BazInheritor1), typeof(BazInheritor2) }, DiscoveredDependency.ImportMultipleBazBase, TestName = DiscoveredDependency.ImportMultipleBazBase)]
        [TestCase(typeof(IEnumerable<QuxBase>), new[] { typeof(QuxInheritor1), typeof(QuxInheritor2) }, DiscoveredDependency.ImportMultipleQuxBaseQuxFactoryBase, TestName = DiscoveredDependency.ImportMultipleQuxBaseQuxFactoryBase)]
        public void GivenMultipleImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(NamedFooImplementation) }, DiscoveredDependency.ImportMultipleIFooNamed, TestName = DiscoveredDependency.ImportMultipleIFooNamed)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(NamedBarImplementation) }, DiscoveredDependency.ImportMultipleIBarIBarFactoryNamed, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryNamed)]
        [TestCase(typeof(IEnumerable<FooBase>), new[] { typeof(NamedFooInheritor) }, DiscoveredDependency.ImportMultipleFooBaseNamed, TestName = DiscoveredDependency.ImportMultipleFooBaseNamed)]
        [TestCase(typeof(IEnumerable<BarBase>), new[] { typeof(NamedBarInheritor) }, DiscoveredDependency.ImportMultipleBarBaseBarFactoryBaseNamed, TestName = DiscoveredDependency.ImportMultipleBarBaseBarFactoryBaseNamed)]
        public void GivenASingleNamedImplementationForTheAbstraction_ThenThatImplementationIsUsed(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        [TestCase(typeof(IEnumerable<IBaz>), new[] { typeof(NamedBazImplementation1), typeof(NamedBazImplementation2) }, DiscoveredDependency.ImportMultipleIBazNamed, TestName = DiscoveredDependency.ImportMultipleIBazNamed)]
        [TestCase(typeof(IEnumerable<IQux>), new[] { typeof(NamedQuxImplementation1), typeof(NamedQuxImplementation2) }, DiscoveredDependency.ImportMultipleIQuxIQuxFactoryNamed, TestName = DiscoveredDependency.ImportMultipleIQuxIQuxFactoryNamed)]
        [TestCase(typeof(IEnumerable<BazBase>), new[] { typeof(NamedBazInheritor1), typeof(NamedBazInheritor2) }, DiscoveredDependency.ImportMultipleBazBaseNamed, TestName = DiscoveredDependency.ImportMultipleBazBaseNamed)]
        [TestCase(typeof(IEnumerable<QuxBase>), new[] { typeof(NamedQuxInheritor1), typeof(NamedQuxInheritor2) }, DiscoveredDependency.ImportMultipleQuxBaseQuxFactoryBaseNamed, TestName = DiscoveredDependency.ImportMultipleQuxBaseQuxFactoryBaseNamed)]
        public void GivenMultipleNamedImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        public static void RunTest(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            var registeredInstances =
                ((IEnumerable)DiscoveredDependency.Find(serviceAbstractionType, serviceName)).Cast<object>().ToList();
            Assert.That(registeredInstances.Count, Is.EqualTo(expectedServiceTypes.Length));

            var instancesEnumerator = registeredInstances.GetEnumerator();
            var expectedEnumerator = ((IList<Type>)expectedServiceTypes).GetEnumerator();

            while (true)
            {
                var expectedHasNext = expectedEnumerator.MoveNext();

                Assert.That(instancesEnumerator.MoveNext(), Is.EqualTo(expectedHasNext));

                if (!expectedHasNext)
                {
                    return;
                }

                Assert.That(instancesEnumerator.Current, Is.Not.Null);
                Assert.That(instancesEnumerator.Current, Is.InstanceOf(expectedEnumerator.Current));
            }
        }
    }
}
