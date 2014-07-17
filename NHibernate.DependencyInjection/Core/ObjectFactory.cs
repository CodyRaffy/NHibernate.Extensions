using NHibernate.Bytecode;

namespace NHibernate.DependencyInjection.Core
{
    public class ObjectsFactory : IObjectsFactory
    {
        private readonly ActivatorObjectsFactory _activatorObjectFactory;
        private readonly IInjector _objectInjector;

        public ObjectsFactory()
        {
            _activatorObjectFactory = new ActivatorObjectsFactory();
        }

        public ObjectsFactory(IInjector objectInjector)
        {
            _activatorObjectFactory = new ActivatorObjectsFactory();
            _objectInjector = objectInjector;
        }

        public object CreateInstance(System.Type type, params object[] ctorArgs)
        {
            return _activatorObjectFactory.CreateInstance(type, ctorArgs);
        }

        public object CreateInstance(System.Type type, bool nonPublic)
        {
            return _activatorObjectFactory.CreateInstance(type, nonPublic);
        }

        public object CreateInstance(System.Type type)
        {
            if (_objectInjector != null)
            {
                var constructorParameters = _objectInjector.GetConstructorParameters(type);
                if (constructorParameters != null) return CreateInstance(type, constructorParameters);
            }
            return _activatorObjectFactory.CreateInstance(type);
        }
    }
}