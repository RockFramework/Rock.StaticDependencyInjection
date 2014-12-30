using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class PriorityTests
    {
        [TestCase(typeof(IFoo), typeof(FooSingleHighestPriority3), ServiceLocator.ImportSingleIFooSingleHighestPriority, TestName = ServiceLocator.ImportSingleIFooSingleHighestPriority)]
        [TestCase(typeof(IBar), typeof(BarSingleHighestPriority3), ServiceLocator.ImportSingleIBarIBarFactorySingleHighestPriority, TestName = ServiceLocator.ImportSingleIBarIBarFactorySingleHighestPriority)]
        public void GivenASingleHighestPriorityExport_ThenImportSingleChoosesThatExport(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(FooSingleHighestPriority3), ServiceLocator.ImportFirstIFooSingleHighestPriority, TestName = ServiceLocator.ImportFirstIFooSingleHighestPriority)]
        [TestCase(typeof(IBar), typeof(BarSingleHighestPriority3), ServiceLocator.ImportFirstIBarIBarFactorySingleHighestPriority, TestName = ServiceLocator.ImportFirstIBarIBarFactorySingleHighestPriority)]
        public void GivenASingleHighestPriorityExport_ThenImportFirstChoosesThatExport(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(FooSingleHighestPriority3), typeof(FooSingleHighestPriority2), typeof(FooSingleHighestPriority1) }, ServiceLocator.ImportMultipleIFooSingleHighestPriority, TestName = ServiceLocator.ImportMultipleIFooSingleHighestPriority)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(BarSingleHighestPriority3), typeof(BarSingleHighestPriority2), typeof(BarSingleHighestPriority1) }, ServiceLocator.ImportMultipleIBarIBarFactorySingleHighestPriority, TestName = ServiceLocator.ImportMultipleIBarIBarFactorySingleHighestPriority)]
        public void GivenASingleHighestPriorityExport_ThenImportMultipleOrdersExportsByPriority(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }

        [TestCase(typeof(IFoo), ServiceLocator.ImportSingleIFooMultipleHighestPriority, TestName = ServiceLocator.ImportSingleIFooMultipleHighestPriority)]
        [TestCase(typeof(IBar), ServiceLocator.ImportSingleIBarIBarFactoryMultipleHighestPriority, TestName = ServiceLocator.ImportSingleIBarIBarFactoryMultipleHighestPriority)]
        public void GivenMultipleHighestPriorityExports_ThenImportSingleChoosesNoExports(Type serviceAbstractionType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.Null);
        }

        [TestCase(typeof(IFoo), typeof(FooMultipleHighestPriority1), ServiceLocator.ImportFirstIFooMultipleHighestPriority, TestName = ServiceLocator.ImportFirstIFooMultipleHighestPriority)]
        [TestCase(typeof(IBar), typeof(BarMultipleHighestPriority1), ServiceLocator.ImportFirstIBarIBarFactoryMultipleHighestPriority, TestName = ServiceLocator.ImportFirstIBarIBarFactoryMultipleHighestPriority)]
        public void GivenMultipleHighestPriorityExports_ThenImportFirstChoosesTheFirstOneAccordingToTheSecondarySortingMechanism(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(FooMultipleHighestPriority1), typeof(FooMultipleHighestPriority2) }, ServiceLocator.ImportMultipleIFooMultipleHighestPriority, TestName = ServiceLocator.ImportMultipleIFooMultipleHighestPriority)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(BarMultipleHighestPriority1), typeof(BarMultipleHighestPriority2) }, ServiceLocator.ImportMultipleIBarIBarFactoryMultipleHighestPriority, TestName = ServiceLocator.ImportMultipleIBarIBarFactoryMultipleHighestPriority)]
        public void GivenMultipleHighestPriorityExports_ThenImportMultipleOrdersExportsAccordingToTheSecondarySortingMechanism(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }
    }
}
