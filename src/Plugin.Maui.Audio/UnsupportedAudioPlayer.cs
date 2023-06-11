namespace Plugin.Maui.Audio;

internal sealed class UnsupportedAudioPlayer : IAudioPlayer
{
	public void Dispose()
	{
		throw new NotImplementedException();
	}

	public double Volume
	{
		get => throw new NotImplementedException();
		set => throw new NotImplementedException();
	}

	public bool IsPlaying => throw new NotImplementedException();

	public void Play()
	{
		throw new NotImplementedException();
	}

	public void Pause()
	{
		throw new NotImplementedException();
	}
}