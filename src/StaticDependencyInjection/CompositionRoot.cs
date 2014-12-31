using System;
using System.Collections.Generic;
using System.Linq;

namespace Rock.StaticDependencyInjection
{
    internal partial class CompositionRoot : CompositionRootBase
    {
        public override void Bootstrap()
        {
            // This method is used to bootstrap the static dependencies of your
            // library. To do this, call the various import methods of this class:
            //  - ImportSingle
            //  - ImportFirst
            //  - ImportMultiple
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

        /// <summary>
        /// Return a collection of metadata objects that describe the export operations for a type.
        /// </summary>
        /// <param name="type">The type to get export metadata.</param>
        /// <returns>A collection of metadata objects that describe export operations.</returns>
        protected override IEnumerable<ExportInfo> GetExportInfos(Type type)
        {
            // Modify this method if your library needs to support a different
            // export mechanism (possibly a different attribute) that inspects
            // the type of a class.
            //
            // Remove this method if your library should not support any advanced
            // export mechanisms based on the type of a class.

            var attributes = Attribute.GetCustomAttributes(type, typeof(ExportAttribute));

            if (attributes.Length == 0)
            {
                return base.GetExportInfos(type);
            }

            return
                attributes.Cast<ExportAttribute>()
                .Select(attribute =>
                    new ExportInfo(type, attribute.Priority)
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