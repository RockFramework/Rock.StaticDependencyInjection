using System;
using System.Text;

namespace Rock.StaticDependencyInjection
{
    internal class ImportInfo
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

        public string Name { get { return _name; } }
        public Type TargetType { get { return _targetType; } }
        public Type FactoryType { get { return _factoryType; } }
        public ImportOptions Options { get { return _options; } }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat(@"{{
  ""Name"": {0},
  ""TargetType"": {1},
  ""FactoryType"": {2},
  ""Options"": {3}
}}", Name, TargetType, FactoryType, Options.ToString("  "));

            return sb.ToString();
        }

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