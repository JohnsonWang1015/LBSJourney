using System.Security.Cryptography;
using System.Text;

namespace LBSJourney.Models
{
    public class AppSecurity
    {
        private String _salt;
        public AppSecurity(String salt)
        {
            _salt = salt;
        }
        public String hash512(String source)
        {
            Byte[] sourceByte = System.Text.Encoding.UTF8.GetBytes(source+_salt);
            SHA512 sha = SHA512.Create();
            Byte[] shaBytes = sha.ComputeHash(sourceByte);
            StringBuilder sb = new StringBuilder();
            foreach(Byte b in shaBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
