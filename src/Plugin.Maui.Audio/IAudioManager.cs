namespace Plugin.Maui.Audio;

/// <summary>
/// Provides the ability to create <see cref="IAudioPlayer" /> instances.
/// </summary>
public interface IAudioManager
{
	/// <summary>
	/// Creates a new <see cref="IAudioPlayer" /> with the supplied <paramref name="audioStream" /> ready to play.
	/// </summary>
	/// <param name="audioStream">The <see cref="Stream" /> containing the audio to play.</param>
	/// <returns>A new <see cref="IAudioPlayer" /> with the supplied <paramref name="audioStream" /> ready to play.</returns>
	Task<IAudioPlayer> CreatePlayerAsync(Stream audioStream);
}

public sealed class AudioManager : IAudioManager
{
	/// <inheritdoc />
	public async Task<IAudioPlayer> CreatePlayerAsync(Stream audioStream)
	{
#if ANDROID
		var player = new AndroidAudioPlayer();
		await player.SetAndPrepareAsync(audioStream).ConfigureAwait(false);
		return player;
#elif IOS
		return new IosAudioPlayer(audioStream);
#elif WINDOWS
		return new WindowsAudioPlayer(audioStream);
#else
		return new UnsupportedAudioPlayer();
#endif
	}
}