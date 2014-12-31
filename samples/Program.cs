using System;
using Rock.StaticDependencyInjection;

namespace ExampleApplication
{
    class Program
    {
        static void Main()
        {
            RunApplication();
        }

        private static void RunApplication()
        {
            var widget = new Widget();
            widget.ReportDependencies();

            Console.WriteLine();
            Console.Write("Press any key to exit. . .");
            Console.ReadKey(true);
        }

        private class SecretBaz : IBaz { }
    }

    public class MyFoo : IFoo { }

    [Export(Name = "NamedFoo")]
    public class NamedFoo : IFoo { }

    public class MyBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new MyBar(123);
        }

        private class MyBar : IBar
        {
            public MyBar(int i)
            {
            }
        }
    }

    public class AnotherBar : IBar { }

    public class MyBaz : IBaz { }

    public class AnotherBaz : IBaz { }

    public class MyQuxFactory : IQuxFactory
    {
        public IQux GetQux()
        {
            return new MyQux(123);
        }

        private class MyQux : IQux
        {
            public MyQux(int i)
            {
            }
        }
    }

    public class AnotherQux : IQux { }
}
