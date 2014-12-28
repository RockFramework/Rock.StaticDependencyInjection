using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rock.StaticDependencyInjection
{
    internal abstract class CompositionRootBase
    {
        private readonly ConcurrentDictionary<string, ICollection<Type>> _candidateTypesCache;
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, IEnumerable<string>>> _candidateTypeNamesByTargetTypeNameCache;

        protected CompositionRootBase()
        {
            _candidateTypesCache = new ConcurrentDictionary<string, ICollection<Type>>();
            _candidateTypeNamesByTargetTypeNameCache = new ConcurrentDictionary<string, ConcurrentDictionary<string, IEnumerable<string>>>();
        }

        /// <summary>
        /// Import the types for this library by calling one of the import methods:
        /// <see cref="ImportSingle{TTargetType}"/>, <see cref="ImportSingle{TTargetType,TFactoryType}"/>,
        /// <see cref="ImportFirst{TTargetType}"/>, <see cref="ImportFirst{TTargetType,TFactoryType}"/>,
        /// <see cref="ImportMultiple{TTargetType}"/>, or <see cref="ImportMultiple{TTargetType, TFactoryType}"/>.
        /// </summary>
        public abstract void Bootstrap();

        /// <summary>
        /// Return a metadata object that describes the export operation for a type.
        /// </summary>
        /// <param name="type">The type to get export metadata.</param>
        /// <returns>A metadata object that describes an export operation.</returns>
        protected virtual ExportInfo GetExportInfo(Type type)
        {
            return new ExportInfo(type);
        }

        /// <summary>
        /// Return a collection of metadata objects that correspond to the attributes.
        /// Use the <see cref="Extensions.AsAttributeType{TAttribute}"/> extension method
        /// to convert applicable CustomAttributeData objects to the desired attribyte type.
        /// </summary>
        /// <param name="assemblyAttributes">
        /// The collection of attribute data describing attributes that decorate an assembly.
        /// </param>
        /// <returns>A collection of metadata objects that describe export operations.</returns>
        protected virtual IEnumerable<ExportInfo> GetExportInfos(
            IEnumerable<CustomAttributeData> assemblyAttributes)
        {
            yield break;
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
        /// A callback function to invoke when an implementation of <typeparamref name="TTargetType"/> is created.
        /// </param>
        /// <param name="importName">
        /// The name of this import operation. If not null, exported classes without a matching name are excluded.
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
            ImportSingleType(
                importAction,
                GetImportInfo<TTargetType>(importName, options),
                CreateInstance<TTargetType>);
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/>. When a single
        /// class with a public parameterless constructor is found that implements or
        /// inherits from either <typeparamref name="TTargetType"/> or
        /// <typeparamref name="TFactoryType"/>, then an instance of that class is created. 
        /// If that instance is a <see cref="TTargetType"/>, than that instance will be
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
            ImportSingleType(
                importAction,
                GetImportInfo<TTargetType>(importName, options, typeof(TFactoryType)),
                t => CreateInstance(t, getTarget));
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
            ImportFirstType(
                importAction,
                GetInstances<TTargetType>(importName, options));
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/>. When any
        /// class with a public parameterless constructor is found that implements or
        /// inherits from either <typeparamref name="TTargetType"/> or
        /// <typeparamref name="TFactoryType"/>, then an instance of the highest priority
        /// class is created. If that instance is a <see cref="TTargetType"/>, than that 
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
            ImportFirstType(
                importAction,
                GetInstances(getTarget, importName, options));
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
            importAction(GetInstances<TTargetType>(importName, options).ToList());
        }

        /// <summary>
        /// Imports the type specified by <typeparamref name="TTargetType"/> for many 
        /// implementations. When zero to many classes with a public parameterless 
        /// constructor are found that implements or inherits from either 
        /// <typeparamref name="TTargetType"/> or <typeparamref name="TFactoryType"/>, 
        /// then instances of those classes are created. If an instance is a 
        /// <see cref="TTargetType"/>, than that instance will be passed as part of a 
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
            importAction(GetInstances(getTarget, importName, options).ToList());
        }

        private IEnumerable<TTargetType> GetInstances<TTargetType>(string importName, ImportOptions options) where TTargetType : class
        {
            return GetInstances(
                GetImportInfo<TTargetType>(importName, options),
                CreateInstance<TTargetType>);
        }

        private IEnumerable<TTargetType> GetInstances<TTargetType, TFactoryType>(Func<TFactoryType, TTargetType> getTarget, string importName, ImportOptions options)
            where TTargetType : class
            where TFactoryType : class
        {
            return GetInstances(
                GetImportInfo<TTargetType>(
                    importName,
                    options,
                    typeof(TFactoryType)),
                type => CreateInstance(type, getTarget));
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
            ImportInfo import, Func<Type, TTargetType> createInstance)
            where TTargetType : class
        {
            var candidateTypeNames = GetCandidateTypeNames(import);

            var instance =
                GetPrioritizedGroupsOfCandidateTypes(candidateTypeNames, import)
                    .Select(candidateTypes => ChooseCandidateType(candidateTypes, import))
                    .Select(t => t == null ? null : createInstance(t))
                    .FirstOrDefault();

            if (instance != null)
            {
                importAction(instance);
            }
        }

        private static void ImportFirstType<TTargetType>(
            Action<TTargetType> importAction,
            IEnumerable<TTargetType> instances)
            where TTargetType : class
        {
            var instance = instances.FirstOrDefault();

            if (instance != null)
            {
                importAction(instance);
            }
        }

        private IEnumerable<TTargetType> GetInstances<TTargetType>(
            ImportInfo import,
            Func<Type, TTargetType> createInstance)
            where TTargetType : class
        {
            var candidateTypeNames = GetCandidateTypeNames(import);

            var prioritizedGroupsOfCandidateTypes =
                GetPrioritizedGroupsOfCandidateTypes(candidateTypeNames, import);

            return (
                from candidateTypes in prioritizedGroupsOfCandidateTypes
                from candidateType in candidateTypes
                select createInstance(candidateType))
                .Where(instance => instance != null);
        }

        private static TTargetType CreateInstance<TTargetType>(Type candidateType)
            where TTargetType : class
        {
            try
            {
                var instance = Instantiate(candidateType);

                var target = instance as TTargetType;
                if (target != null)
                {
                    return target;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static TTargetType CreateInstance<TTargetType, TFactoryType>(
            Type candidateType,
            Func<TFactoryType, TTargetType> getTarget)
            where TTargetType : class
            where TFactoryType : class
        {
            try
            {
                var instance = Instantiate(candidateType);

                var factory = instance as TFactoryType;
                if (factory != null)
                {
                    return getTarget(factory);
                }

                var target = instance as TTargetType;
                if (target != null)
                {
                    return target;
                }

                return null;
            }
            catch
            {
                return null;
            }
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
                isPreferredType = GetIsTargetTypeFunc(import, import.FactoryType);
            }

            var prioritizedGroupsOfCandidateTypes =
                candidateTypeNames.Select(GetExportInfo)
                    .Concat(
                        LoadExportInfosFromAssemblyAttributes(
                            import.TargetType,
                            import.Options.DirectoryPaths,
                            import.Options.IncludeTypesFromThisAssembly))
                    .Where(export =>
                        export != null
                        && !export.Disabled
                        && AreCompatible(import, export))
                    .GroupBy(x => x.Priority)
                    .OrderByDescending(g => g.Key)
                    .Select(g =>
                        g.OrderByDescending(export => isPreferredType(export.TargetClass))
                            .ThenBy(export => export.TargetClass.AssemblyQualifiedName)
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
                        && export.Name == uniqueExport.Name
                        && export.Priority == uniqueExport.Priority))
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

            return (export.Name == import.Name || import.Name == null);
        }

        private IEnumerable<ExportInfo> LoadExportInfosFromAssemblyAttributes(
            Type targetType,
            IEnumerable<string> directoryPaths,
            bool includeTypesFromThisAssembly)
        {
            try
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AppDomainOnReflectionOnlyAssemblyResolve;

                return
                    GetAssemblyFiles(directoryPaths)
                        .SelectMany(assemblyFile =>
                            LoadExportInfos(assemblyFile, targetType, includeTypesFromThisAssembly))
                        .ToList();
            }
            finally
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= AppDomainOnReflectionOnlyAssemblyResolve;
            }
        }

        private IEnumerable<ExportInfo> LoadExportInfos(
            string assemblyFile,
            Type targetType,
            bool includeTypesFromThisAssembly)
        {
            try
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);

                if (!includeTypesFromThisAssembly
                    && assembly.FullName == typeof(CompositionRootBase).Assembly.FullName)
                {
                    return Enumerable.Empty<ExportInfo>();
                }

                return
                    GetExportInfos(CustomAttributeData.GetCustomAttributes(assembly))
                        .Where(export => export.TargetClass.AssemblyQualifiedName == targetType.AssemblyQualifiedName);
            }
            catch
            {
                return Enumerable.Empty<ExportInfo>();
            }
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

        private ExportInfo GetExportInfo(string assemblyQualifiedName)
        {
            try
            {
                var type = Type.GetType(assemblyQualifiedName);

                if (type == null)
                {
                    return null;
                }

                return GetExportInfo(type);
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
                    _ => new ConcurrentDictionary<string, IEnumerable<string>>());

            var candidateTypeNames =
                candidateTypeNamesCache.GetOrAdd(
                    import.TargetTypeName,
                    _ =>
                    {
                        var isTargetType = GetIsTargetTypeFunc(import, import.TargetType);

                        if (import.FactoryType != null)
                        {
                            var isFactoryType = GetIsTargetTypeFunc(import, import.FactoryType);
                            var isTargetTypeLocal = isTargetType;

                            isTargetType = type => isTargetTypeLocal(type) || isFactoryType(type);
                        }

                        var candidateTypes =
                            _candidateTypesCache.GetOrAdd(
                                string.Join("|", import.Options.DirectoryPaths),
                                __ => GetCandidateTypes(import.Options.DirectoryPaths));

                        return
                            candidateTypes
                                .Where(isTargetType)
                                .Select(t => t.AssemblyQualifiedName)
                                .ToList();
                    });

            return candidateTypeNames;
        }

        private static Func<Type, bool> GetIsTargetTypeFunc(ImportInfo import, Type targetType)
        {
            var targetTypeName = targetType.AssemblyQualifiedName;

            if (targetTypeName == null)
            {
                return typeInQuestion => false;
            }

            if (targetType.IsInterface)
            {
                return typeInQuestion =>
                    (typeInQuestion.IsPublic || import.Options.AllowNonPublicClasses)
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

        private ICollection<Type> GetCandidateTypes(IEnumerable<string> directoryPaths)
        {
            try
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AppDomainOnReflectionOnlyAssemblyResolve;
                return GetAssemblyFiles(directoryPaths).SelectMany(LoadCandidateTypes).ToList();
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

        private static IEnumerable<string> GetAssemblyFiles(IEnumerable<string> directoryPaths)
        {
            foreach (var directoryPath in directoryPaths)
            {
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

        private static IEnumerable<Type> LoadCandidateTypes(string assemblyFile)
        {
            try
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);

                // Don't look here.
                if (assembly.FullName == typeof(CompositionRootBase).Assembly.FullName)
                {
                    return Enumerable.Empty<Type>();
                }

                return
                    assembly.GetTypes()
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

        private static bool HasDefaultishConstructor(Type type)
        {
            return
                type.GetConstructor(Type.EmptyTypes) != null
                || type.GetConstructors().Any(ctor => ctor.GetParameters().All(HasDefaultValue));
        }

        private static bool HasDefaultValue(ParameterInfo parameter)
        {
            const ParameterAttributes hasDefaultValue =
                ParameterAttributes.HasDefault | ParameterAttributes.Optional;

            return (parameter.Attributes & hasDefaultValue) == hasDefaultValue;
        }

        private class ImportInfo
        {
            private readonly string _name;
            private readonly Type _targetType;
            private readonly Type _factoryType;
            private readonly ImportOptions _options;

            public ImportInfo(
                string name,
                Type targetType,
                Type factoryType,
                ImportOptions options)
            {
                _name = name;
                _targetType = targetType;
                _factoryType = factoryType;
                _options = options;
            }

            internal string Name { get { return _name; } }
            internal Type TargetType { get { return _targetType; } }
            internal Type FactoryType { get { return _factoryType; } }
            internal ImportOptions Options { get { return _options; } }

            internal string TargetTypeName
            {
                get
                {
                    return TargetType.AssemblyQualifiedName;
                }
            }

            internal string FactoryTypeName
            {
                get
                {
                    return
                        _factoryType == null
                            ? null
                            : _factoryType.AssemblyQualifiedName;
                }
            }
        }
    }
}