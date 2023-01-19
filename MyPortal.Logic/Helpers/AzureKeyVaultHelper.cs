using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace MyPortal.Logic.Helpers;

public class AzureKeyVaultHelper
{
    public static string GetSecret(string keyVaultName, string secretName)
    {
        var keyVaultUri = $"https://{keyVaultName}.vault.azure.net";
        var keyVaultSecret = secretName;
        var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

        var response = client.GetSecret(keyVaultSecret);
        var secret = response.Value;
        return secret.Value;
    }

    public static async Task<string> GetSecretAsync(string keyVaultName, string secretName)
    {
        var keyVaultUri = $"https://{keyVaultName}.vault.azure.net";
        var keyVaultSecret = secretName;
        var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

        var response = await client.GetSecretAsync(keyVaultSecret);
        var secret = response.Value;
        return secret.Value;
    }
}