using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;

namespace NHibernateUnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        FluentConfiguration Configuration { get; }
        ISessionFactory SessionFactory { get; }
        ISession CurrentSession { get; set; }

        IUnitOfWork Create();
        void DisposeUnitOfWork(UnitOfWorkImplementor adapter);
    }
}