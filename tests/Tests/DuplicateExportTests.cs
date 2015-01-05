using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class DuplicateExportTests
    {
        [TestCase(typeof(IFoo), typeof(DuplicateExportFoo), DiscoveredDependency.ImportSingleIFooDuplicateExport, TestName = DiscoveredDependency.ImportSingleIFooDuplicateExport)]
        [TestCase(typeof(IBar), typeof(DuplicateExportBar), DiscoveredDependency.ImportSingleIBarIBarFactoryDuplicateExport, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryDuplicateExport)]
        public void DuplicateExportAttributesHaveNoEffectOnImportSingle(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(DuplicateExportFoo), DiscoveredDependency.ImportFirstIFooDuplicateExport, TestName = DiscoveredDependency.ImportFirstIFooDuplicateExport)]
        [TestCase(typeof(IBar), typeof(DuplicateExportBar), DiscoveredDependency.ImportFirstIBarIBarFactoryDuplicateExport, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryDuplicateExport)]
        public void DuplicateExportAttributesHaveNoEffectOnImportFirst(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(DuplicateExportFoo) }, DiscoveredDependency.ImportMultipleIFooDuplicateExport, TestName = DiscoveredDependency.ImportMultipleIFooDuplicateExport)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(DuplicateExportBar) }, DiscoveredDependency.ImportMultipleIBarIBarFactoryDuplicateExport, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryDuplicateExport)]
        public void DuplicateExportAttributesHaveNoEffectOnImportMultiple(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }
    }
}
