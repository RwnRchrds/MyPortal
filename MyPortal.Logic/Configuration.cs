using System;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Configuration;

[assembly:InternalsVisibleTo("MyPortal.Tests")]
namespace MyPortal.Logic
{
    public class Configuration
    {
        public static Configuration Instance;

        private string _installLocation;
        private string _connectionString;
        private string _fileEncryptionKey;
        private FileProvider _fileProvider;
        private GoogleConfig _googleConfig;
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

        internal static void CreateInstance(string databaseProvider, string connectionString)
        {
            if (Instance != null)
            {
                throw new ConfigurationException("A configuration has already been added.");
            }

            DatabaseProvider databaseProviderValue;

            switch (databaseProvider.ToLower())
            {
                case "mssql":
                    databaseProviderValue = DatabaseProvider.MsSqlServer;
                    break;
                case "mysql":
                    databaseProviderValue = DatabaseProvider.MySql;
                    break;
                default:
                    throw new ArgumentException($"The database provider '{databaseProvider}' is invalid.",
                        nameof(databaseProvider));
            }

            TestConnection(connectionString);

            Instance = new Configuration();

            Instance.DatabaseProvider = databaseProviderValue;
            Instance.ConnectionString = connectionString;
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
            set
            {
                _fileEncryptionKey = value;
            }
        }

        internal FileProvider FileProvider
        {
            get { return _fileProvider; }
            set { _fileProvider = value; }
        }

        internal GoogleConfig GoogleConfig
        {
            get { return _googleConfig; }
            set { _googleConfig = value; }
        }
    }
}
