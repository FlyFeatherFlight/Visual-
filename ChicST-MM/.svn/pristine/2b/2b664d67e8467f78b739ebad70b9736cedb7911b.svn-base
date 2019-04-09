
using System.Security.Cryptography;
using System.Text;

namespace ChicST_MM.WEB.Utils
{
    public class MD5Helper
    {
        /// <summary>
        /// 使用MD5加密（Added by Yangdong 2009-12-01）
        /// </summary>
        /// <param name="strIN">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string pw)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(StringToByte(pw));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();

        }
        public static byte[] StringToByte(string strKey)
        {
            byte[] tmpByte = Encoding.Default.GetBytes(strKey);
            return tmpByte;

        }
    }
}