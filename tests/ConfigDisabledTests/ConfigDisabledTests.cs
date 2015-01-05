using System;
using NUnit.Framework;
using Rock.StaticDependencyInjection.Tests;

namespace Rock.StaticDependencyInjection.ConfigDisabledTests
{
    public class ConfigDisabledTests
    {
        [TestCase(typeof(IFoo), DiscoveredDependency.ImportSingleIFoo)]
        public void WhenDisabledViaConfigStaticDependencyInjectionIsSkipped(Type serviceAbstractionType, string serviceName)
        {
            // To make this test fail, change app.config by either:
            //   - Removing the "Rock.StaticDependencyInjection.Enabled" app setting.
            //   - Changing the value of the "Rock.StaticDependencyInjection.Enabled"
            //     app setting to anything other than "false" (case insensitive).

            var registeredInstance = DiscoveredDependency.Find(typeof(IFoo), serviceName);
            Assert.That(registeredInstance, Is.Null);
        }
    }

    public class UndiscoveredFoo : IFoo
    {
    }
}
