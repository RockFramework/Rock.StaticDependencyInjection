using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class ImportMultipleTests
    {
        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(FooImplementation) }, ServiceLocator.ImportMultipleIFoo, TestName = ServiceLocator.ImportMultipleIFoo)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(BarImplementation) }, ServiceLocator.ImportMultipleIBarIBarFactory, TestName = ServiceLocator.ImportMultipleIBarIBarFactory)]
        [TestCase(typeof(IEnumerable<FooBase>), new[] { typeof(FooInheritor) }, ServiceLocator.ImportMultipleFooBase, TestName = ServiceLocator.ImportMultipleFooBase)]
        [TestCase(typeof(IEnumerable<BarBase>), new[] { typeof(BarInheritor) }, ServiceLocator.ImportMultipleBarBaseBarFactoryBase, TestName = ServiceLocator.ImportMultipleBarBaseBarFactoryBase)]
        public void GivenASingleImplementationForTheAbstraction_ThenThatImplementationIsUsed(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        [TestCase(typeof(IEnumerable<IBaz>), new[] { typeof(BazImplementation1), typeof(BazImplementation2) }, ServiceLocator.ImportMultipleIBaz, TestName = ServiceLocator.ImportMultipleIBaz)]
        [TestCase(typeof(IEnumerable<IQux>), new[] { typeof(QuxImplementation1), typeof(QuxImplementation2) }, ServiceLocator.ImportMultipleIQuxIQuxFactory, TestName = ServiceLocator.ImportMultipleIQuxIQuxFactory)]
        [TestCase(typeof(IEnumerable<BazBase>), new[] { typeof(BazInheritor1), typeof(BazInheritor2) }, ServiceLocator.ImportMultipleBazBase, TestName = ServiceLocator.ImportMultipleBazBase)]
        [TestCase(typeof(IEnumerable<QuxBase>), new[] { typeof(QuxInheritor1), typeof(QuxInheritor2) }, ServiceLocator.ImportMultipleQuxBaseQuxFactoryBase, TestName = ServiceLocator.ImportMultipleQuxBaseQuxFactoryBase)]
        public void GivenMultipleImplementationsForTheAbstraction_ThenEachImplementationIsUsedAlphabetically(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        private static void RunTest(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            var registeredInstances =
                ((IEnumerable)ServiceLocator.Get(serviceAbstractionType, serviceName)).Cast<object>().ToList();
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
