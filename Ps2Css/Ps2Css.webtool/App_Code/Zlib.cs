using System.IO;
using Ionic.Zlib;

public static class Zlib
{
	private const int BUFFER_SIZE = 4 * 1024;

	public static byte[] Compress(byte[] value)
	{
		using(MemoryStream oms = new MemoryStream())
		{
			using(Stream ims = new MemoryStream(value))
			using(Stream zcs = new ZlibStream(oms, CompressionMode.Compress))
			{
				ims.CopyTo(zcs, BUFFER_SIZE);
			}
			return oms.ToArray();
		}
	}
	public static byte[] Decompress(byte[] value)
	{
		using(MemoryStream oms = new MemoryStream())
		{
			using(Stream ims = new MemoryStream(value))
			using(Stream zds = new ZlibStream(ims, CompressionMode.Decompress))
			{
				zds.CopyTo(oms, BUFFER_SIZE);
			}
			return oms.ToArray();
		}
	}

	private static void CopyTo(this Stream source, Stream destination, int bufferSize)
	{
		byte[] buffer = new byte[bufferSize];
		int bytesRead;
		while((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
		{
			destination.Write(buffer, 0, bytesRead);
		}
	}
}
