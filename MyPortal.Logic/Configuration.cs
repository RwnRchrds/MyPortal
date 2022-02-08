using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Data.SqlClient;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Configuration;

[assembly:InternalsVisibleTo("MyPortal.Tests")]
namespace MyPortal.Logic
{
    public class Configuration
    {
        public static Configuration Instance = new Configuration();

        private string _installLocation;
        private string _connectionString;
        private bool _isSetUp;
        private FileProvider _fileProvider;
        private GoogleConfig _googleConfig;

        private void TestConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);

            conn.Open();
        }

        public string InstallLocation
        {
            get { return _installLocation; }
            set { _installLocation = value; }
        }

        public bool IsSetUp
        {
            get { return _isSetUp; }
            set { _isSetUp = value; }
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
