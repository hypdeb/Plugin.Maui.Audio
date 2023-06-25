using Microsoft.Extensions.DependencyInjection;

namespace Plugin.Maui.Audio;

public static class AudioExtensions
{
	public static IServiceCollection AddMauiAudioPlugin(this IServiceCollection services)
	{
		services.AddSingleton<IAudioManager, AudioManager>();
		return services;
	}
}