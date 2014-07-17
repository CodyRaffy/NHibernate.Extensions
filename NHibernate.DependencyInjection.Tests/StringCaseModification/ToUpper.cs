namespace NHibernate.DependencyInjection.Tests.StringCaseModification
{
    public class ToUpper : IStringCaseModifier
    {
        public string ModifyCase(string subject)
        {
            return subject == null ? null : subject.ToUpperInvariant();
        }
    }
}
