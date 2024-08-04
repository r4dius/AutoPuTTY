using System.Security.Cryptography;
using System.Text;

namespace AutoPuTTY
{
    class cryptVNC
    {
        public static string EncryptPassword(string passwd)
        {
            // Use standard VNC Server Key
            byte[] rawKey = new byte[8];
            rawKey[0] = 23;
            rawKey[1] = 82;
            rawKey[2] = 107;
            rawKey[3] = 6;
            rawKey[4] = 35;
            rawKey[5] = 78;
            rawKey[6] = 88;
            rawKey[7] = 7;
            // revert it
            rawKey = FixDESBug(rawKey);
            byte[] Passwd_Bytes = new byte[8];
            _ = passwd.Length >= 8
                ? Encoding.ASCII.GetBytes(passwd, 0, 8, Passwd_Bytes, 0)
                : Encoding.ASCII.GetBytes(passwd, 0, passwd.Length, Passwd_Bytes, 0);

            // VNC uses DES, not 3DES as written in some documentation
            DES des = new DESCryptoServiceProvider
            {
                Padding = PaddingMode.None,
                Mode = CipherMode.ECB
            };

            ICryptoTransform enc = des.CreateEncryptor(rawKey, null);

            byte[] passwd_enc = new byte[8];
            enc.TransformBlock(Passwd_Bytes, 0, Passwd_Bytes.Length, passwd_enc, 0);
            string ret = "";

            for (int i = 0; i < 8; i++)
            {
                ret += passwd_enc[i].ToString("x2");
            }
            return ret;
        }

        /// <summary>VNC DES authentication has a bug, such that keys are reversed.  This code 
        /// was written by Dominic Ullmann (dominic_ullmann@swissonline.ch) and is 
        /// is being used under the GPL.</summary>
        /// <param name="desKey">The key to be altered.</param>
        /// <returns>Returns the fixed key as an array of bytes.</returns>
        public static byte[] FixDESBug(byte[] desKey)
        {
            byte[] newkey = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                // revert desKey[i]:
                newkey[i] = (byte)(
                    ((desKey[i] & 0x01) << 7) |
                    ((desKey[i] & 0x02) << 5) |
                    ((desKey[i] & 0x04) << 3) |
                    ((desKey[i] & 0x08) << 1) |
                    ((desKey[i] & 0x10) >> 1) |
                    ((desKey[i] & 0x20) >> 3) |
                    ((desKey[i] & 0x40) >> 5) |
                    ((desKey[i] & 0x80) >> 7)
                    );
            }
            return newkey;
        }
    }
}
