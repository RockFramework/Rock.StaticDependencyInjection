using System;
using System.Collections.Generic;
using System.Linq;

namespace Rock.StaticDependencyInjection
{
    /// <summary>
    /// Defines various options for an import operation.
    /// </summary>
    internal class ImportOptions
    {
        private string[] _directoryPaths;

        public ImportOptions()
        {
            _directoryPaths = new[] { AppDomain.CurrentDomain.BaseDirectory };
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow non-public classes to be imported.
        /// Default value is false.
        /// </summary>
        public bool AllowNonPublicClasses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a named export will be included from an
        /// unnamed import operation. Default value is false.
        /// </summary>
        public bool IncludeNamedExportsFromUnnamedImports { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether, given equal priorities, an implementation
        /// of TTargetType will be chosen over an implementation of TFactoryType. Default
        /// value is false.
        /// </summary>
        public bool PreferTTargetType { get; set; }

        /// <summary>
        /// Gets or sets the directory paths that are searched for this import operation. The
        /// default value is an array containing a single element: the value returned by
        /// AppDomain.CurrentDomain.BaseDirectory.
        /// </summary>
        public IEnumerable<string> DirectoryPaths
        {
            get { return _directoryPaths; }
            set
            {
                if (value != null)
                {
                    _directoryPaths = value.OrderBy(x => x).ToArray();
                }
            }
        }
    }
}