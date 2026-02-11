

using MasterApplication.Server.Authorization;

namespace MasterApplication.Server.IOC
{
	public class IoCConfiguration
	{
		public static void Configuration(IServiceCollection services)
		{
			Configure(services, MasterApplication.DB.IOC.Module.GetTypes());
			services.AddScoped(typeof(IJwtManager), typeof(JwtManager));
		}

		private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
		{
			foreach (var type in types)
			{
				services.AddScoped(type.Key, type.Value);
			}
		}
	}
}
