using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace MyPortal.Database.Helpers
{
    public static class DatabaseHelper
    {
        public static async Task<bool> TryGetApplicationLock(DbTransaction transaction, string name, int lockTimeout = 0)
        {
            int returnCode = await transaction.Connection.ExecuteScalarAsync<int>($@"DECLARE @RC INT
Exec @RC =sp_getapplock @Resource='{name}', @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout = {lockTimeout}
SELECT @RC [return code]", transaction: transaction);

            if (returnCode < 0)
            {
                return false;
            }

            return true;
        }
    }
}