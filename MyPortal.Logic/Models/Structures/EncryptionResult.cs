using System;

namespace MyPortal.Logic.Models.Structures;

public class EncryptionResult
{
    internal EncryptionResult(byte[] data, byte[] iv)
    {
        Data = data;
        Iv = iv;
    }

    public byte[] Data { get; private set; }
    public byte[] Iv { get; private set; }

    public string DataString => Convert.ToBase64String(Data);
    public string IvString => Convert.ToBase64String(Iv);
}