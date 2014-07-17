
namespace NHibernate.DependencyInjection.Tests.StringCaseModification
{
    public interface IStringCaseModifier
    {
        string ModifyCase(string subject);
    }
}