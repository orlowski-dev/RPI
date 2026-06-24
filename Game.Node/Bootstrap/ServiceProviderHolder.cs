using System;

public static class ServiceProviderHolder
{
    public static IServiceProvider Provider { get; private set; } = null!;

    public static void Init(IServiceProvider provider)
    {
        Provider = provider;
    }
}
