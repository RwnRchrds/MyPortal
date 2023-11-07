using System;
using System.Collections;

namespace MyPortal.Logic.Extensions
{
    public static class BitArrayExtensions
    {
        public static byte[] ToBytes(this BitArray bitArray)
        {
            double bitCount = (double)bitArray.Length / 8;
            int bytesRequired = Convert.ToInt32(Math.Ceiling(bitCount));
            byte[] bytes = new byte[bytesRequired];
            bitArray.CopyTo(bytes, 0);
            return bytes;
        }
    }
}