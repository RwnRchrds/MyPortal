using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;

[assembly: InternalsVisibleTo("MyPortal.Tests")]

namespace MyPortal.Logic.Configuration
{
    public class Configuration
    {
        public static Configuration Instance;

        private string _connectionString;

        internal static bool CheckConfiguration(bool testConnection = false)
        {
            if (Instance == null)
            {
                throw new ConfigurationException("The configuration has not been set.");
            }

            if (testConnection)
            {
                try
                {
                    TestConnection(Instance.ConnectionString);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        internal static void CreateTestInstance(string installLocation)
        {
            if (Instance != null)
            {
                throw new ConfigurationException("A configuration has already been added.");
            }

            Instance = new Configuration();

            Instance.InstallLocation = installLocation;
            Instance.FileEncryptionKey = CryptoHelper.GenerateEncryptionKey();
        }

        internal static void CreateInstance(ConfigBuilder builder)
        {
            if (Instance != null)
            {
                throw new ConfigurationException("A configuration has already been added.");
            }

            TestConnection(builder.ConnectionString);

            Instance = new Configuration
            {
                DatabaseProvider = builder.DatabaseProvider,
                FileProvider = builder.FileProvider,
                ConnectionString = builder.ConnectionString
            };
        }

        private static void TestConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);

            conn.Open();
            conn.Close();
        }

        internal string InstallLocation { get; private set; }

        public DatabaseProvider DatabaseProvider { get; private set; }

        public string ConnectionString
        {
            get => _connectionString;
            private set
            {
                TestConnection(value);
                _connectionString = value;
            }
        }

        public string FileEncryptionKey { get; private set; }

        public FileProvider FileProvider { get; private set; }
    }
}