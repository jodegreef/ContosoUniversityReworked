using Autofac;
using Autofac.Features.Variance;
using AutoMapper;
using ContosoUniversity.DAL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ContosoUniversity.Infrastructure
{
    public static class Bootstrap
    {
        public static IContainer Create()
        {
            return Create(null);
        }

        public static IContainer Create(Action<ContainerBuilder> additionalRegistrations)
        {
            IContainer container = null;

            var builder = new ContainerBuilder();


            //query handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces();

            //mediator
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            //db context
            builder.Register<SchoolContext>(c => new SchoolContext());


            //automapper
            RegisterAutomapper(builder);

            if (additionalRegistrations != null)
                additionalRegistrations.Invoke(builder);


            container = builder.Build();


            return container;
        }

        private static void RegisterAutomapper(ContainerBuilder builder)
        {
            var profiles =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }
    }
}