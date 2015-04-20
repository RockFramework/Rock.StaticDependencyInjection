using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rock.StaticDependencyInjection
{
    /// <summary>
    /// Defines various options for an import operation.
    /// </summary>
    internal class ImportOptions
    {
        private string[] _directoryPaths = GetDefaultDirectoryPaths();
        private IComparer<ExportInfo> _exportComparer = GetDefaultExportComparer();

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
                    value != null
                        ? value.OrderBy(x => x).ToArray()
                        : GetDefaultDirectoryPaths();
            }
        }

        /// <summary>
        /// Gets or sets a comparer to be used to differentiate between multiple
        /// exports with the same priority. If not set, or set to null, the value
        /// returned will be a comparer that sorts based on the assembly qualified
        /// name of the target class.
        /// </summary>
        public IComparer<ExportInfo> ExportComparer
        {
            get { return _exportComparer; }
            set
            {
                _exportComparer = value ?? GetDefaultExportComparer();
            }
        }

        /// <summary>
        /// Returns an array containing a single element: the value returned by
        /// AppDomain.CurrentDomain.BaseDirectory.
        /// </summary>
        private static string[] GetDefaultDirectoryPaths()
        {
            return new[]
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")
            };
        }

        /// <summary>
        /// Gets a new instance of <see cref="TargetClassAssemblyQualifiedNameComparer"/>.
        /// </summary>
        private static IComparer<ExportInfo> GetDefaultExportComparer()
        {
            return new TargetClassAssemblyQualifiedNameComparer();
        }

        private class TargetClassAssemblyQualifiedNameComparer : IComparer<ExportInfo>
        {
            public int Compare(ExportInfo lhs, ExportInfo rhs)
            {
                var lhsString = lhs.TargetClass.AssemblyQualifiedName ?? lhs.TargetClass.ToString();
                var rhsString = rhs.TargetClass.AssemblyQualifiedName ?? rhs.TargetClass.ToString();

                return lhsString.CompareTo(rhsString);
            }
        }
    }
}