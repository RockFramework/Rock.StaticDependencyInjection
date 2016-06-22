using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Rock.StaticDependencyInjection.Tests
{
    public class BadDependencyTests
    {
        [TestCase(typeof(IFoo), DiscoveredDependency.FooBadConstructor1, DiscoveredDependency.ImportSingleIFooBadDependency, TestName = DiscoveredDependency.ImportSingleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor1)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadConstructor1, DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor1)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadMethod1, DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod1)]
        [TestCase(typeof(IFoo), DiscoveredDependency.FooBadConstructor2, DiscoveredDependency.ImportFirstIFooBadDependency, TestName = DiscoveredDependency.ImportFirstIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor2)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadConstructor2, DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor2)]
        [TestCase(typeof(IBar), DiscoveredDependency.BarFactoryBadMethod2, DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod2)]
        [TestCase(typeof(IEnumerable<IFoo>), DiscoveredDependency.FooBadConstructor3, DiscoveredDependency.ImportMultipleIFooBadDependency, TestName = DiscoveredDependency.ImportMultipleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor3)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.BarFactoryBadConstructor3, DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor3)]
        [TestCase(typeof(IEnumerable<IBar>), DiscoveredDependency.BarFactoryBadMethod3, DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod3)]
        public void DependenciesThatThrowExceptionsDuringCreationAreIgnored(Type serviceAbstractionType, string importExportName, string serviceName)
        {
            var registeredInstance = DiscoveredDependency.Find(serviceAbstractionType, serviceName + ":" + importExportName);

            if (registeredInstance is IEnumerable)
            {
                Assert.That(registeredInstance, Is.Empty);
            }
            else
            {
                Assert.That(registeredInstance, Is.Null);
            }
        }

        [TestCase(typeof(IFoo), null, DiscoveredDependency.FooBadConstructor1, typeof(FooBadConstructor1), TestName = DiscoveredDependency.ImportSingleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor1)]
        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.BarFactoryBadConstructor1, typeof(BarFactoryBadConstructor1), TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor1)]
        [TestCase(typeof(IFoo), null, DiscoveredDependency.FooBadConstructor2, typeof(FooBadConstructor2), TestName = DiscoveredDependency.ImportFirstIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor2)]
        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.BarFactoryBadConstructor2, typeof(BarFactoryBadConstructor2), TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor2)]
        [TestCase(typeof(IFoo), null, DiscoveredDependency.FooBadConstructor3, typeof(FooBadConstructor3), TestName = DiscoveredDependency.ImportMultipleIFooBadDependency + ":" + DiscoveredDependency.FooBadConstructor3)]
        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.BarFactoryBadConstructor3, typeof(BarFactoryBadConstructor3), TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadConstructor3)]
        public void OnErrorIsCalledWhenTargetOrFactoryInstantiationThrows(Type serviceAbstractionType, Type factoryType, string importExportName, Type expectedBadType)
        {
            var t = HandledErrors.Find(importExportName, serviceAbstractionType, factoryType);

            Assert.That(t.Item1, Is.EqualTo(string.Format("An error occurred while creating an instance of the '{0}' type.", expectedBadType)));
            Assert.That(t.Item2.Message, Is.EqualTo(importExportName));
        }

        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.BarFactoryBadMethod1, TestName = DiscoveredDependency.ImportSingleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod1)]
        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.BarFactoryBadMethod2, TestName = DiscoveredDependency.ImportFirstIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod2)]
        [TestCase(typeof(IBar), typeof(IBarFactory), DiscoveredDependency.BarFactoryBadMethod3, TestName = DiscoveredDependency.ImportMultipleIBarIBarFactoryBadDependency + ":" + DiscoveredDependency.BarFactoryBadMethod3)]
        public void OnErrorIsCalledWhenTheGetTargetCallbackThrows(Type serviceAbstractionType, Type factoryType, string importExportName)
        {
            var t = HandledErrors.Find(importExportName, serviceAbstractionType, factoryType);

            Assert.That(t.Item1, Is.EqualTo("An error occurred in the 'getTarget' callback."));
            Assert.That(t.Item2.Message, Is.EqualTo(importExportName));
        }
    }
}