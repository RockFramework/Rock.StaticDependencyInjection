using System;

namespace Rock.StaticDependencyInjection
{
    /// <summary>
    /// Provides information about an export.
    /// </summary>
    internal class ExportInfo
    {
        private const int _defaultPriority = -1;

        private readonly Type _targetClass;
        private readonly int _priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportInfo"/> class with
        /// a priority of negative one.
        /// </summary>
        /// <param name="targetClass">The class to be exported.</param>
        internal ExportInfo(Type targetClass)
            : this(targetClass, _defaultPriority)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportInfo"/> class.
        /// </summary>
        /// <param name="targetClass">The class to be exported.</param>
        /// <param name="priority">
        /// The priority of the export, relative to the priority of other exports.
        /// </param>
        internal ExportInfo(Type targetClass, int priority)
        {
            if (targetClass.Assembly.ReflectionOnly)
            {
                targetClass = Type.GetType(targetClass.AssemblyQualifiedName);
            }

            _targetClass = targetClass;
            _priority = priority;
        }

        /// <summary>
        /// Gets the class to be exported.
        /// </summary>
        internal Type TargetClass { get { return _targetClass; } }

        /// <summary>
        /// Gets the priority of the export, relative to the priority of other exports.
        /// The default value, if not specified in the constructor is negative one.
        /// This value is used during import operations to sort the discovered classes.
        /// </summary>
        internal int Priority { get { return _priority; } }

        /// <summary>
        /// Gets or sets the name of the export. This value is compared against the
        /// name parameter in various import operations.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the type indicated by
        /// <see cref="TargetClass"/> should be excluded from consideration during an
        /// import operation.
        /// </summary>
        internal bool Disabled { get; set; }
    }
}