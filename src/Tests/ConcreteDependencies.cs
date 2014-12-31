﻿namespace Rock.StaticDependencyInjection.Tests
{
    [Export(Name = DiscoveredDependency.IncludeTypesFromThisAssembly)]
    public class AbcFoo : IFoo
    {
    }

    public class AbcBar : IBar
    {
        private readonly int _value;

        public AbcBar(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    [Export(Name = DiscoveredDependency.IncludeTypesFromThisAssembly)]
    public class AbcBarFactory : IBarFactory
    {
        public IBar GetBar()
        {
            return new AbcBar(123);
        }
    }
}
