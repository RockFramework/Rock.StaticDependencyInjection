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
        private string[] _directoryPaths = GetDefaultDirectoryPaths();

        /// <summary>
        /// Gets or sets a value indicating whether to allow non-public classes to be imported.
        /// Default value is false, indicating that only public classes will be included in an
        /// import operation.
        /// </summary>
        public bool AllowNonPublicClasses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a named export will be included from an
        /// unnamed import operation. Default value is false, indicating that named exports
        /// will not be used given an unnamed import.
        /// </summary>
        public bool IncludeNamedExportsFromUnnamedImports { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether, given equal priorities, an implementation
        /// of TTargetType will be chosen over an implementation of TFactoryType. Default
        /// value is false, indicating that TFactoryType will be preferred.
        /// </summary>
        public bool PreferTTargetType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether types that are defined in this assembly
        /// should be considered from an import operation. Default value is false, indicating
        /// that types defined in this assembly will be excluded.
        /// </summary>
        public bool IncludeTypesFromThisAssembly { get; set; }

        /// <summary>
        /// Gets or sets the directory paths that are searched for an import operation. 
        /// If not set, or set to null, the value returned will contain a single element:
        /// the value returned by AppDomain.CurrentDomain.BaseDirectory.
        /// </summary>
        public IEnumerable<string> DirectoryPaths
        {
            get { return _directoryPaths; }
            set
            {
                _directoryPaths = 
                    (value != null
                        ? value.OrderBy(x => x).ToArray()
                        : GetDefaultDirectoryPaths());
            }
        }

        /// <summary>
        /// Returns a single element: the value returned by
        /// AppDomain.CurrentDomain.BaseDirectory.
        /// </summary>
        private static string[] GetDefaultDirectoryPaths()
        {
            return new[] { AppDomain.CurrentDomain.BaseDirectory };
        }
    }
}