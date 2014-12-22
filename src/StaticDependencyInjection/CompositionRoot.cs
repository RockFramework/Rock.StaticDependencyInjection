using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rock.StaticDependencyInjection
{
    internal partial class CompositionRoot
    {
        public override void Bootstrap()
        {
            // TODO: Add calls to the various Import methods.
            // Rock.StaticDependencyInjection: BEGIN EXAMPLE
            // You can import a single instance of the specified type.
            ImportSingle<IFoo>(foo => Foo.Current = foo);

            // You can also import from named exports.
            //ImportSingle<IFoo>(foo => Console.WriteLine("Named Foo: {0}", foo), "NamedFoo");

            // If you don't care about an absolute winner in the Priority game, use ImportFirst.
            // The fallback sorting mechanism will be used, which is, at the moment, according
            // to a type's AssemblyQualifiedName.
            //ImportFirst<IFoo>(foo => Foo.Current = foo);

            // Or you can import a single instance via a factory that creates the instance.
            ImportSingle<IBar, IBarFactory>(bar => Bar.Current = bar, factory => factory.GetBar());

            // ImportFirst works with factories as well.
            //ImportFirst<IBar, IBarFactory>(bar => Bar.Current = bar, factory => factory.GetBar());

            // You can import multiple instances of a specific type.
            ImportMultiple<IBaz>(bazes => BazCollection.Current = bazes);

            // Or you can import multiple instances via a factory.
            ImportMultiple<IQux, IQuxFactory>(quxes => QuxCollection.Current = quxes, factory => factory.GetQux());
            // Rock.StaticDependencyInjection: END EXAMPLE
        }

        protected override ExportInfo GetExportInfo(Type type)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(ExportAttribute)) as ExportAttribute;

            if (attribute == null)
            {
                return base.GetExportInfo(type);
            }

            return
                new ExportInfo(type, attribute.Priority)
                {
                    Disabled = attribute.Disabled,
                    Name = attribute.Name
                };
        }

        protected override IEnumerable<ExportInfo> GetExportInfos(
            IEnumerable<CustomAttributeData> assemblyAttributes)
        {
            return
                assemblyAttributes.AsAttributeType<ExportExternalAttribute>()
                    .Where(attribute => attribute.ClassType.IsClass)
                    .Select(attribute =>
                        new ExportInfo(attribute.ClassType, attribute.Priority)
                        {
                            Disabled = attribute.Disabled,
                            Name = attribute.Name
                        });
        }
        // Rock.StaticDependencyInjection: BEGIN EXAMPLE
        protected override ImportOptions GetDefaultImportOptions()
        {
            return new ImportOptions
            {
                AllowNonPublicClasses = true,
                PreferTTargetType = true,
                IncludeNamedExportsFromUnnamedImports = true
            };
        }
        // Rock.StaticDependencyInjection: END EXAMPLE
    }
}