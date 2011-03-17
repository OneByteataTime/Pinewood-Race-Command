using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateUnitOfWork;
using NHibernate;
using PinewoodDerby.DataAccess.Repository;

namespace PinewoodDerby.DataAccess.Repository
{
    public class Repository<T> : IRepository<T>
    {
        public ISession Session { get { return UnitOfWork.CurrentSession; } }

        public T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public ICollection<T> FindAll()
        {
            return Session.CreateCriteria(typeof(T)).List<T>();
        }

        public void Add(T item)
        {
            Session.Save(item);
        }

        public void Remove(T item)
        {
            Session.Delete(item);
        }
    }
}
