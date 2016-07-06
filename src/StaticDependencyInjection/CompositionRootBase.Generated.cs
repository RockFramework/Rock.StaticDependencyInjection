using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rock.StaticDependencyInjection
{
    internal abstract class CompositionRootBase
    {
        private readonly ConcurrentDictionary<Tuple<string, bool>, ICollection<Type>> _candidateTypesCache;
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Tuple<string, string, bool, bool>, IEnumerable<string>>> _candidateTypeNamesByTargetTypeNameCache;

        protected CompositionRootBase()
        {
            _candidateTypesCache = new ConcurrentDictionary<Tuple<string, bool>, ICollection<Type>>();
            _candidateTypeNamesByTargetTypeNameCache = new ConcurrentDictionary<string, ConcurrentDictionary<Tuple<string, string, bool, bool>, IEnumerable<string>>>();
        }

        /// <summary>
        /// Import the types for this library by calling one of the import methods:
        /// <see cref="ImportSingle{TTargetType}"/>, <see cref="ImportSingle{TTargetType,TFactoryType}"/>,
        /// <see cref="ImportFirst{TTargetType}"/>, <see cref="ImportFirst{TTargetType,TFactoryType}"/>,
        /// <see cref="ImportMultiple{TTargetType}"/>, or <see cref="ImportMultiple{TTargetType, TFactoryType}"/>.
        /// </summary>
        public abstract void Bootstrap();

        /// <summary>
        /// Gets a value indicating whether static dependency injection is enabled.
        /// </summary>
        public virtual bool IsEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Called when an error condition occurrs.
        /// </summary>
        /// <param name="message">A message describing the error condition.</param>
        /// <param name="exception">The <see cref="Exception"/> that caused the error condition. Can be null.</param>
        /// <param name="import">An <see cref="ImportInfo"/> object that describes the import operation that failed.</param>
        protected virtual void OnError(string message, Exception exception, ImportInfo import)
        {
#if DEBUG
            var sb = new StringBuilder();

            if (message != null)
            {
                sb.AppendLine("Message: " + message).AppendLine();
            }

            if (import != null)
            {
                sb.AppendLine("Import: " + import.ToString()).AppendLine();
            }

            if (exception != null)
            {
                sb.AppendLine(exception.ToString()).AppendLine();
            }

            var debugMessage = sb.ToString();
            Debug.WriteLine(debugMessage);
#endif
        }

        /// <summary>
        /// Return a collection of metadata objects that describe the export operations for a type.
        /// </summary>
        /// <param name="type">The type to get export metadata.</param>
        /// <returns>A collection of metadata objects that describe export operations.</returns>
        protected virtual IEnumerable<ExportInfo> GetExportInfos(Type type)
        {
            yield return new ExportInfo(type);
        }

        /// <summary>
        /// Return an object that defines various options.
        /// </summary>
        protected virtual ImportOptions GetDefaultImportOptions()
        {
            return new ImportOptions();
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/>. When a single
        /// class with a public parameterless constructor is found that implements or
        /// inherits from <typeparamref name="TTargetType"/>, then an instance of that class 
        /// will be created and passed to the <paramref name="importAction"/> parameter callback.
        /// </summary>
        /// <typeparam name="TTargetType">
        /// The type to import. An object of this type will be passed to the 
        /// <paramref name="importAction"/> parameter callback.
        /// </typeparam>
        /// <param name="importAction">
        /// A callback function to invoke when an implementation of
        ///  <typeparamref name="TTargetType"/> is created.
        /// </param>
        /// <param name="importName">
        /// The name of this import operation. If not null, exported classes without 
        /// a matching name are excluded.
        /// </param>
        /// <param name="options">
        /// The import options to use. If null or not provided, the value returned by 
        /// <see cref="GetDefaultImportOptions"/> is returned.
        /// </param>
        protected void ImportSingle<TTargetType>(
            Action<TTargetType> importAction,
            string importName = null,
            ImportOptions options = null)
            where TTargetType : class
        {
            var import = GetImportInfo<TTargetType>(importName, options);

            try
            {
                ImportSingleType(
                    importAction,
                    import,
                    type => CreateInstance<TTargetType>(type, import));
            }
            catch (Exception ex)
            {
                var message = string.Format("Unexpected error in ImportSingle<{0}>.",
                    typeof(TTargetType));

                OnError(message, ex, import);
            }
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/>. When a single
        /// class with a public parameterless constructor is found that implements or
        /// inherits from either <typeparamref name="TTargetType"/> or
        /// <typeparamref name="TFactoryType"/>, then an instance of that class is created. 
        /// If that instance is a <typeparamref name="TTargetType"/>, than that instance will be
        /// passed to the <paramref name="importAction"/> callback. If the instance is a
        /// <typeparamref name="TFactoryType"/>, then an instance of
        /// <typeparamref name="TTargetType"/> is obtained by using the 
        /// <paramref name="getTarget"/> function and passed to the 
        /// <paramref name="importAction"/> callback.
        /// </summary>
        /// <typeparam name="TTargetType">
        /// The type to import. An object of this type will be passed to the 
        /// <paramref name="importAction"/> parameter callback.
        /// </typeparam>
        /// <typeparam name="TFactoryType">
        /// A type that exposes a method or property that can be invoked to obtain an instance 
        /// of <typeparamref name="TTargetType"/>.
        /// </typeparam>
        /// <param name="importAction">
        /// A callback function to invoke when an implementation of <typeparamref name="TTargetType"/> is created.
        /// </param>
        /// <param name="getTarget">
        /// A function used to obtain an instance of <typeparamref name="TTargetType"/>
        /// by using an instance of <typeparamref name="TFactoryType"/>.
        /// </param>
        /// <param name="importName">
        /// The name of this import operation. If not null, exported classes without a matching name are excluded.
        /// </param>
        /// <param name="options">
        /// The import options to use. If null or not provided, the value returned by 
        /// <see cref="GetDefaultImportOptions"/> is returned.
        /// </param>
        protected void ImportSingle<TTargetType, TFactoryType>(
            Action<TTargetType> importAction,
            Func<TFactoryType, TTargetType> getTarget,
            string importName = null,
            ImportOptions options = null)
            where TTargetType : class
            where TFactoryType : class
        {
            var import = GetImportInfo<TTargetType>(importName, options, typeof(TFactoryType));

            try
            {
                ImportSingleType(
                    importAction,
                    import,
                    type => CreateInstance(type, getTarget, import));
            }
            catch (Exception ex)
            {
                var message = string.Format("Unexpected error in ImportSingle<{0}, {1}>.",
                    typeof(TTargetType), typeof(TFactoryType));

                OnError(message, ex, import);
            }
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/>. When any class
        /// with a public parameterless constructor is found that implements or inherits from
        /// <typeparamref name="TTargetType"/>, then the one with the highest priority will be
        /// created and passed to the <paramref name="importAction"/> parameter callback.
        /// </summary>
        /// <typeparam name="TTargetType">
        /// The type to import. An object of this type will be passed to the 
        /// <paramref name="importAction"/> parameter callback.
        /// </typeparam>
        /// <param name="importAction">
        /// A callback function to invoke when an implementation of <typeparamref name="TTargetType"/> is created.
        /// </param>
        /// <param name="importName">
        /// The name of this import operation. If not null, exported classes without a matching name are excluded.
        /// </param>
        /// <param name="options">
        /// The import options to use. If null or not provided, the value returned by 
        /// <see cref="GetDefaultImportOptions"/> is returned.
        /// </param>
        protected void ImportFirst<TTargetType>(
            Action<TTargetType> importAction,
            string importName = null,
            ImportOptions options = null)
            where TTargetType : class
        {
            var import = GetImportInfo<TTargetType>(importName, options);

            try
            {
                ImportFirstType(importAction, GetInstances<TTargetType>(import), import);
            }
            catch (Exception ex)
            {
                var message = string.Format("Unexpected error in ImportFirst<{0}>.",
                    typeof(TTargetType));

                OnError(message, ex, import);
            }
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/>. When any
        /// class with a public parameterless constructor is found that implements or
        /// inherits from either <typeparamref name="TTargetType"/> or
        /// <typeparamref name="TFactoryType"/>, then an instance of the highest priority
        /// class is created. If that instance is a <typeparamref name="TTargetType"/>, than that 
        /// instance will be passed to the <paramref name="importAction"/> callback. If the 
        /// instance is a <typeparamref name="TFactoryType"/>, then an instance of
        /// <typeparamref name="TTargetType"/> is obtained by using the 
        /// <paramref name="getTarget"/> function and passed to the 
        /// <paramref name="importAction"/> callback.
        /// </summary>
        /// <typeparam name="TTargetType">
        /// The type to import. An object of this type will be passed to the 
        /// <paramref name="importAction"/> parameter callback.
        /// </typeparam>
        /// <typeparam name="TFactoryType">
        /// A type that exposes a method or property that can be invoked to obtain an instance 
        /// of <typeparamref name="TTargetType"/>.
        /// </typeparam>
        /// <param name="importAction">
        /// A callback function to invoke when an implementation of <typeparamref name="TTargetType"/> is created.
        /// </param>
        /// <param name="getTarget">
        /// A function used to obtain an instance of <typeparamref name="TTargetType"/>
        /// by using an instance of <typeparamref name="TFactoryType"/>.
        /// </param>
        /// <param name="importName">
        /// The name of this import operation. If not null, exported classes without a matching name are excluded.
        /// </param>
        /// <param name="options">
        /// The import options to use. If null or not provided, the value returned by 
        /// <see cref="GetDefaultImportOptions"/> is returned.
        /// </param>
        protected void ImportFirst<TTargetType, TFactoryType>(
            Action<TTargetType> importAction,
            Func<TFactoryType, TTargetType> getTarget,
            string importName = null,
            ImportOptions options = null)
            where TTargetType : class
            where TFactoryType : class
        {
            var import = GetImportInfo<TTargetType>(importName, options, typeof(TFactoryType));

            try
            {
                ImportFirstType(importAction, GetInstances(getTarget, import), import);
            }
            catch (Exception ex)
            {
                var message = string.Format("Unexpected error in ImportFirst<{0}, {1}>.",
                    typeof(TTargetType), typeof(TFactoryType));

                OnError(message, ex, import);
            }
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/> for many
        /// implementations. When zero to many classes with a public parameterless 
        /// constructor are found that implements or inherits from 
        /// <typeparamref name="TTargetType"/>, then an instances of those class will be 
        /// created and passed to the <paramref name="importAction"/> parameter callback.
        /// </summary>
        /// <typeparam name="TTargetType">
        /// The type to import. Objects of this type will be passed to the 
        /// <paramref name="importAction"/> parameter callback.
        /// </typeparam>
        /// <param name="importAction">
        /// A callback function to invoke when a implementations of 
        /// <typeparamref name="TTargetType"/> are created.
        /// </param>
        /// <param name="importName">
        /// The name of this import operation. If not null, exported classes without a matching name are excluded.
        /// </param>
        /// <param name="options">
        /// The import options to use. If null or not provided, the value returned by 
        /// <see cref="GetDefaultImportOptions"/> is returned.
        /// </param>
        protected void ImportMultiple<TTargetType>(
            Action<IEnumerable<TTargetType>> importAction,
            string importName = null,
            ImportOptions options = null)
            where TTargetType : class
        {
            var import = GetImportInfo<TTargetType>(importName, options);

            List<TTargetType> instances;

            try
            {
                instances = GetInstances<TTargetType>(import).ToList();
            }
            catch (Exception ex)
            {
                var message = string.Format("Unexpected error in ImportMultiple<{0}>.",
                    typeof(TTargetType));

                OnError(message, ex, import);
                return;
            }

            try
            {
                importAction(instances);
            }
            catch (Exception ex)
            {
                OnError("An error occurred in the 'importAction' callback.", ex, import);
            }
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/> for many 
        /// implementations. When zero to many classes with a public parameterless 
        /// constructor are found that implements or inherits from either 
        /// <typeparamref name="TTargetType"/> or <typeparamref name="TFactoryType"/>, 
        /// then instances of those classes are created. If an instance is a 
        /// <typeparamref name="TTargetType"/>, than that instance will be passed as part of a 
        /// collection to the <paramref name="importAction"/> callback. If an instance is a
        /// <typeparamref name="TFactoryType"/>, then an instance of
        /// <typeparamref name="TTargetType"/> is obtained by using the 
        /// <paramref name="getTarget"/> function and passed to the 
        /// <paramref name="importAction"/> callback.
        /// </summary>
        /// <typeparam name="TTargetType">
        /// The type to import. Objects of this type will be passed to the 
        /// <paramref name="importAction"/> parameter callback.
        /// </typeparam>
        /// <typeparam name="TFactoryType">
        /// A type that exposes a method or property that can be invoked to obtain an instance 
        /// of <typeparamref name="TTargetType"/>.
        /// </typeparam>
        /// <param name="importAction">
        /// A callback function to invoke when a implementations of 
        /// <typeparamref name="TTargetType"/> are created.
        /// </param>
        /// <param name="getTarget">
        /// A function used to obtain an instance of <typeparamref name="TTargetType"/>
        /// by using an instance of <typeparamref name="TFactoryType"/>.
        /// </param>
        /// <param name="importName">
        /// The name of this import operation. If not null, exported classes without a matching name are excluded.
        /// </param>
        /// <param name="options">
        /// The import options to use. If null or not provided, the value returned by 
        /// <see cref="GetDefaultImportOptions"/> is returned.
        /// </param>
        protected void ImportMultiple<TTargetType, TFactoryType>(
            Action<IEnumerable<TTargetType>> importAction,
            Func<TFactoryType, TTargetType> getTarget,
            string importName = null,
            ImportOptions options = null)
            where TTargetType : class
            where TFactoryType : class
        {
            var import = GetImportInfo<TTargetType>(importName, options, typeof(TFactoryType));

            List<TTargetType> instances;

            try
            {
                instances = GetInstances(getTarget, import).ToList();
            }
            catch (Exception ex)
            {
                var message = string.Format("Unexpected error in ImportMultiple<{0}, {1}>.",
                    typeof(TTargetType), typeof(TFactoryType));

                OnError(message, ex, import);
                return;
            }

            try
            {
                importAction(instances);
            }
            catch (Exception ex)
            {
                OnError("An error occurred in the 'importAction' callback.", ex, import);
            }
        }

        private IEnumerable<TTargetType> GetInstances<TTargetType>(ImportInfo import)
            where TTargetType : class
        {
            return GetInstances(import, type => CreateInstance<TTargetType>(type, import));
        }

        private IEnumerable<TTargetType> GetInstances<TTargetType, TFactoryType>(
            Func<TFactoryType, TTargetType> getTarget,
            ImportInfo import)
            where TTargetType : class
            where TFactoryType : class
        {
            return GetInstances(import, type => CreateInstance(type, getTarget, import));
        }

        private ImportInfo GetImportInfo<TTargetType>(
            string importName,
            ImportOptions options,
            Type factoryType = null)
            where TTargetType : class
        {
            return new ImportInfo(
                importName,
                typeof(TTargetType),
                factoryType,
                options ?? GetDefaultImportOptions());
        }

        private void ImportSingleType<TTargetType>(
            Action<TTargetType> importAction,
            ImportInfo import,
            Func<Type, TTargetType> createInstance)
            where TTargetType : class
        {
            var candidateTypeNames = GetCandidateTypeNames(import);

            var prioritizedGroupsOfCandidateTypes =
                GetPrioritizedGroupsOfCandidateTypes(candidateTypeNames, import).GetEnumerator();

            // If any candidate types were found, the enumerator will advance.
            if (prioritizedGroupsOfCandidateTypes.MoveNext())
            {
                var highestPriorityCandidateTypes = prioritizedGroupsOfCandidateTypes.Current;

                var bestCandidateType = ChooseCandidateType(highestPriorityCandidateTypes, import);

                if (bestCandidateType == null)
                {
                    var message = string.Format(
                        "Unable to import a single instance of type '{0}' - more than one export " +
                        "with the same highest priority was discovered: {1}.", typeof(TTargetType),
                        string.Join(", ", highestPriorityCandidateTypes.Select(t => "'" + t + "'")));

                    OnError(message, null, import);
                }
                else
                {
                    var instance = createInstance(bestCandidateType);

                    try
                    {
                        importAction(instance);
                    }
                    catch (Exception ex)
                    {
                        OnError("An error occurred in the 'importAction' callback.", ex, import);
                    }
                }
            }
        }

        private void ImportFirstType<TTargetType>(
            Action<TTargetType> importAction,
            IEnumerable<TTargetType> instances,
            ImportInfo import)
            where TTargetType : class
        {
            var instance = instances.FirstOrDefault();

            if (instance != null)
            {
                try
                {
                    importAction(instance);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred in the 'importAction' callback.", ex, import);
                }
            }
        }

        private IEnumerable<TTargetType> GetInstances<TTargetType>(
            ImportInfo import,
            Func<Type, TTargetType> createInstance)
            where TTargetType : class
        {
            var candidateTypeNames = GetCandidateTypeNames(import);

            var prioritizedGroupsOfCandidateTypes =
                GetPrioritizedGroupsOfCandidateTypes(
                    candidateTypeNames,
                    import);

            return (
                from candidateTypes in prioritizedGroupsOfCandidateTypes
                from candidateType in candidateTypes
                select createInstance(candidateType))
                .Where(instance => instance != null);
        }

        private TTargetType CreateInstance<TTargetType>(Type candidateType, ImportInfo import)
            where TTargetType : class
        {
            object instance;
            try
            {
                instance = Instantiate(candidateType);
            }
            catch (Exception ex)
            {
                OnError(string.Format(
                    "An error occurred while creating an instance of the '{0}' type.",
                    candidateType), ex, import);
                return null;
            }

            var target = instance as TTargetType;
            if (target != null)
            {
                return target;
            }

            OnError(string.Format(
                "The type of the created instance, '{0}', is not assignable to the target type, '{1}'.",
                instance.GetType(), typeof(TTargetType)), null, import);
            return null;
        }

        private TTargetType CreateInstance<TTargetType, TFactoryType>(
            Type candidateType,
            Func<TFactoryType, TTargetType> getTarget,
            ImportInfo import)
            where TTargetType : class
            where TFactoryType : class
        {
            object instance;
            try
            {
                instance = Instantiate(candidateType);
            }
            catch (Exception ex)
            {
                OnError(string.Format(
                    "An error occurred while creating an instance of the '{0}' type.",
                    candidateType), ex, import);
                return null;
            }

            var factory = instance as TFactoryType;
            if (factory != null)
            {
                try
                {
                    return getTarget(factory);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred in the 'getTarget' callback.", ex, import);
                    return null;
                }
            }

            var target = instance as TTargetType;
            if (target != null)
            {
                return target;
            }

            OnError(string.Format(
                "The type of the created instance, '{0}', is not assignable to the target type, '{1}'.",
                instance.GetType(), typeof(TTargetType)), null, import);
            return null;
        }

        private static object Instantiate(Type candidateType)
        {
            if (candidateType.GetConstructor(Type.EmptyTypes) != null)
            {
                return Activator.CreateInstance(candidateType);
            }

            var ctor =
                candidateType.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Length)
                    .First(c => c.GetParameters().All(HasDefaultValue));

            var args = ctor.GetParameters().Select(p => p.DefaultValue).ToArray();

            return Activator.CreateInstance(candidateType, args);
        }

        private IEnumerable<IList<Type>> GetPrioritizedGroupsOfCandidateTypes(
            IEnumerable<string> candidateTypeNames,
            ImportInfo import)
        {
            Func<Type, bool> isPreferredType;

            if (import.FactoryType == null)
            {
                isPreferredType = type => false;
            }
            else
            {
                if (import.Options.PreferTTargetType)
                {
                    isPreferredType = GetIsTargetTypeFunc(import.TargetType, import.Options.AllowNonPublicClasses);
                }
                else
                {
                    isPreferredType = GetIsTargetTypeFunc(import.FactoryType, import.Options.AllowNonPublicClasses);
                }
            }

            var prioritizedGroupsOfCandidateTypes =
                candidateTypeNames.SelectMany(GetExportInfos)
                    .Where(export =>
                        export != null
                        && !export.Disabled
                        && AreCompatible(import, export))
                    .GroupBy(x => x.Priority)
                    .OrderByDescending(g => g.Key)
                    .Select(g =>
                        g.OrderByDescending(export => isPreferredType(export.TargetClass))
                            .ThenBy(export => export, import.Options.ExportComparer)
                            .ToList())
                    .ToList();

            var uniqueExports = new List<ExportInfo>();
            var groupsToRemove = new List<List<ExportInfo>>();

            foreach (var group in prioritizedGroupsOfCandidateTypes)
            {
                var exportsToRemove = new List<ExportInfo>();

                foreach (var export in group)
                {
                    if (uniqueExports.Any(uniqueExport =>
                        export.TargetClass == uniqueExport.TargetClass
                        && export.Name == uniqueExport.Name))
                    {
                        exportsToRemove.Add(export);
                    }
                    else
                    {
                        uniqueExports.Add(export);
                    }
                }

                foreach (var export in exportsToRemove)
                {
                    group.Remove(export);
                }

                if (group.Count == 0)
                {
                    groupsToRemove.Add(group);
                }
            }

            foreach (var group in groupsToRemove)
            {
                prioritizedGroupsOfCandidateTypes.Remove(group);
            }

            return
                prioritizedGroupsOfCandidateTypes
                    .Select(group => group.Select(g => g.TargetClass).ToList()).ToList();
        }

        private static bool AreCompatible(ImportInfo import, ExportInfo export)
        {
            if (!import.Options.IncludeNamedExportsFromUnnamedImports
                && export.Name != null
                && import.Name == null)
            {
                return false;
            }

            return export.Name == import.Name || import.Name == null;
        }

        private static Type ChooseCandidateType(
            IList<Type> candidateTypes,
            ImportInfo import)
        {
            if (candidateTypes.Count == 1)
            {
                return candidateTypes[0];
            }

            if (import.FactoryType != null)
            {
                var data =
                    candidateTypes.Select(type =>
                    {
                        var interfaces = type.GetInterfaces();

                        return new
                        {
                            Type = type,
                            IsTarget = interfaces.Any(i => i.AssemblyQualifiedName == import.TargetTypeName),
                            IsFactory = interfaces.Any(i => i.AssemblyQualifiedName == import.FactoryTypeName)
                        };
                    }).ToList();

                if (import.Options.PreferTTargetType)
                {
                    if ((data.Count(x => x.IsTarget) == 1)
                        && (data.Count(x => x.IsFactory && !x.IsTarget) == (data.Count - 1)))
                    {
                        return data.Single(x => x.IsTarget).Type;
                    }
                }
                else
                {
                    if ((data.Count(x => x.IsFactory) == 1)
                        && (data.Count(x => x.IsTarget && !x.IsFactory) == (data.Count - 1)))
                    {
                        return data.Single(x => x.IsFactory).Type;
                    }
                }
            }

            return null;
        }

        private IEnumerable<ExportInfo> GetExportInfos(string assemblyQualifiedName)
        {
            try
            {
                var type = Type.GetType(assemblyQualifiedName);

                if (type == null)
                {
                    return null;
                }

                return GetExportInfos(type);
            }
            catch
            {
                return null;
            }
        }

        private IEnumerable<string> GetCandidateTypeNames(ImportInfo import)
        {
            var candidateTypeNamesCache =
                _candidateTypeNamesByTargetTypeNameCache.GetOrAdd(
                    import.TargetTypeName,
                    _ => new ConcurrentDictionary<Tuple<string, string, bool, bool>, IEnumerable<string>>());

            var key = Tuple.Create(
                import.TargetTypeName,
                import.FactoryTypeName,
                import.Options.AllowNonPublicClasses,
                import.Options.IncludeTypesFromThisAssembly);

            var candidateTypeNames =
                candidateTypeNamesCache.GetOrAdd(
                    key,
                    _ =>
                    {
                        var isTargetType = GetIsTargetTypeFunc(import.TargetType, import.Options.AllowNonPublicClasses);

                        if (import.FactoryType != null)
                        {
                            var isFactoryType = GetIsTargetTypeFunc(import.FactoryType, import.Options.AllowNonPublicClasses);
                            var isTargetTypeLocal = isTargetType;

                            isTargetType = type => isTargetTypeLocal(type) || isFactoryType(type);
                        }

                        var candidateTypes =
                            _candidateTypesCache.GetOrAdd(
                                Tuple.Create(string.Join("|", import.Options.DirectoryPaths), import.Options.IncludeTypesFromThisAssembly),
                                __ => GetCandidateTypes(import.Options.DirectoryPaths, import.Options.IncludeTypesFromThisAssembly));

                        return
                            candidateTypes
                                .Where(isTargetType)
                                .Select(t => t.AssemblyQualifiedName)
                                .ToList();
                    });

            return candidateTypeNames;
        }

        private static Func<Type, bool> GetIsTargetTypeFunc(Type targetType, bool allowNonPublicClasses)
        {
            var targetTypeName = targetType.AssemblyQualifiedName;

            if (targetTypeName == null)
            {
                return typeInQuestion => false;
            }

            if (targetType.IsInterface)
            {
                return typeInQuestion =>
                    (typeInQuestion.IsPublic || allowNonPublicClasses)
                    && typeInQuestion.GetInterfaces().Any(i => i.AssemblyQualifiedName == targetTypeName);
            }

            if (targetType.IsClass && !targetType.IsSealed)
            {
                return typeInQuestion =>
                {
                    var type = typeInQuestion;

                    while (type != null)
                    {
                        if (type.AssemblyQualifiedName == targetTypeName)
                        {
                            return true;
                        }

                        type = type.BaseType;
                    }

                    return false;
                };
            }

            return typeInQuestion => false;
        }

        private ICollection<Type> GetCandidateTypes(IEnumerable<string> directoryPaths, bool includeTypesFromThisAssembly)
        {
            try
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AppDomainOnReflectionOnlyAssemblyResolve;
                return GetAssemblyFiles(directoryPaths, includeTypesFromThisAssembly).SelectMany(x => LoadCandidateTypes(x, includeTypesFromThisAssembly)).ToList();
            }
            finally
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= AppDomainOnReflectionOnlyAssemblyResolve;
            }
        }

        private static Assembly AppDomainOnReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        private static IEnumerable<string> GetAssemblyFiles(IEnumerable<string> directoryPaths, bool includeTypesFromThisAssembly)
        {
            foreach (var directoryPath in directoryPaths)
            {
                if (!Directory.Exists(directoryPath))
                {
                    continue;
                }

                IEnumerable<string> dllFiles;

                try
                {
                    dllFiles = Directory.EnumerateFiles(directoryPath, "*.dll");
                }
                catch
                {
                    dllFiles = Enumerable.Empty<string>();
                }

                IEnumerable<string> exeFiles;

                try
                {
                    exeFiles = Directory.EnumerateFiles(directoryPath, "*.exe");
                }
                catch
                {
                    exeFiles = Enumerable.Empty<string>();
                }

                foreach (var file in dllFiles.Concat(exeFiles))
                {
                    yield return file;
                }
            }
        }

        private static IEnumerable<Type> LoadCandidateTypes(string assemblyFile, bool includeTypesFromThisAssembly)
        {
            try
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);

                if (!includeTypesFromThisAssembly
                    && assembly.FullName == typeof(CompositionRootBase).Assembly.FullName)
                {
                    return Enumerable.Empty<Type>();
                }

                return
                    GetTypesSafely(assembly)
                        .Where(t =>
                            t.IsClass
                            && !t.IsAbstract
                            && t.AssemblyQualifiedName != null
                            && HasDefaultishConstructor(t));
            }
            catch
            {
                return Enumerable.Empty<Type>();
            }
        }

        private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null);
            }
        }

        private static bool HasDefaultishConstructor(Type type)
        {
            try
            {
                var constructors = GetConstructors(type);

                return
                    constructors.Any(
                        ctor =>
                        {
                            var parameters = ctor.GetParameters();
                            return parameters.Length == 0 || parameters.All(HasDefaultValue);
                        });
            }
            catch
            {
                return false;
            }
        }

        private static IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {
            // Retreiving constructors via TypeInfo.DeclaredConstructors is less likely to throw an
            // an exception, but TypeInfo doesn't exist in .NET 4.0. So use reflection to attempt
            // to try to get a TypeInfo. If we're unable to get it, just return type.GetConstructors()

            var introspectionExtensionsType = Type.GetType("System.Reflection.IntrospectionExtensions, mscorlib");
            if (introspectionExtensionsType == null)
            {
                return type.GetConstructors();
            }

            var getTypeInfo = introspectionExtensionsType.GetMethod("GetTypeInfo");
            if (getTypeInfo == null)
            {
                return type.GetConstructors();
            }

            dynamic typeInfo = getTypeInfo.Invoke(null, new object[] { type });
            IEnumerable<ConstructorInfo> constructors = typeInfo.DeclaredConstructors;

            return constructors.Where(ctor => ctor.IsPublic);
        }

        private static bool HasDefaultValue(ParameterInfo parameter)
        {
            const ParameterAttributes hasDefaultValue =
                ParameterAttributes.HasDefault | ParameterAttributes.Optional;

            return (parameter.Attributes & hasDefaultValue) == hasDefaultValue;
        }
    }
}