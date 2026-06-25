using Microsoft.Extensions.DependencyInjection;

// lista usług
public static class DependencyInjection
{
	public static IServiceProvider Build()
	{
		var services = new ServiceCollection();
		RegisterCore(services);
		RegisterApplication(services);
		RegisterNode(services);

		return services.BuildServiceProvider();
	}

	private static void RegisterCore(IServiceCollection services) { }

	private static void RegisterApplication(IServiceCollection services) { }

	private static void RegisterNode(IServiceCollection services)
	{
		services.AddTransient<MainMenuPresenter>();
	}
}
