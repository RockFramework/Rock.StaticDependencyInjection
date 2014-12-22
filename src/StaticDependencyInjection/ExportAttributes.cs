﻿using System;

namespace Rock.StaticDependencyInjection
{
    /// <summary>
    /// Indicates that a class should be exported as a static dependency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public sealed class ExportAttribute : Attribute
    {
        private readonly int _priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportAttribute"/> class.
        /// </summary>
        public ExportAttribute()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportAttribute"/> class.
        /// </summary>
        /// <param name="priority">The priority of the class.</param>
        public ExportAttribute(int priority)
        {
            _priority = priority;
        }

        /// <summary>
        /// Gets a value that indicates this class's relative priority.
        /// </summary>
        public int Priority { get { return _priority; } }

        /// <summary>
        /// Gets a value indicating whether this class is explicitly ineligible for exporting.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the arbitrary name of this export. Named import operations use
        /// this value to filter eligible results.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Indicates that a class should be exported as a static dependency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
    public class ExportExternalAttribute : Attribute
    {
        private readonly Type _classType;
        private readonly int _priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportExternalAttribute"/> class.
        /// </summary>
        /// <param name="classType">The type of the class to export.</param>
        public ExportExternalAttribute(Type classType)
            : this(classType, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportExternalAttribute"/> class.
        /// </summary>
        /// <param name="classType">The type of the class to export.</param>
        /// <param name="priority">The priority of the class.</param>
        public ExportExternalAttribute(Type classType, int priority)
        {
            _priority = priority;
            _classType = classType;
        }

        /// <summary>
        /// Gets the type of class to export.
        /// </summary>
        public Type ClassType { get { return _classType; } }

        /// <summary>
        /// Gets a value that indicates this class's relative priority.
        /// </summary>
        public int Priority { get { return _priority; } }

        /// <summary>
        /// Gets a value indicating whether this class is explicitly ineligible for exporting.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the arbitrary name of this export. Named import operations use
        /// this value to filter eligible results.
        /// </summary>
        public string Name { get; set; }
    }
}