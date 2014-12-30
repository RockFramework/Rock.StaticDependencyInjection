using Rock.StaticDependencyInjection.AcceptanceTests.Library;

namespace Rock.StaticDependencyInjection.AcceptanceTests
{
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
}
