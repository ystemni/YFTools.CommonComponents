using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace YFTools.CommonComponents.Helpers;

public static class RandomHelper
{
    private static int StrongRandom(int max)
    {
        var randomByte = new byte[4];
        var gen = RandomNumberGenerator.Create();
        gen.GetBytes(randomByte);
        var value = BitConverter.ToInt32(randomByte, 0);
        value %= (max + 1);
        if (value < 0) value = -value;
        return value;
    }

    public static string RandomString(int len, RandomCategory category = RandomCategory.Default)
    {
        if (len <= 0) return string.Empty;

        var builder = new StringBuilder();
        var chars = category switch
        {
            RandomCategory.Simple => "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ".ToArray(),
            RandomCategory.Number => "0123456789".ToArray(),
            _ => "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()".ToArray()
        };
        for (var i = 0; i < len; i++)
        {
            builder.Append(chars[StrongRandom(chars.Length - 1)]);
        }
        return builder.ToString();
    }
}

public enum RandomCategory
{
    Default,
    Simple,
    Number
}