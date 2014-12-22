using System;

namespace Rock.StaticDependencyInjection
{
    internal class ExportInfo
    {
        private readonly Type _targetClass;
        private readonly int _priority;

        internal ExportInfo(Type targetClass)
            : this(targetClass, -1)
        {
        }

        internal ExportInfo(Type targetClass, int priority)
        {
            if (targetClass.Assembly.ReflectionOnly)
            {
                targetClass = Type.GetType(targetClass.AssemblyQualifiedName);
            }

            _targetClass = targetClass;
            _priority = priority;
        }

        internal Type TargetClass { get { return _targetClass; } }
        internal int Priority { get { return _priority; } }
        internal string Name { get; set; }
        internal bool Disabled { get; set; }
    }
}