using System;

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
        /// Return a metadata object that describes the export operation for a type.
        /// </summary>
        /// <param name="type">The type to get export metadata.</param>
        /// <returns>A metadata object that describes an export operation.</returns>
        protected override ExportInfo GetExportInfo(Type type)
        {
            // Modify this method if your library needs to support a different
            // export mechanism (possibly a different attribute) that inspects
            // the type of a class.
            //
            // Remove this method if your library should not support any advanced
            // export mechanisms based on the type of a class.

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