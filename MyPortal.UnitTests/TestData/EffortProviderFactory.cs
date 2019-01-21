using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace MyPortal.UnitTests.TestData
{
    public class EffortProviderFactory : IDbConnectionFactory
    {
        private static readonly object _lock = new object();

        private static DbConnection _connection;

        public static void ResetDb()
        {
            lock (_lock)
            {
                _connection = null;
            }
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            lock (_lock)
            {
                if (_connection == null)
                {
                    _connection = Effort.DbConnectionFactory.CreateTransient();
                }

                return _connection;
            }
        }
    }
}
