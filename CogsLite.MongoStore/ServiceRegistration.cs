using System;
using System.Collections.Generic;
using System.Text;
using CogsLite.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CogsLite.MongoStore
{
    public class ServiceRegistration
    {
		public static void Register(IServiceCollection services)
		{
			services.AddTransient<IGameStore, GameStore>();
			services.AddTransient<IImageStore, ImageStore>();
			services.AddTransient<ICardStore, CardStore>();
		}
    }
}
