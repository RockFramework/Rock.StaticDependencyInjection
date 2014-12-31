namespace Rock.StaticDependencyInjection.Tests
{
    public interface IFoo
    {
    }

    public abstract class FooBase
    {
    }

    public interface IBar
    {
    }

    public abstract class BarBase
    {
    }

    public interface IBarFactory
    {
        IBar GetBar();
    }

    public abstract class BarFactoryBase
    {
        public abstract BarBase GetBar();
    }

    public interface IBaz
    {
    }

    public abstract class BazBase
    {
    }

    public interface IQux
    {
    }

    public abstract class QuxBase
    {
    }

    public interface IQuxFactory
    {
        IQux GetQux();
    }

    public abstract class QuxFactoryBase
    {
        public abstract QuxBase GetQux();
    }
}
