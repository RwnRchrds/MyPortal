using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Helpers;

public static class CryptoHelper
{
    public static string GenerateEncryptionKey()
    {
        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.GenerateKey();

            var key = aes.Key;

            return Convert.ToBase64String(key);
        }
    }
    
    public static async Task<EncryptionResult> EncryptAsync(string text, string keyBase64)
    {
        var data = Encoding.UTF8.GetBytes(text);

        return await EncryptAsync(data, keyBase64);
    }

    public static async Task<EncryptionResult> EncryptAsync(byte[] data, string keyBase64)
    {
        var key = Convert.FromBase64String(keyBase64);

        return await EncryptAesAsync(data, key);
    }
    
    public static async Task<string> DecryptAsync(string dataBase64, string keyBase64, string vectorBase64)
    {
        var data = Convert.FromBase64String(dataBase64);

        return Encoding.UTF8.GetString(await DecryptAsync(data, keyBase64, vectorBase64));
    }

    public static async Task<byte[]> DecryptAsync(byte[] encryptedData, string keyBase64, byte[] vector)
    {
        var key = Convert.FromBase64String(keyBase64);

        return await DecryptAesAsync(encryptedData, key, vector);
    }

    public static async Task<byte[]> DecryptAsync(byte[] encryptedData, string keyBase64, string vectorBase64)
    {
        var key = Convert.FromBase64String(keyBase64);
        var iv = Convert.FromBase64String(vectorBase64);

        return await DecryptAesAsync(encryptedData, key, iv);
    }

    private static async Task<EncryptionResult> EncryptAesAsync(byte[] plainData, byte[] key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.GenerateIV();

            var encryptor = aes.CreateEncryptor();

            var encryptedData = await PerformCryptoTransformAsync(encryptor, plainData);

            return new EncryptionResult(encryptedData, aes.IV);
        }
    }

    private static async Task<byte[]> PerformCryptoTransformAsync(ICryptoTransform cryptoTransform, byte[] data)
    {
        byte[] transformedData;
        
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                await cs.WriteAsync(data);
            }
            
            transformedData = ms.ToArray();

            return transformedData;
        }
    }

    private static async Task<byte[]> DecryptAesAsync(byte[] encryptedData, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor();

            var plainData = await PerformCryptoTransformAsync(decryptor, encryptedData);

            return plainData;
        }
    }
}