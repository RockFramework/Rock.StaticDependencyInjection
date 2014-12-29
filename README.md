Rock.StaticDependencyInjection
==============================

Rock.StaticDependencyInjection is a dependency injection mechanism intended to be used by libraries, not applications. For applications, wiring up dependencies happens in the composition root. For console and WinForms applications, this would be at the beginning of Program.Main. For standard web apps, this would be in global.asax's `Application_Start` method. However, libraries do not have an entry point. Without an entry point, a library has no composition root. This is one problem that Rock.StaticDependencyInjection solves - it creates an entry point in your library.

Rock.StaticDependencyInjection also provides an api for *static* dependency injection. Most DI containers work via the concepts of constructor injection or property injection. These mechanisms rely on the creation and modification of objects. But what if you have a static dependency? And just what do I mean by a static dependency?

Let's say you a widget library. And this is what a widget looks like, along with its dependency's interface.

``` C#
    public class Widget
    {
        private readonly IFoo _foo;
        
        public Widget(IFoo foo = null)
        {
            _foo = foo ?? Foo.Current;
        }
        
        public void ReportDependencies()
        {
            Console.WriteLine("IFoo: {0}", _foo);
        }
    }

    public interface IFoo
    {
    }
```
    
There are a few interesting things about this `Widget` class. First, it has an *optional* dependency on the `IFoo` interface - if not provided, the value of the `foo` parameter defaults to null. Second, if that foo parameter is indeed null, it uses `Foo.Current` as its value. Here are the rest of the classes in this example.

``` C#
    public static class Foo
    {
        static Foo()
        {
            Current = new DefaultFoo();
        }

        public static IFoo Current { internal get; set; }
    }

    public class DefaultFoo : IFoo
    {
    }
```

`Foo.Current` is the static dependency. It provides a fall-back `IFoo` value for `Widget` to use if one was not explicitly provided. While this is dangerously close to the Service Locator anti-pattern, I give it a pass here for two reasons. First, the getter is internal, making the Current property unusable outside of the library, making it not exactly the Service Locator pattern. And second, because there is a statically-accessible default value, the API of our library improves. Consumers of the `Widget` class don't need to pass an instance of `IFoo` to its constructor if they are ok with using the value from `Foo.Current` instead. This is a huge win for library APIs.

Here's how an application can customize the value of `Foo.Current`. Pretty simple and straightforward.

``` C#
    class Program
    {
        static void Main()
        {
            Foo.Current = new SpecialFoo();
        }
    }
    
    public class SpecialFoo : IFoo
    {
    }
```

However, this pattern falls short. It requires that an application that consumes this library will have to explicitly specify, at its composition root, the implementation of IFoo to use. 

Wouldn't it be great if, just by declaring a single implementation of `IFoo`, that implementation would be used? So that the application wouldn't need to add `Foo.Current = new SpecialFoo();` to their composition root? What if that could be done automatically using some sort of convention?

Rock.StaticDependencyInjection allows you to do just that. You simply add a nuget package to your library, add a few lines of code, and your library will automatically discover and set the static dependencies that it requires with no intervention from the applications that use it.

This ability enables another neat pattern. What if an organization takes advantage of internal nuget packages for distribution of core modules of some sort? If one of these internal nuget packages depends on your library, then the libraries in that internal nuget package can define the dependencies for your library. As a result, by merely taking a nuget dependency on their internal nuget package, applications will be properly configured with the right dependency for their organization.

Entry Point Creation
====================

Many thanks to Einar Egilsson for his excellent [module initialization mechanism](http://einaregilsson.com/module-initializers-in-csharp/). Rock.StaticDependencyInjection uses it to initiate static dependency injection when a library's assembly is loaded for the first time. Einar's InjectModuleInitializer project is hosted on [github](https://github.com/einaregilsson/InjectModuleInitializer) and is available via [nuget](https://www.nuget.org/packages/InjectModuleInitializer/).