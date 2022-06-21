using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace YFTools.CommonComponents.Extensions;

public static class StringExtension
{
    private const string EncryptKey = "_hS2@17_";

    private const string EncryptIv = "HencE#66";

    public static string Md5(this string str, string salt = "", bool toUpper = true, Md5Category category = Md5Category.Base32)
    {
        return str.Md5(Encoding.UTF8, salt, toUpper, category);
    }

    public static string Md5(this string str, Encoding encoding, string salt = "", bool toUpper = true, Md5Category category = Md5Category.Base32)
    {
        if (string.IsNullOrWhiteSpace(str)) return string.Empty;

        var result = encoding.GetBytes($"{str}{salt}");
        var md5 = MD5.Create();
        var output = md5.ComputeHash(result);
        string s;
        switch (category)
        {
            case Md5Category.Base16:
                s = BitConverter.ToString(output).Replace("-", "");
                return toUpper ? s.ToUpper() : s;
            case Md5Category.Base32:
                var ret = "";
                foreach (var t in output)
                {
                    ret += Convert.ToString(t, 16).PadLeft(2, '0');
                }
                s = ret.PadLeft(32, '0');
                return toUpper ? s.ToUpper() : s;
            default:
                return string.Empty;
        }
    }

    public static string EncryptDes(this string str, string key = "", string iv = "", bool toBase64 = true)
    {
        return str.EncryptDes(Encoding.UTF8, key, iv, toBase64);
    }

    public static string EncryptDes(this string str, Encoding encoding, string key = "", string iv = "", bool toBase64 = true)
    {
        if (string.IsNullOrWhiteSpace(str)) return string.Empty;

        if (string.IsNullOrWhiteSpace(key) || key.Length != 8)
            key = EncryptKey;
        if (string.IsNullOrWhiteSpace(iv) || iv.Length != 8)
            iv = EncryptIv;
        try
        {
            var bytes = encoding.GetBytes(str);
            var des = DES.Create();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(iv);
            des.Mode = CipherMode.CBC;
            var encrypt = des.CreateEncryptor();
            var result = encrypt.TransformFinalBlock(bytes, 0, bytes.Length);
            return toBase64 ? Convert.ToBase64String(result) : Convert.ToString(result);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string DecryptDes(this string str, string key = "", string iv = "", bool fromBase64 = true)
    {
        return str.DecryptDes(Encoding.UTF8, key, iv, fromBase64);
    }

    public static string DecryptDes(this string str, Encoding encoding, string key = "", string iv = "", bool fromBase64 = true)
    {
        if (string.IsNullOrWhiteSpace(str)) return string.Empty;

        if (string.IsNullOrWhiteSpace(key) || key.Length != 8)
            key = EncryptKey;
        if (string.IsNullOrWhiteSpace(iv) || iv.Length != 8)
            iv = EncryptIv;
        try
        {
            var bytes = fromBase64 ? Convert.FromBase64String(str) : encoding.GetBytes(str);
            var des = DES.Create();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(iv);
            des.Mode = CipherMode.CBC;
            var encrypt = des.CreateDecryptor();
            var result = encrypt.TransformFinalBlock(bytes, 0, bytes.Length);
            return encoding.GetString(result);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string EncryptAes(this string str, string key = "", string iv = "", bool toBase64 = true)
    {
        return str.EncryptAes(Encoding.UTF8, key, iv, toBase64);
    }

    public static string EncryptAes(this string str, Encoding encoding, string key = "", string iv = "", bool toBase64 = true)
    {
        if (string.IsNullOrWhiteSpace(str)) return string.Empty;

        if (string.IsNullOrWhiteSpace(key) || key.Length != 8)
            key = EncryptKey;
        try
        {
            var toEncryptArray = encoding.GetBytes(str);
            var aes = Aes.Create();
            aes.Key = encoding.GetBytes(key.Md5());
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            var cTransform = aes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return toBase64 ? Convert.ToBase64String(resultArray) : Convert.ToString(resultArray);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string DecryptAes(this string str, string key = "", string iv = "", bool fromBase64 = true)
    {
        return str.DecryptAes(Encoding.UTF8, key, iv, fromBase64);
    }

    public static string DecryptAes(this string str, Encoding encoding, string key = "", string iv = "", bool fromBase64 = true)
    {
        if (string.IsNullOrWhiteSpace(str)) return string.Empty;

        if (string.IsNullOrWhiteSpace(key) || key.Length != 8)
            key = EncryptKey;
        try
        {
            var toEncryptArray = fromBase64 ? Convert.FromBase64String(str) : encoding.GetBytes(str);
            var aes = Aes.Create();
            aes.Key = encoding.GetBytes(key.Md5());
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            var cTransform = aes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return encoding.GetString(resultArray);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string ToUnicode(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return str;

        var bytes = Encoding.Unicode.GetBytes(str);
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < bytes.Length; i += 2)
        {
            stringBuilder.Append($@"\u{bytes[i + 1].ToString("x").PadLeft(2, '0')}{bytes[i].ToString("x").PadLeft(2, '0')}");
        }
        return stringBuilder.ToString();
    }

    public static string FromUnicode(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return str;

        try
        {
            var result = "";
            var strList = str.Split('u');
            for (var i = 1; i < strList.Length; i++)
            {
                result += (char)int.Parse(strList[i], NumberStyles.HexNumber);
            }
            return result;
        }
        catch
        {
            return str;
        }
    }
}

public enum Md5Category
{
    Base16,
    Base32
}