using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;

[assembly:InternalsVisibleTo("MyPortal.Tests")]
namespace MyPortal.Logic.Configuration
{
    public class Configuration
    {
        public static Configuration Instance;

        private string _installLocation;
        private string _connectionString;
        private string _fileEncryptionKey;
        private FileProvider _fileProvider;
        private DatabaseProvider _databaseProvider;

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

            Instance = new Configuration();

            Instance.DatabaseProvider = builder.DatabaseProvider;
            Instance.FileProvider = builder.FileProvider;
            Instance.ConnectionString = builder.ConnectionString;
        }

        private static void TestConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);

            conn.Open();
            conn.Close();
        }

        internal string InstallLocation
        {
            get { return _installLocation; }
            set { _installLocation = value; }
        }

        public DatabaseProvider DatabaseProvider
        {
            get { return _databaseProvider; }
            private set
            {
                _databaseProvider = value;
            }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            private set
            {
                TestConnection(value);
                _connectionString = value;
            }
        }

        public string FileEncryptionKey
        {
            get { return _fileEncryptionKey; }
            internal set
            {
                _fileEncryptionKey = value;
            }
        }

        public FileProvider FileProvider
        {
            get { return _fileProvider; }
            internal set { _fileProvider = value; }
        }
    }
}
