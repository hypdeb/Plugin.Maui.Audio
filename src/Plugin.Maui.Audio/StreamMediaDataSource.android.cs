#if ANDROID

using Android.Media;
using Stream = System.IO.Stream;

namespace Plugin.Maui.Audio;

internal class StreamMediaDataSource : MediaDataSource
{
	private Stream? data;

	public StreamMediaDataSource(Stream data)
	{
		this.data = data;
	}

	public override long Size => this.data?.Length ?? 0;

	public override int ReadAt(long position, byte[]? buffer, int offset, int size)
	{
		ArgumentNullException.ThrowIfNull(buffer);

		this.data?.Seek(position, SeekOrigin.Begin);

		return this.data?.Read(buffer, offset, size) ?? 0;
	}

	public override void Close()
	{
		this.data?.Dispose();
		this.data = null;
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		this.data?.Dispose();
		this.data = null;
	}
}

#endif