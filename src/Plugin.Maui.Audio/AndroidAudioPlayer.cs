#if ANDROID
using Android.Media;
using Stream = System.IO.Stream;

namespace Plugin.Maui.Audio;

internal sealed class AndroidAudioPlayer : IAudioPlayer
{
	private readonly MediaPlayer player;

	internal AndroidAudioPlayer()
	{
		this.player = new MediaPlayer();
	}

	public bool IsPlaying => this.player.IsPlaying;

	public double Volume
	{
		get => this.Volume;
		set => this.SetVolume(value);
	}

	public void Play()
	{
		if (this.IsPlaying)
		{
			this.Pause();
		}

		this.player.Start();
	}

	public void Pause()
	{
		this.player.Pause();
	}

	public void Dispose()
	{
		this.player.Release();
		this.player.Dispose();
	}

	public async Task SetAndPrepareAsync(Stream audioStream)
	{
		var mediaDataSource = new StreamMediaDataSource(audioStream);
		await this.player.SetDataSourceAsync(mediaDataSource).ConfigureAwait(false);
		this.player.Looping = true;
		var taskCompletionSource = new TaskCompletionSource();
		this.player.Prepared += (sender, args) => { taskCompletionSource.SetResult(); };
		this.player.Prepare();
		await taskCompletionSource.Task.ConfigureAwait(false);
	}

	private void SetVolume(double volume)
	{
		volume = Math.Clamp(volume, 0, 1);


		// Using the "constant power pan rule." See: http://www.rs-met.com/documents/tutorials/PanRules.pdf
		var left = Math.Cos(Math.PI / 4) * volume;
		var right = Math.Sin(Math.PI / 4) * volume;

		this.player.SetVolume((float) left, (float) right);
	}
}

#endif