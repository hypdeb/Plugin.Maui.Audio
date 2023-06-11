#if IOS
using AVFoundation;
using Foundation;

namespace Plugin.Maui.Audio;

internal sealed class IosAudioPlayer : IAudioPlayer
{
	private readonly AVAudioPlayer player;
	private bool isDisposed;

	public double Volume
	{
		get => this.player.Volume;
		set => this.player.Volume = (float) Math.Clamp(value, 0, 1);
	}

	public bool IsPlaying => this.player.Playing;

	internal IosAudioPlayer(Stream audioStream)
	{
		var data = NSData.FromStream(audioStream)!;
		this.player = AVAudioPlayer.FromData(data);

		this.PreparePlayer();
	}

	public void Play()
	{
		if (this.player.Playing)
		{
			this.player.Pause();
		}
		else
		{
			this.player.Play();
		}
	}

	public void Pause() => this.player.Pause();


	public void Dispose()
	{
		if (this.isDisposed)
		{
			return;
		}
		this.Pause();

		this.player.Dispose();
		this.isDisposed = true;
	}

	private bool PreparePlayer()
	{
		this.player.EnableRate = true;
		this.player.PrepareToPlay();

		return true;
	}
}
#endif