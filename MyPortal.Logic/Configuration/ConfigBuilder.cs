using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Configuration;

public class ConfigBuilder
{
    internal ConfigBuilder(string connectionString)
    {
        DatabaseProvider = DatabaseProvider.MsSqlServer;
        ConnectionString = connectionString;
        FileProvider = FileProvider.Local;
        FileEncryptionKey = Keys.DefaultEncryptionKey;
        DirectoryPath = Directory.GetCurrentDirectory();
    }

    public void FromConfig(IConfiguration config)
    {
        var databaseProvider = config.GetSection(Keys.ConfigDatabaseProviderKey);
        if (databaseProvider.Exists())
        {
            switch (databaseProvider.Value)
            {
                case Keys.SqlServerProviderKey:
                    DatabaseProvider = DatabaseProvider.MsSqlServer;
                    break;
                default:
                    throw new ConfigurationException($"The database provider '{databaseProvider.Value}' is not supported.");
            }
        }
        
        var connectionStringVerbatim = config.GetSection(Keys.ConfigConnectionStringKey);
        if (connectionStringVerbatim.Exists())
        {
            ConnectionString = connectionStringVerbatim.Value;
        }
        else
        {
            var connectionStringSource = config.GetSection(Keys.ConfigConnectionStringSourceKey);
            if (connectionStringSource.Exists())
            {
                ConnectionString = GetSecret(config, connectionStringSource.Value, "cs");
            }
        }

        var fileProvider = config.GetSection(Keys.ConfigFileProviderKey);
        if (fileProvider.Exists())
        {
            switch (fileProvider.Value)
            {
                case Keys.LocalFileProviderKey:
                    FileProvider = FileProvider.Local;
                    break;
                case Keys.GoogleFileProviderKey:
                    FileProvider = FileProvider.GoogleDrive;
                    break;
                default:
                    throw new ConfigurationException($"The file provider '{fileProvider.Value}' is not supported.");
            }
        }

        var fileEncryptionKeyVerbatim = config.GetSection(Keys.ConfigFileEncryptionKey);
        if (fileEncryptionKeyVerbatim.Exists())
        {
            FileEncryptionKey = fileEncryptionKeyVerbatim.Value;
        }
        else
        {
            var fileEncryptionKeySource = config.GetSection(Keys.ConfigFileEncryptionKeySourceKey);
            if (fileEncryptionKeySource.Exists())
            {
                FileEncryptionKey = GetSecret(config, fileEncryptionKeySource.Value, "fek");
            }
        }

        var directoryPath = config.GetSection(Keys.ConfigDirectoryPathKey);
        if (directoryPath.Exists())
        {
            DirectoryPath = directoryPath.Value;
        }
    }
    
    private static string GetSecret(IConfiguration config, string configKey, string secretName)
    {
        var secretSource = config[configKey];

        if (secretSource.ToLower() == "azure")
        {
            var keyVaultName = Environment.GetEnvironmentVariable("MYPORTAL_KEYVAULT");
            var keyVaultSecret = Environment.GetEnvironmentVariable($"MYPORTAL_{secretName.ToUpper()}");

            var secret = AzureKeyVaultHelper.GetSecret(keyVaultName, keyVaultSecret);

            return secret;
        }   
            
        if (secretSource.ToLower() == "environment")
        {
            var secret = Environment.GetEnvironmentVariable($"MYPORTAL_{secretName.ToUpper()}");
                
            return secret;
        }
            
        return secretSource;
    }

    public DatabaseProvider DatabaseProvider { get; set; }

    public FileProvider FileProvider { get; set; }

    public string FileEncryptionKey { get; set; }

    public string ConnectionString { get; set; }

    public string DirectoryPath { get; set; }

    internal void Build()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            throw new ConfigurationException("No database connection details have been provided.");
        }
        
        Configuration.CreateInstance(this);
    }
}