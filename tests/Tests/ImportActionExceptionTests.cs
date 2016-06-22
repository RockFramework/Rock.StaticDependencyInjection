using System;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class ImportActionExceptionTests
    {
        [TestCase(typeof(IFoo), null, DiscoveredDependency.ImportSingleImportActionExceptionIFoo, TestName = DiscoveredDependency.ImportSingleImportActionExceptionIFoo)]
        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.ImportSingleImportActionExceptionIBarIBarFactory, TestName = DiscoveredDependency.ImportSingleImportActionExceptionIBarIBarFactory)]
        [TestCase(typeof(IFoo), null, DiscoveredDependency.ImportFirstImportActionExceptionIFoo, TestName = DiscoveredDependency.ImportFirstImportActionExceptionIFoo)]
        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.ImportFirstImportActionExceptionIBarIBarFactory, TestName = DiscoveredDependency.ImportFirstImportActionExceptionIBarIBarFactory)]
        [TestCase(typeof(IBaz), null, DiscoveredDependency.ImportMultipleImportActionExceptionIBaz, TestName = DiscoveredDependency.ImportMultipleImportActionExceptionIBaz)]
        [TestCase(typeof(IQux), typeof(IQuxFactory), DiscoveredDependency.ImportMultipleImportActionExceptionIQuxIQuxFactory, TestName = DiscoveredDependency.ImportMultipleImportActionExceptionIQuxIQuxFactory)]
        public void OnErrorIsCalledWhenImportActionThrows(Type serviceAbstractionType, Type factoryType, string importExportName)
        {
            var t = HandledErrors.Find(importExportName, serviceAbstractionType, factoryType);

            Assert.That(t.Item1, Is.EqualTo("An error occurred in the 'importAction' callback."));
            Assert.That(t.Item2.Message, Is.EqualTo(importExportName));
        } 
    }
}