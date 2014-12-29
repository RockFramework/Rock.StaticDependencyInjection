Rock.StaticDependencyInjection
==============================

Rock.StaticDependencyInjection is a dependency injection mechanism intended to be used by libraries, not applications. For applications, wiring up dependencies happens in the composition root. For console and WinForms applications, this would be at the beginning of Program.Main. For standard web apps, this would be in global.asax's `Application_Start` method. However, libraries do not have an entry point. Without an entry point, a library has no composition root. This is one problem that Rock.StaticDependencyInjection solves - it creates an entry point in your library.

Rock.StaticDependencyInjection also provides an api for *static* dependency injection. Most DI containers work via the concepts of constructor injection or property injection. These mechanisms rely on the creation and modification of objects. But what if you have a static dependency? And just what do I mean by a static dependency?

Let's say you own a widget library. And this is what a widget looks like, along with its dependency's interface.

```csharp

public class Widget
{
    private readonly IFoo _foo;
    
    public Widget(IFoo foo = null)
    {
        _foo = foo ?? Foo.Current;
    }
    
    public void ReportDependencies()
    {
        Console.WriteLine("_foo: {0}", _foo.GetType().Name);
    }
}

public interface IFoo
{
}

```
    
There are a few interesting things about this `Widget` class. First, it has an *optional* dependency on the `IFoo` interface - if not provided, the value of the `foo` parameter defaults to null. Second, if that foo parameter is indeed null, it uses `Foo.Current` as its value. Here are the rest of the classes in this example.

```csharp

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

`Foo.Current` is the static dependency. It provides a fallback `IFoo` value for `Widget` to use if one was not explicitly provided. While this is dangerously close to the Service Locator anti-pattern, I give it a pass here for two reasons. First, the getter is internal, making the Current property unusable outside of the library. This clearly expresses its intent: you are welcome to set this value, but its value is for internal use only. Secondly: because there is a statically-accessible default value, the API of our library improves. Consumers of the `Widget` class don't need to pass an instance of `IFoo` to its constructor if they are ok with using the value from `Foo.Current` instead. This is a huge win for library APIs.

Here's how an application can customize the value of `Foo.Current`. Pretty simple and straightforward.

```csharp

class Program
{
    static void Main()
    {
        Foo.Current = new SpecialFoo();
        RunApplication();
    }
    
    void RunApplication()
    {
        // Note that we don't pass an instance of IFoo to the Widget's constructor.
        // The widget will get its IFoo from Foo.Current.
        var widget = new Widget();
        
        // Because we set Foo.Current, this will display: "_foo: SpecialFoo".
        // If we hadn't set Foo.Current, this would display: "_foo: DefaultFoo".
        widget.ReportDependencies();
    }
}

public class SpecialFoo : IFoo
{
}

```

However, while this pattern is good, it fails to be great. It requires that all applications that consume this library will have to explicitly specify, at its composition root, the implementation of IFoo to use.

Wouldn't it be great if, just by declaring a single implementation of `IFoo`, that implementation would be used? So that the application wouldn't need to add `Foo.Current = new SpecialFoo();` to their composition root? What if that could be done automatically using some sort of convention?

Rock.StaticDependencyInjection allows you to do just that. You simply add a nuget package to your library, add a few lines of code, and your library will automatically discover and set the static dependencies that it requires with no intervention from the applications that use it.

This is what composition root of your widget library might look like.

```csharp

ImportSingle<IFoo>(foo => Foo.Current = foo); // Yep, this is it.

```

With that one line of code, the static dependency injection mechanism will find the "best" implementation of `IFoo`, create an instance of it, and pass that instance to the lambda expression, `foo => Foo.Current = foo`. There are also many ways of customizing this process, including prioritizing one class over another, specifying the path(s) in which to search for assemblies with implementations of dependencies, and using named import/export pairs.

This ability to discover implementations of dependencies enables another neat pattern. What if an organization takes advantage of internal nuget packages for distribution of core modules of some sort? If one of these internal nuget packages depends on your library, then the libraries in that internal nuget package can define the dependencies for your library. As a result, by merely taking a nuget dependency on their internal nuget package, applications will be properly configured with the right dependency for their organization.

About InjectModuleInitializer
=============================

Many thanks to Einar Egilsson for his excellent [module initialization mechanism](http://einaregilsson.com/module-initializers-in-csharp/). Rock.StaticDependencyInjection uses it to initiate static dependency injection when a library's assembly is loaded for the first time. Einar's InjectModuleInitializer project is hosted on [github](https://github.com/einaregilsson/InjectModuleInitializer) and is available via [nuget](https://www.nuget.org/packages/InjectModuleInitializer/).