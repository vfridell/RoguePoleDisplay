using NHibernate;
using NHibernate.Cfg;

namespace RoguePoleDisplay.Repositories
{
    class SessionFactory
    {
        private static Configuration _config;
        private static ISessionFactory _sessionFactory;

        internal static ISessionFactory Get()
        {
            if (_sessionFactory == null)
            {
                _config = new Configuration();
                string[] files = typeof(SessionFactory).Assembly.GetManifestResourceNames();
                _config.Configure(typeof(SessionFactory).Assembly, @"RoguePoleDisplay.hibernate.cfg.xml");
                _config.AddAssembly(typeof(SessionFactory).Assembly);
                _sessionFactory = _config.BuildSessionFactory();
            }
            return _sessionFactory;
        }
    }
}
