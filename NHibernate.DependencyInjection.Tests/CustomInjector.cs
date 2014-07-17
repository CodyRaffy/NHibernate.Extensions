using NHibernate.DependencyInjection.Tests.Model;
using NHibernate.DependencyInjection.Tests.StringCaseModification;
using NHibernate.DependencyInjection.Tests.UserType;

namespace NHibernate.DependencyInjection.Tests
{
    public class CustomInjector : IInjector
    {
        public object[] GetConstructorParameters(System.Type type)
        {
            if (type == typeof(DependencyInjectionCat)) return new object[] { new CatBehavior() };
            if (type == typeof (SpecialCaseString)) return new object[] {new ToUpper()};

            return null;
        }
    }
}