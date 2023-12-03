using System.Security.Cryptography;
using System.Text;

namespace LBSJourneyWebService.Models;

public class HashUtility
{
    public static String hashData(String source, String salt)
    {
        SHA512 sha = SHA512.Create();
        String sourceData = source + salt;
        Encoding encoding = Encoding.UTF8;
        Byte[] sourceByte = encoding.GetBytes(sourceData);
        Byte[] hashData = sha.ComputeHash(sourceByte);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashData)
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }
}