using System;

namespace Rock.StaticDependencyInjection.Tests
{
    #region Implicit Exports

    public class FooImplementation : IFoo
    {
    }

    public class FooInheritor : FooBase
    {
    }

    public class BarImplementation : IBar
    {
        private readonly int _value;

        public BarImplementation(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class BarInheritor : BarBase
    {
        private readonly int _value;

        public BarInheritor(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class BarFactoryImplementation : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarImplementation(123);
        }
    }

    public class BarFactoryInheritor : BarFactoryBase
    {
        public override BarBase GetBar()
        {
            return new BarInheritor(123);
        }
    }

    public class BazImplementation1 : IBaz
    {
    }

    public class BazImplementation2 : IBaz
    {
    }

    public class BazInheritor1 : BazBase
    {
    }

    public class BazInheritor2 : BazBase
    {
    }

    public class QuxImplementation1 : IQux
    {
        private readonly int _value;

        public QuxImplementation1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class QuxImplementation2 : IQux
    {
        private readonly int _value;

        public QuxImplementation2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class QuxInheritor1 : QuxBase
    {
        private readonly int _value;

        public QuxInheritor1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class QuxInheritor2 : QuxBase
    {
        private readonly int _value;

        public QuxInheritor2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class QuxFactoryImplementation1 : IQuxFactory
    {
        public IQux GetQux()
        {
            return new QuxImplementation1(123);
        }
    }

    public class QuxFactoryImplementation2 : IQuxFactory
    {
        public IQux GetQux()
        {
            return new QuxImplementation2(123);
        }
    }

    public class QuxFactoryInheritor1 : QuxFactoryBase
    {
        public override QuxBase GetQux()
        {
            return new QuxInheritor1(123);
        }
    }

    public class QuxFactoryInheritor2 : QuxFactoryBase
    {
        public override QuxBase GetQux()
        {
            return new QuxInheritor2(123);
        }
    }

    #endregion

    #region Named Exports

    [Export(Name = "MyName")]
    public class NamedFooImplementation : IFoo
    {
    }

    [Export(Name = "MyName")]
    public class NamedFooInheritor : FooBase
    {
    }

    public class NamedBarImplementation : IBar
    {
        private readonly int _value;

        public NamedBarImplementation(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class NamedBarInheritor : BarBase
    {
        private readonly int _value;

        public NamedBarInheritor(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = "MyName")]
    public class NamedBarFactoryImplementation : IBarFactory
    {
        public IBar GetBar()
        {
            return new NamedBarImplementation(123);
        }
    }

    [Export(Name = "MyName")]
    public class NamedBarFactoryInheritor : BarFactoryBase
    {
        public override BarBase GetBar()
        {
            return new NamedBarInheritor(123);
        }
    }

    [Export(Name = "MyName")]
    public class NamedBazImplementation1 : IBaz
    {
    }

    [Export(Name = "MyName")]
    public class NamedBazImplementation2 : IBaz
    {
    }

    [Export(Name = "MyName")]
    public class NamedBazInheritor1 : BazBase
    {
    }

    [Export(Name = "MyName")]
    public class NamedBazInheritor2 : BazBase
    {
    }

    public class NamedQuxImplementation1 : IQux
    {
        private readonly int _value;

        public NamedQuxImplementation1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class NamedQuxImplementation2 : IQux
    {
        private readonly int _value;

        public NamedQuxImplementation2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class NamedQuxInheritor1 : QuxBase
    {
        private readonly int _value;

        public NamedQuxInheritor1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class NamedQuxInheritor2 : QuxBase
    {
        private readonly int _value;

        public NamedQuxInheritor2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = "MyName")]
    public class NamedQuxFactoryImplementation1 : IQuxFactory
    {
        public IQux GetQux()
        {
            return new NamedQuxImplementation1(123);
        }
    }

    [Export(Name = "MyName")]
    public class NamedQuxFactoryImplementation2 : IQuxFactory
    {
        public IQux GetQux()
        {
            return new NamedQuxImplementation2(123);
        }
    }

    [Export(Name = "MyName")]
    public class NamedQuxFactoryInheritor1 : QuxFactoryBase
    {
        public override QuxBase GetQux()
        {
            return new NamedQuxInheritor1(123);
        }
    }

    [Export(Name = "MyName")]
    public class NamedQuxFactoryInheritor2 : QuxFactoryBase
    {
        public override QuxBase GetQux()
        {
            return new NamedQuxInheritor2(123);
        }
    }

#endregion

    #region AllowNonPublicClasses Exports

    [Export(Name = DiscoveredDependency.AllowNonPublicClasses)]
    public class PublicFoo : IFoo
    {
    }

    [Export(Name = DiscoveredDependency.AllowNonPublicClasses)]
    internal class NonPublicFoo : IFoo
    {
    }

    public class PublicBar : IBar
    {
        private readonly int _value;

        public PublicBar(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    internal class NonPublicBar : IBar
    {
        private readonly int _value;

        public NonPublicBar(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = DiscoveredDependency.AllowNonPublicClasses)]
    public class PublicBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new PublicBar(123);
        }
    }

    [Export(Name = DiscoveredDependency.AllowNonPublicClasses)]
    internal class NonPublicBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new NonPublicBar(456);
        }
    }

    #endregion

    #region PreferTTargetType Exports

    [Export(Name = DiscoveredDependency.PreferTTargetType)]
    public class TargetTypeBar : IBar
    {
    }

    [Export(Name = DiscoveredDependency.PreferTTargetType)]
    public class FactoryTypeBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new FactoryTypeBar(123);
        }

        internal class FactoryTypeBar : IBar
        {
            private readonly int _value;

            public FactoryTypeBar(int value)
            {
                _value = value;
            }

            public int Value
            {
                get { return _value; }
            }
        }
    }

    #endregion

    #region IncludeTypesFromThisAssembly Exports

    [Export(Name = DiscoveredDependency.IncludeTypesFromThisAssembly)]
    public class XyzFoo : IFoo
    {
    }

    public class XyzBar : IBar
    {
        private readonly int _value;

        public XyzBar(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = DiscoveredDependency.IncludeTypesFromThisAssembly)]
    public class XyzBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new XyzBar(123);
        }
    }

    #endregion

    #region ExportComparer Exports

    [Export(Name = DiscoveredDependency.ExportComparer)]
    public class FooExportComparer1 : IFoo
    {
    }

    [Export(Name = DiscoveredDependency.ExportComparer)]
    public class FooExportComparer2 : IFoo
    {
    }

    [Export(Name = DiscoveredDependency.ExportComparer)]
    public class FooExportComparer3 : IFoo
    {
    }

    public class BarExportComparer1 : IBar
    {
        private readonly int _value;

        public BarExportComparer1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class BarExportComparer2 : IBar
    {
        private readonly int _value;

        public BarExportComparer2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class BarExportComparer3 : IBar
    {
        private readonly int _value;

        public BarExportComparer3(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = DiscoveredDependency.ExportComparer)]
    public class BarFactoryExportComparer1 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarExportComparer1(123);
        }
    }

    [Export(Name = DiscoveredDependency.ExportComparer)]
    public class BarFactoryExportComparer2 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarExportComparer2(123);
        }
    }

    [Export(Name = DiscoveredDependency.ExportComparer)]
    public class BarFactoryExportComparer3 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarExportComparer3(123);
        }
    }

    #endregion

    #region SingleHighestPriority Exports

    [Export(-30, Name = DiscoveredDependency.SingleHighestPriority)]
    public class FooSingleHighestPriority1 : IFoo
    {
    }

    [Export(-20, Name = DiscoveredDependency.SingleHighestPriority)]
    public class FooSingleHighestPriority2 : IFoo
    {
    }

    [Export(-10, Name = DiscoveredDependency.SingleHighestPriority)]
    public class FooSingleHighestPriority3 : IFoo
    {
    }

    public class BarSingleHighestPriority1 : IBar
    {
        private readonly int _value;

        public BarSingleHighestPriority1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class BarSingleHighestPriority2 : IBar
    {
        private readonly int _value;

        public BarSingleHighestPriority2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class BarSingleHighestPriority3 : IBar
    {
        private readonly int _value;

        public BarSingleHighestPriority3(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(-30, Name = DiscoveredDependency.SingleHighestPriority)]
    public class BarFactorySingleHighestPriority1 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarSingleHighestPriority1(123);
        }
    }

    [Export(-20, Name = DiscoveredDependency.SingleHighestPriority)]
    public class BarFactorySingleHighestPriority2 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarSingleHighestPriority2(123);
        }
    }

    [Export(-10, Name = DiscoveredDependency.SingleHighestPriority)]
    public class BarFactorySingleHighestPriority3 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarSingleHighestPriority3(123);
        }
    }

    #endregion

    #region MultipleHighestPriority Exports

    [Export(-100, Name = DiscoveredDependency.MultipleHighestPriority)]
    public class FooMultipleHighestPriority1 : IFoo
    {
    }

    [Export(-100, Name = DiscoveredDependency.MultipleHighestPriority)]
    public class FooMultipleHighestPriority2 : IFoo
    {
    }

    public class BarMultipleHighestPriority1 : IBar
    {
        private readonly int _value;

        public BarMultipleHighestPriority1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class BarMultipleHighestPriority2 : IBar
    {
        private readonly int _value;

        public BarMultipleHighestPriority2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(-100, Name = DiscoveredDependency.MultipleHighestPriority)]
    public class BarFactoryMultipleHighestPriority1 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarMultipleHighestPriority1(123);
        }
    }

    [Export(-100, Name = DiscoveredDependency.MultipleHighestPriority)]
    public class BarFactoryMultipleHighestPriority2 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarMultipleHighestPriority2(123);
        }
    }

    #endregion

    #region DuplicateExport Exports

    [Export(-300, Name = DiscoveredDependency.DuplicateExport)]
    [Export(-200, Name = DiscoveredDependency.DuplicateExport)]
    [Export(-200, Name = DiscoveredDependency.DuplicateExport)]
    public class DuplicateExportFoo : IFoo
    {
    }

    public class DuplicateExportBar : IBar
    {
        private readonly int _value;

        public DuplicateExportBar(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(-300, Name = DiscoveredDependency.DuplicateExport)]
    [Export(-200, Name = DiscoveredDependency.DuplicateExport)]
    [Export(-200, Name = DiscoveredDependency.DuplicateExport)]
    public class DuplicateExportBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new DuplicateExportBar(123);
        }
    }

    #endregion

    #region Non-Default Constructor Exports

    [Export(-400, Name = DiscoveredDependency.NonDefaultConstructor)]
    public class NonDefaultConstructorFoo : IFoo
    {
        private readonly int _value;

        public NonDefaultConstructorFoo(int value = 123)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class NonDefaultConstructorBar : IBar
    {
        private readonly int _value;

        public NonDefaultConstructorBar(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(-400, Name = DiscoveredDependency.NonDefaultConstructor)]
    public class NonDefaultConstructorBarFactory : IBarFactory
    {
        private readonly int _value;

        public NonDefaultConstructorBarFactory(int value = 123)
        {
            _value = value;
        }

        public IBar GetBar()
        {
            return new NonDefaultConstructorBar(_value);
        }
    }

    #endregion

    #region Dependencies That Throw During Construction

    [Export(-500, Name = DiscoveredDependency.FooBadConstructor1)]
    internal class FooBadConstructor1 : IFoo
    {
        public FooBadConstructor1()
        {
            throw new Exception(DiscoveredDependency.FooBadConstructor1);
        }
    }

    [Export(-500, Name = DiscoveredDependency.FooBadConstructor2)]
    internal class FooBadConstructor2 : IFoo
    {
        public FooBadConstructor2()
        {
            throw new Exception(DiscoveredDependency.FooBadConstructor2);
        }
    }

    [Export(-500, Name = DiscoveredDependency.FooBadConstructor3)]
    internal class FooBadConstructor3 : IFoo
    {
        public FooBadConstructor3()
        {
            throw new Exception(DiscoveredDependency.FooBadConstructor3);
        }
    }

    [Export(-500, Name = DiscoveredDependency.BarFactoryBadConstructor1)]
    internal class BarFactoryBadConstructor1 : IBarFactory
    {
        public BarFactoryBadConstructor1()
        {
            throw new Exception(DiscoveredDependency.BarFactoryBadConstructor1);
        }

        public IBar GetBar()
        {
            return new AbcBar(123);
        }
    }

    [Export(-500, Name = DiscoveredDependency.BarFactoryBadConstructor2)]
    internal class BarFactoryBadConstructor2 : IBarFactory
    {
        public BarFactoryBadConstructor2()
        {
            throw new Exception(DiscoveredDependency.BarFactoryBadConstructor2);
        }

        public IBar GetBar()
        {
            return new AbcBar(123);
        }
    }

    [Export(-500, Name = DiscoveredDependency.BarFactoryBadConstructor3)]
    internal class BarFactoryBadConstructor3 : IBarFactory
    {
        public BarFactoryBadConstructor3()
        {
            throw new Exception(DiscoveredDependency.BarFactoryBadConstructor3);
        }

        public IBar GetBar()
        {
            return new AbcBar(123);
        }
    }

    [Export(-500, Name = DiscoveredDependency.BarFactoryBadMethod1)]
    internal class BarFactoryBadMethod1 : IBarFactory
    {
        public IBar GetBar()
        {
            throw new Exception(DiscoveredDependency.BarFactoryBadMethod1);
        }
    }

    [Export(-500, Name = DiscoveredDependency.BarFactoryBadMethod2)]
    internal class BarFactoryBadMethod2 : IBarFactory
    {
        public IBar GetBar()
        {
            throw new Exception(DiscoveredDependency.BarFactoryBadMethod2);
        }
    }

    [Export(-500, Name = DiscoveredDependency.BarFactoryBadMethod3)]
    internal class BarFactoryBadMethod3 : IBarFactory
    {
        public IBar GetBar()
        {
            throw new Exception(DiscoveredDependency.BarFactoryBadMethod3);
        }
    }

    #endregion

    #region Exports For Import Action Exceptions

    [Export(Name = DiscoveredDependency.ImportSingleImportActionExceptionIFoo)]
    internal class ImportSingleImportActionExceptionFooImplementation : IFoo
    {
    }

    internal class ImportSingleImportActionExceptionBarImplementation : IBar
    {
        private readonly int _value;

        public ImportSingleImportActionExceptionBarImplementation(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = DiscoveredDependency.ImportSingleImportActionExceptionIBarIBarFactory)]
    internal class ImportSingleImportActionExceptionBarFactoryImplementation : IBarFactory
    {
        public IBar GetBar()
        {
            return new NamedBarImplementation(123);
        }
    }

    [Export(Name = DiscoveredDependency.ImportFirstImportActionExceptionIFoo)]
    internal class ImportFirstImportActionExceptionFooImplementation : IFoo
    {
    }

    internal class ImportFirstImportActionExceptionBarImplementation : IBar
    {
        private readonly int _value;

        public ImportFirstImportActionExceptionBarImplementation(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = DiscoveredDependency.ImportFirstImportActionExceptionIBarIBarFactory)]
    internal class ImportFirstImportActionExceptionBarFactoryImplementation : IBarFactory
    {
        public IBar GetBar()
        {
            return new NamedBarImplementation(123);
        }
    }

    [Export(Name = DiscoveredDependency.ImportMultipleImportActionExceptionIBaz)]
    internal class ImportMultipleImportActionExceptionBazImplementation1 : IBaz
    {
    }

    [Export(Name = DiscoveredDependency.ImportMultipleImportActionExceptionIBaz)]
    internal class ImportMultipleImportActionExceptionBazImplementation2 : IBaz
    {
    }

    internal class ImportMultipleImportActionExceptionQuxImplementation1 : IQux
    {
        private readonly int _value;

        public ImportMultipleImportActionExceptionQuxImplementation1(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    internal class ImportMultipleImportActionExceptionQuxImplementation2 : IQux
    {
        private readonly int _value;

        public ImportMultipleImportActionExceptionQuxImplementation2(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = DiscoveredDependency.ImportMultipleImportActionExceptionIQuxIQuxFactory)]
    internal class ImportMultipleImportActionExceptionQuxFactoryImplementation1 : IQuxFactory
    {
        public IQux GetQux()
        {
            return new NamedQuxImplementation1(123);
        }
    }

    [Export(Name = DiscoveredDependency.ImportMultipleImportActionExceptionIQuxIQuxFactory)]
    internal class ImportMultipleImportActionExceptionQuxFactoryImplementation2 : IQuxFactory
    {
        public IQux GetQux()
        {
            return new NamedQuxImplementation2(123);
        }
    }

    #endregion

    #region Disabled Exports

    [Export(Disabled = true)]
    public class DisabledFoo : IFoo
    {
    }

    public class DisabledBar : IBar
    {
        private readonly int _value;

        public DisabledBar(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Disabled = true)]
    public class DisabledBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new DisabledBar(123);
        }
    }

    #endregion
}
