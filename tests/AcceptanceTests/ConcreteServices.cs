using Rock.StaticDependencyInjection.AcceptanceTests.Library;
using Rock.StaticDependencyInjection.AcceptanceTests.Library.Rock.StaticDependencyInjection;

namespace Rock.StaticDependencyInjection.AcceptanceTests
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

    [Export(Name = ServiceLocator.AllowNonPublicClasses)]
    public class PublicFoo : IFoo
    {
    }

    [Export(Name = ServiceLocator.AllowNonPublicClasses)]
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

    [Export(Name = ServiceLocator.AllowNonPublicClasses)]
    public class PublicBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new PublicBar(123);
        }
    }

    [Export(Name = ServiceLocator.AllowNonPublicClasses)]
    internal class NonPublicBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new NonPublicBar(456);
        }
    }

    #endregion

    #region PreferTTargetType Exports

    [Export(Name = ServiceLocator.PreferTTargetType)]
    public class TargetTypeBar : IBar
    {
    }

    [Export(Name = ServiceLocator.PreferTTargetType)]
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

    [Export(Name = ServiceLocator.IncludeTypesFromThisAssembly)]
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

    [Export(Name = ServiceLocator.IncludeTypesFromThisAssembly)]
    public class XyzBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new XyzBar(123);
        }
    }

    #endregion

    #region ExportComparer Exports

    [Export(Name = ServiceLocator.ExportComparer)]
    public class FooExportComparer1 : IFoo
    {
    }

    [Export(Name = ServiceLocator.ExportComparer)]
    public class FooExportComparer2 : IFoo
    {
    }

    [Export(Name = ServiceLocator.ExportComparer)]
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

    [Export(Name = ServiceLocator.ExportComparer)]
    public class BarFactoryExportComparer1 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarExportComparer1(123);
        }
    }

    [Export(Name = ServiceLocator.ExportComparer)]
    public class BarFactoryExportComparer2 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarExportComparer2(123);
        }
    }

    [Export(Name = ServiceLocator.ExportComparer)]
    public class BarFactoryExportComparer3 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarExportComparer3(123);
        }
    }

    #endregion

    #region SingleHighestPriority Exports

    [Export(-30, Name = ServiceLocator.SingleHighestPriority)]
    public class FooSingleHighestPriority1 : IFoo
    {
    }

    [Export(-20, Name = ServiceLocator.SingleHighestPriority)]
    public class FooSingleHighestPriority2 : IFoo
    {
    }

    [Export(-10, Name = ServiceLocator.SingleHighestPriority)]
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

    [Export(-30, Name = ServiceLocator.SingleHighestPriority)]
    public class BarFactorySingleHighestPriority1 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarSingleHighestPriority1(123);
        }
    }

    [Export(-20, Name = ServiceLocator.SingleHighestPriority)]
    public class BarFactorySingleHighestPriority2 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarSingleHighestPriority2(123);
        }
    }

    [Export(-10, Name = ServiceLocator.SingleHighestPriority)]
    public class BarFactorySingleHighestPriority3 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarSingleHighestPriority3(123);
        }
    }

    #endregion

    #region MultipleHighestPriority Exports

    [Export(-100, Name = ServiceLocator.MultipleHighestPriority)]
    public class FooMultipleHighestPriority1 : IFoo
    {
    }

    [Export(-100, Name = ServiceLocator.MultipleHighestPriority)]
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

    [Export(-100, Name = ServiceLocator.MultipleHighestPriority)]
    public class BarFactoryMultipleHighestPriority1 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarMultipleHighestPriority1(123);
        }
    }

    [Export(-100, Name = ServiceLocator.MultipleHighestPriority)]
    public class BarFactoryMultipleHighestPriority2 : IBarFactory
    {
        public IBar GetBar()
        {
            return new BarMultipleHighestPriority2(123);
        }
    }

    #endregion

    #region DuplicateExport Exports

    [Export(-300, Name = ServiceLocator.DuplicateExport)]
    [Export(-200, Name = ServiceLocator.DuplicateExport)]
    [Export(-200, Name = ServiceLocator.DuplicateExport)]
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

    [Export(-300, Name = ServiceLocator.DuplicateExport)]
    [Export(-200, Name = ServiceLocator.DuplicateExport)]
    [Export(-200, Name = ServiceLocator.DuplicateExport)]
    public class DuplicateExportBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new DuplicateExportBar(123);
        }
    }

    #endregion

    #region Non-Default Constructor Exports

    [Export(-400, Name = ServiceLocator.NonDefaultConstructor)]
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

    [Export(-400, Name = ServiceLocator.NonDefaultConstructor)]
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
}
