using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class PriorityTests
    {
        [TestCase(typeof(IFoo), typeof(FooSingleHighestPriority3), DiscoveredDependency.ImportSingleIFooSingleHighestPriority, TestName = DiscoveredDependency.ImportSingleIFooSingleHighestPriority)]
        [TestCase(typeof(IBar), typeof(BarSingleHighestPriority3), DiscoveredDependency.ImportSingleIBarIBarFactorySingleHighestPriority, TestName = DiscoveredDependency.ImportSingleIBarIBarFactorySingleHighestPriority)]
        public void GivenASingleHighestPriorityExport_ThenImportSingleChoosesThatExport(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(FooSingleHighestPriority3), DiscoveredDependency.ImportFirstIFooSingleHighestPriority, TestName = DiscoveredDependency.ImportFirstIFooSingleHighestPriority)]
        [TestCase(typeof(IBar), typeof(BarSingleHighestPriority3), DiscoveredDependency.ImportFirstIBarIBarFactorySingleHighestPriority, TestName = DiscoveredDependency.ImportFirstIBarIBarFactorySingleHighestPriority)]
        public void GivenASingleHighestPriorityExport_ThenImportFirstChoosesThatExport(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(FooSingleHighestPriority3), typeof(FooSingleHighestPriority2), typeof(FooSingleHighestPriority1) }, DiscoveredDependency.ImportMultipleIFooSingleHighestPriority, TestName = DiscoveredDependency.ImportMultipleIFooSingleHighestPriority)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(BarSingleHighestPriority3), typeof(BarSingleHighestPriority2), typeof(BarSingleHighestPriority1) }, DiscoveredDependency.ImportMultipleIBarIBarFactorySingleHighestPriority, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactorySingleHighestPriority)]
        public void GivenASingleHighestPriorityExport_ThenImportMultipleOrdersExportsByPriority(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        [TestCase(typeof(IFoo), DiscoveredDependency.ImportSingleIFooMultipleHighestPriority, TestName = DiscoveredDependency.ImportSingleIFooMultipleHighestPriority)]
        [TestCase(typeof(IBar), DiscoveredDependency.ImportSingleIBarIBarFactoryMultipleHighestPriority, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryMultipleHighestPriority)]
        public void GivenMultipleHighestPriorityExports_ThenImportSingleChoosesNoExports(Type serviceAbstractionType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.Null);
        }

        [TestCase(typeof(IFoo), typeof(FooMultipleHighestPriority1), DiscoveredDependency.ImportFirstIFooMultipleHighestPriority, TestName = DiscoveredDependency.ImportFirstIFooMultipleHighestPriority)]
        [TestCase(typeof(IBar), typeof(BarMultipleHighestPriority1), DiscoveredDependency.ImportFirstIBarIBarFactoryMultipleHighestPriority, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryMultipleHighestPriority)]
        public void GivenMultipleHighestPriorityExports_ThenImportFirstChoosesTheFirstOneAccordingToTheSecondarySortingMechanism(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(FooMultipleHighestPriority1), typeof(FooMultipleHighestPriority2) }, DiscoveredDependency.ImportMultipleIFooMultipleHighestPriority, TestName = DiscoveredDependency.ImportMultipleIFooMultipleHighestPriority)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(BarMultipleHighestPriority1), typeof(BarMultipleHighestPriority2) }, DiscoveredDependency.ImportMultipleIBarIBarFactoryMultipleHighestPriority, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryMultipleHighestPriority)]
        public void GivenMultipleHighestPriorityExports_ThenImportMultipleOrdersExportsAccordingToTheSecondarySortingMechanism(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }
    }
}
