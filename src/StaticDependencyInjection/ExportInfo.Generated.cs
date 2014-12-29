using System;

namespace Rock.StaticDependencyInjection
{
    /// <summary>
    /// Provides information about an export.
    /// </summary>
    internal class ExportInfo
    {
        /// <summary>
        /// The default priority for an instance of <see cref="ExportInfo"/> if not specified.
        /// </summary>
        public const int DefaultPriority = -1;

        private readonly Type _targetClass;
        private readonly int _priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportInfo"/> class with
        /// a priority with the value of <see cref="DefaultPriority"/>.
        /// </summary>
        /// <param name="targetClass">The class to be exported.</param>
        public ExportInfo(Type targetClass)
            : this(targetClass, DefaultPriority)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportInfo"/> class.
        /// </summary>
        /// <param name="targetClass">The class to be exported.</param>
        /// <param name="priority">
        /// The priority of the export, relative to the priority of other exports.
        /// </param>
        public ExportInfo(Type targetClass, int priority)
        {
            if (targetClass == null)
            {
                throw new ArgumentNullException("targetClass");
            }

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
        public Type TargetClass { get { return _targetClass; } }

        /// <summary>
        /// Gets the priority of the export, relative to the priority of other exports.
        /// The default value, if not specified in the constructor is negative one.
        /// This value is used during import operations to sort the discovered classes.
        /// </summary>
        public int Priority { get { return _priority; } }

        /// <summary>
        /// Gets or sets the name of the export. This value is compared against the
        /// name parameter in various import operations.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the type indicated by
        /// <see cref="TargetClass"/> should be excluded from consideration during an
        /// import operation.
        /// </summary>
        public bool Disabled { get; set; }
    }
}