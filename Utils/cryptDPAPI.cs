using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Text;

class cryptDPAPI
{
    private const int CRYPTPROTECT_UI_FORBIDDEN = 0x1;
    // Wrapper for the NULL handle or pointer.
    static private IntPtr NullPtr = ((IntPtr)((int)(0)));
    // Wrapper for DPAPI CryptProtectData function.
    [DllImport("crypt32.dll", SetLastError = true,
    CharSet = CharSet.Auto)]
    private static extern bool CryptProtectData(
    ref DATA_BLOB pPlainText,
    [MarshalAs(UnmanagedType.LPWStr)]string szDescription, IntPtr pEntroy, IntPtr pReserved, IntPtr pPrompt, int dwFlags,
    ref DATA_BLOB pCipherText);
    // BLOB structure used to pass data to DPAPI functions.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct DATA_BLOB
    {
        public int cbData;
        public IntPtr pbData;
    }

    private static void InitBLOB(byte[] data, ref DATA_BLOB blob)
    {
        blob.pbData = Marshal.AllocHGlobal(data.Length);
        if (blob.pbData == IntPtr.Zero) throw new Exception("Unable to allocate buffer for BLOB data.");

        blob.cbData = data.Length;
        Marshal.Copy(data, 0, blob.pbData, data.Length);
    }

    public static string Encrypt(string plainText)
    {
        byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);
        DATA_BLOB plainTextBlob = new DATA_BLOB();
        DATA_BLOB cipherTextBlob = new DATA_BLOB();
        StringBuilder cipherString = new StringBuilder();
        try
        {
            try
            {
                InitBLOB(plainTextBytes, ref plainTextBlob);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot initialize dataIn BLOB.", ex);
            }

            bool success = CryptProtectData(
            ref plainTextBlob,
            "psw",
            NullPtr,
            NullPtr,
            NullPtr,
            CRYPTPROTECT_UI_FORBIDDEN,
            ref cipherTextBlob);

            if (!success)
            {
                int errCode = Marshal.GetLastWin32Error();
                throw new Exception("CryptProtectData failed.", new Win32Exception(errCode));
            }

            byte[] cipherTextBytes = new byte[cipherTextBlob.cbData];
            Marshal.Copy(cipherTextBlob.pbData, cipherTextBytes, 0, cipherTextBlob.cbData);
            // Convert hex data to hex characters (suitable for a string)
            for (int i = 0; i < cipherTextBlob.cbData; i++) cipherString.Append(Convert.ToString(cipherTextBytes[i], 16).PadLeft(2, '0').ToUpper());
        }
        catch (Exception ex)
        {
            throw new Exception("unable to encrypt data.", ex);
        }
        finally
        {
            if (plainTextBlob.pbData != IntPtr.Zero) Marshal.FreeHGlobal(plainTextBlob.pbData);

            if (cipherTextBlob.pbData != IntPtr.Zero) Marshal.FreeHGlobal(cipherTextBlob.pbData);
        }
        return cipherString.ToString();
    }
}