//using System;
//using System.Collections.Generic;

//namespace Rock.StaticDependencyInjection
//{
//    public class Widget
//    {
//        private readonly IFoo _foo;
//        private readonly IBar _bar;
//        private readonly IEnumerable<IBaz> _bazes;
//        private readonly IEnumerable<IQux> _quxes;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="Widget"/> class using
//        /// the specified dependencies.
//        /// </summary>
//        /// <param name="foo">
//        /// An instance of <see cref="IFoo"/>. If null or not provided, the value 
//        /// of Foo.Current will be used.
//        /// </param>
//        /// <param name="bar">
//        /// An instance of <see cref="IBar"/>. If null or not provided, the value 
//        /// of Bar.Current will be used.
//        /// </param>
//        /// <param name="bazes">
//        /// A collection of <see cref="IBaz"/> instances. If null or not provided, 
//        /// the value of BazCollection.Current will be used.
//        /// </param>
//        /// <param name="quxes">
//        /// A collection of <see cref="IQux"/> instances. If null or not provided, 
//        /// the value of QuxCollection.Current will be used.
//        /// </param>
//        public Widget(
//            IFoo foo = null,
//            IBar bar = null,
//            IEnumerable<IBaz> bazes = null,
//            IEnumerable<IQux> quxes = null)
//        {
//            _foo = foo ?? Foo.Current;
//            _bar = bar ?? Bar.Current;
//            _bazes = bazes ?? BazCollection.Current;
//            _quxes = quxes ?? QuxCollection.Current;
//        }

//        public void ReportDependencies()
//        {
//            Console.WriteLine("IFoo: {0}", _foo);
//            Console.WriteLine("IBar: {0}", _bar);
//            Console.WriteLine("Array of IBaz: [{0}]", string.Join(", ", _bazes));
//            Console.WriteLine("Array of IQux: [{0}]", string.Join(", ", _quxes));
//        }
//    }

//    public interface IFoo
//    {
//    }

//    public class DefaultFoo : IFoo
//    {
//    }

//    public static class Foo
//    {
//        static Foo()
//        {
//            Current = new DefaultFoo();
//        }

//        public static IFoo Current { internal get; set; }
//    }

//    public interface IBar
//    {
//    }

//    public class DefaultBar : IBar
//    {
//    }

//    public static class Bar
//    {
//        static Bar()
//        {
//            Current = new DefaultBar();
//        }

//        public static IBar Current { internal get; set; }
//    }

//    public interface IBarFactory
//    {
//        IBar GetBar();
//    }

//    public interface IBaz
//    {
//    }

//    public static class BazCollection
//    {
//        static BazCollection()
//        {
//            Current = new IBaz[0];
//        }

//        public static IEnumerable<IBaz> Current { internal get; set; }
//    }

//    public interface IQux
//    {
//    }

//    public static class QuxCollection
//    {
//        static QuxCollection()
//        {
//            Current = new IQux[0];
//        }

//        public static IEnumerable<IQux> Current { internal get; set; }
//    }

//    public interface IQuxFactory
//    {
//        IQux GetQux();
//    }
//}
