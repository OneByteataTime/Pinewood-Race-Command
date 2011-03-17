using System;
using System.IO;
using System.Xml;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PinewoodDerby.DataAccess.Models;

namespace NHibernateUnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private const string Default_HibernateConfig = "hibernate.cfg.xml";
        private const string DEFAULT_PINEWOOD = @"Data Source=C:\Users\Steve\Documents\Visual Studio 2008\Projects\Pinewood Race Command\PinewoodDerby.DataAccess\Database\PinewoodDerby.s3db;Version=3;";

        private static ISession _currentSession;
        private ISessionFactory _sessionFactory;
        private FluentConfiguration _configuration;

        internal UnitOfWorkFactory()
        { }

        public IUnitOfWork Create()
        {
            ISession session = CreateSession();
            session.FlushMode = FlushMode.Commit;
            _currentSession = session;
            return new UnitOfWorkImplementor(this, session);
        }

        public FluentConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = Fluently.Configure()
                                                    .Database(
                                                        SQLiteConfiguration.Standard
                                                        .ConnectionString(DEFAULT_PINEWOOD)
                                                    )
                                                    .Mappings(m =>
                                                        m.FluentMappings.AddFromAssemblyOf<Racer>());

                    //_configuration = new Configuration();
                    //string hibernateConfig = Default_HibernateConfig;
                    ////if not rooted, assume path from base directory
                    //if (Path.IsPathRooted(hibernateConfig) == false)
                    //    hibernateConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, hibernateConfig);
                    //if (File.Exists(hibernateConfig))
                    //    _configuration.Configure(new XmlTextReader(hibernateConfig));
                }
                return _configuration;
            }
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    _sessionFactory = Configuration.BuildSessionFactory();
                return _sessionFactory;
            }
        }

        public ISession CurrentSession
        {
            get
            {
                if (_currentSession == null)
                    throw new InvalidOperationException("You are not in a unit of work.");
                return _currentSession;
            }
            set { _currentSession = value; }
        }

        public void DisposeUnitOfWork(UnitOfWorkImplementor adapter)
        {
            CurrentSession = null;
            UnitOfWork.DisposeUnitOfWork(adapter);
        }

        private ISession CreateSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}