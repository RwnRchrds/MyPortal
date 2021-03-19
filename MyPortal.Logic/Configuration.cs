using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Configuration;

namespace MyPortal.Logic
{
    public class Configuration
    {
        public static Configuration Instance = new Configuration();

        private string _installLocation;
        private string _tokenKey;
        private string _connectionString;
        private FileProvider _fileProvider;
        private GoogleConfig _googleConfig;

        private void TestConnection(string connectionString)
        {
            try
            {
                var conn = new SqlConnection(connectionString);

                conn.Open();
            }
            catch (Exception e)
            {
                throw new ConnectionStringException(@"Could not contact database with the specified connection string.");
            }
        }

        public string InstallLocation
        {
            get { return _installLocation; }
            set { _installLocation = value; }
        }

        public string TokenKey
        {
            get { return _tokenKey; }
            set { _tokenKey = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                TestConnection(value);
                _connectionString = value;
            }
        }

        public FileProvider FileProvider
        {
            get { return _fileProvider; }
            set { _fileProvider = value; }
        }

        public GoogleConfig GoogleConfig
        {
            get { return _googleConfig; }
            set { _googleConfig = value; }
        }
    }
}
