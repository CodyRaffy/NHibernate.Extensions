using Iesi.Collections.Generic;

namespace NHibernate.DependencyInjection.Tests.Model
{
    public class BasicCatWithUserTypeRequiringDependencyInjection
    {
        public virtual int Id { get; protected set; }

        public virtual string Name { get; set; }

        private ISet<BasicCatWithUserTypeRequiringDependencyInjection> _kittens = new HashedSet<BasicCatWithUserTypeRequiringDependencyInjection>();
        public virtual ISet<BasicCatWithUserTypeRequiringDependencyInjection> Kittens
        {
            get { return _kittens; }
            set { _kittens = value; }
        }

        public virtual BasicCatWithUserTypeRequiringDependencyInjection Parent { get; set; }
    }
}
