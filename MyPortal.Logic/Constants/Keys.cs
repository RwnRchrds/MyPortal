namespace MyPortal.Logic.Constants;

public static class Keys
{
    public static readonly string DefaultEncryptionKey = @"v9y$B&E)H+MbQeThWmZq4t7w!z%C*F-J";

    internal static readonly string ConfigDatabaseProviderKey = @"DataSource:DatabaseProvider";
    internal static readonly string ConfigConnectionStringKey = @"DataSource:ConnectionString";
    internal static readonly string ConfigConnectionStringSourceKey = @"DataSource:ConnectionStringSource";
    internal static readonly string ConfigFileProviderKey = @"DataSource:FileProvider";
    internal static readonly string ConfigFileEncryptionKey = @"DataSource:FileEncryptionKey";
    internal static readonly string ConfigFileEncryptionKeySourceKey = @"DataSource:FileEncryptionKeySource";
    internal static readonly string ConfigDirectoryPathKey = @"MyPortal:DirectoryPath";

    internal const string SqlServerProviderKey = @"mssql";

    internal const string LocalFileProviderKey = @"local";
    internal const string GoogleFileProviderKey = @"google";
}