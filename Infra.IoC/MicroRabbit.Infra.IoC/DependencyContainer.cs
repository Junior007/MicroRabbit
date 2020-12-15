using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using MicroRabbit.Banking.Data.Context;
using MediatR;
using System.Reflection;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.CommandHandlers;
using System;
using System.Linq;
using MicroRabbit.Transfer.Application.Services;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Domain.EventHandlers;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain bus
            services.AddSingleton<IEventBus, RabbitMQBus>(
                sp => {
                    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    return new RabbitMQBus(sp.GetRequiredService<IMediator>(), scopeFactory);
                }
                );

            //subscriptions in Event Bus
            services.AddTransient<TransferEventHandler>();


            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var assembly = assemblies.Where(ass => ass.FullName.Contains("MicroRabbit.Banking.Domain")).FirstOrDefault();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = assemblies.Where(ass => ass.FullName.Contains("MicroRabbit.Banking.Domain") || ass.FullName.Contains("MicroRabbit.Transfer.Domain")).ToArray();
            services.AddMediatR(assembly);


            //Banking command
            //services.AddTransient <IRequestHandler<CreateTransferCommand>, TransferCommandHandler>();

            //Application services
            //Banking
            services.AddTransient<IAccountService, AccountService>();

            //Transfer
            services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
            //
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<TransferDbContext>();


        }
    }
}
