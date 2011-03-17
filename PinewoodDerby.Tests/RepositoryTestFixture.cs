using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernateUnitOfWork;
using PinewoodDerby.DataAccess.Models;
using PinewoodDerby.DataAccess.Repository;

namespace PinewoodDerby.Tests
{
    [TestFixture]
    class RepositoryTestFixture
    {
        public void Sample()
        {
            using (UnitOfWork.Start())
            {
                IRacerRepository repository = new RacerRepository();

                var racer = repository.GetById(1);
            }
        }
    }
}
