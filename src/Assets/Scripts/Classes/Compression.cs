using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public class Compression
{
    public static void CopyTo(Stream src, Stream dest)
    {
        byte[] bytes = new byte[4096];

        int cnt;

        while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
        {
            dest.Write(bytes, 0, cnt);
        }
    }

    public static string BrotliString(string str)
    {
        using (var msi = new MemoryStream(Encoding.UTF8.GetBytes(str)))
        using (var mso = new MemoryStream())
        {
            using (var gs = new BrotliSharpLib.BrotliStream(mso, CompressionMode.Compress))
                CopyTo(msi, gs);

            return Convert.ToBase64String(mso.ToArray());
        }
    }

    public static string DeBrotliString(string str)
    {
        using (var msi = new MemoryStream(Convert.FromBase64String(str)))
        using (var mso = new MemoryStream())
        {
            using (var gs = new BrotliSharpLib.BrotliStream(msi, CompressionMode.Decompress))
                CopyTo(gs, mso);

            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }

}
