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

    #region Nothing Exports



    #endregion

    #region Nothing Exports



    #endregion

    #region Nothing Exports



    #endregion

    #region Nothing Exports



    #endregion
}
