#if WINDOWS
using Microsoft.VisualBasic;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage.Streams;

namespace Plugin.Maui.Audio;

internal sealed class WindowsAudioPlayer : IAudioPlayer
{
	private readonly MediaPlayer player;
	private readonly IRandomAccessStream stream;

	public WindowsAudioPlayer(Stream audioStream)
	{
		this.player = this.CreatePlayer();
		var stream = new MemoryStream();
		audioStream.CopyToAsync(stream);
		this.SetVolume(1);
		stream.Seek(0, SeekOrigin.Begin);
		this.stream = stream.AsRandomAccessStream();
		this.player.Source = MediaSource.CreateFromStream(this.stream, "audio/mpeg");
	}

	public double Volume
	{
		get => this.player.Volume;
		set => this.SetVolume(value);
	}

	public bool IsPlaying => this.player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing; //might need to expand

	public void Play()
	{
		if (this.player.Source is null)
		{
			return;
		}

		if (this.player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing)
		{
			this.Pause();
		}

		this.player.Play();
	}

	public void Pause()
	{
		this.player.Pause();
	}

	private void SetVolume(double volume)
	{
		this.player.Volume = Math.Clamp(volume, 0, 1);
	}

	private MediaPlayer CreatePlayer() => new() {AutoPlay = false, IsLoopingEnabled = false};

	public void Dispose()
	{
		this.stream.Dispose();
		this.player.Dispose();
	}
}
#endif