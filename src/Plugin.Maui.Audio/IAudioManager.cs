namespace Plugin.Maui.Audio;

/// <summary>
/// Provides the ability to create <see cref="IAudioPlayer" /> instances.
/// </summary>
public interface IAudioManager
{
	/// <summary>
	/// Creates a new <see cref="IAudioPlayer"/> with the supplied <paramref name="audioStream"/> ready to play.
	/// </summary>
	/// <param name="audioStream">The <see cref="Stream"/> containing the audio to play.</param>
	/// <returns>A new <see cref="IAudioPlayer"/> with the supplied <paramref name="audioStream"/> ready to play.</returns>
	IAudioPlayer CreatePlayer(Stream audioStream);
}

public sealed class AudioManager : IAudioManager
{
	/// <inheritdoc />
	public IAudioPlayer CreatePlayer(Stream audioStream)
	{
		#if ANDROID
		return new AndroidAudioPlayer(audioStream);
		#elif IOS
		return new IosAudioPlayer(audioStream);
		#elif WINDOWS
		return new WindowsAudioPlayer(audioStream);
		#else
		return new UnsupportedAudioPlayer();
		#endif
	}
}