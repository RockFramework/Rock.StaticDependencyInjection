using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
    public class DuplicateExportTests
    {
        [TestCase(typeof(IFoo), typeof(DuplicateExportFoo), ServiceLocator.ImportSingleIFooDuplicateExport, TestName = ServiceLocator.ImportSingleIFooDuplicateExport)]
        [TestCase(typeof(IBar), typeof(DuplicateExportBar), ServiceLocator.ImportSingleIBarIBarFactoryDuplicateExport, TestName = ServiceLocator.ImportSingleIBarIBarFactoryDuplicateExport)]
        public void DuplicateExportAttributesHaveNoEffectOnImportSingle(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IFoo), typeof(DuplicateExportFoo), ServiceLocator.ImportFirstIFooDuplicateExport, TestName = ServiceLocator.ImportFirstIFooDuplicateExport)]
        [TestCase(typeof(IBar), typeof(DuplicateExportBar), ServiceLocator.ImportFirstIBarIBarFactoryDuplicateExport, TestName = ServiceLocator.ImportFirstIBarIBarFactoryDuplicateExport)]
        public void DuplicateExportAttributesHaveNoEffectOnImportFirst(Type serviceAbstractionType, Type expectedServiceType, string serviceName)
        {
            var registeredInstance = ServiceLocator.Get(serviceAbstractionType, serviceName);
            Assert.That(registeredInstance, Is.InstanceOf(expectedServiceType));
        }

        [TestCase(typeof(IEnumerable<IFoo>), new[] { typeof(DuplicateExportFoo) }, ServiceLocator.ImportMultipleIFooDuplicateExport, TestName = ServiceLocator.ImportMultipleIFooDuplicateExport)]
        [TestCase(typeof(IEnumerable<IBar>), new[] { typeof(DuplicateExportBar) }, ServiceLocator.ImportMultipleIBarIBarFactoryDuplicateExport, TestName = ServiceLocator.ImportMultipleIBarIBarFactoryDuplicateExport)]
        public void DuplicateExportAttributesHaveNoEffectOnImportMultiple(Type serviceAbstractionType, Type[] expectedServiceTypes, string serviceName)
        {
            ImportMultipleTests.RunTest(serviceAbstractionType, expectedServiceTypes, serviceName);
        }
    }
}
