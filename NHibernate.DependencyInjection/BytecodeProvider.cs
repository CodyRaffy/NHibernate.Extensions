using System.Configuration;
using System.Security.Permissions;
using NHibernate.Bytecode;
using NHibernate.DependencyInjection.Core;
using NHibernate.Properties;

namespace NHibernate.DependencyInjection
{
    /// <summary>
    /// Custom bytecode provider for NHibernate. 
    /// Creates verifiable proxy types for medium trust environments.
    /// Supports dependency injection scenarios.
    /// All credit goes to the NHibernate / LinFu developers - most of this code is theirs with only minor
    /// modifications for bug fixes, enhancements.
    /// Required permissions above default medium trust: 
    /// ConfigurationPermission -> unrestricted
    /// ReflectionPermission -> RestrictedMemberAccess, ReflectionEmit
    /// </summary>
    [ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
    [ReflectionPermission(SecurityAction.Demand, MemberAccess = true, ReflectionEmit = true, RestrictedMemberAccess = true)]
    public class BytecodeProvider : AbstractBytecodeProvider
    {
        internal static IInjector EntityInjector { get; private set; }
        private static IObjectsFactory _objectsFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        public BytecodeProvider() : this(null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="injector">Provide an IInjector implementation to support dependency injection for entities and objects</param>
        public BytecodeProvider(IInjector injector)
        {
            EntityInjector = injector ?? new DefaultInjector();

            _objectsFactory = injector != null
                ? new ObjectsFactory(injector)
                : new ObjectsFactory();
        }

        public override IObjectsFactory ObjectsFactory
        {
            get { return _objectsFactory; }
        }

        /// <summary>
        /// Gets the reflection optimizer
        /// </summary>
        /// <param name="clazz">entity class</param>
        /// <param name="getters">getters</param>
        /// <param name="setters">setters</param>
        /// <returns>the reflection optimizer</returns>
        public override IReflectionOptimizer GetReflectionOptimizer(System.Type clazz, IGetter[] getters, ISetter[] setters)
        {
            return new ReflectionOptimizer(clazz, getters, setters);
        }

        /// <summary>
        /// ProxyFactoryFactory
        /// </summary>
        public override IProxyFactoryFactory ProxyFactoryFactory
        {
            get { return new ProxyFactoryFactory(); }
        }
    }
}