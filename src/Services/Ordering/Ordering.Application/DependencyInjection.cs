﻿using BuildingBlocks.Behevior;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
   public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register application services here
            // Example: services.AddScoped<IOrderService, OrderService>();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehevior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
            services.AddFeatureManagement();
            return services;
        }
    }
}
