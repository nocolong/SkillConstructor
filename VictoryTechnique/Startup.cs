using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VictoryTechnique.Core.Domain;
using VictoryTechnique.Core.Infrastructure;
using VictoryTechnique.Core.Repository;
using VictoryTechnique.Data.Infrastructure;
using VictoryTechnique.Data.Repository;
using VictoryTechnique.Infrastructure;

[assembly: OwinStartup(typeof(VictoryTechnique.Startup))]
namespace VictoryTechnique
{
    public class Startup
    {
        public void Configuration(Owin.IAppBuilder app)
        {
            var container = ConfigureSimpleInjector(app);
            ConfigureOAuth(app, container);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, Container container)
        {
            Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;

            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
   
                Provider = new VictoryTechniqueAuthorizationServerProvider(authRepositoryFactory)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(authorizationOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public Container ConfigureSimpleInjector(IAppBuilder app)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();

            container.Register<IAreaOfStudyRepository, AreaOfStudyRepository>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IVidCritiqueRepository, VidCritiqueRepository>();
            container.Register<IVidCritiqueTagRepository, VidCritiqueTagRepository>();
            container.Register<IVidSubmissionRepository, VidSubmissionRepository>();
            container.Register<IVidSubmissionTagRepository, VidSubmissionTagRepository>();
            container.Register<IUserStore<User, int>, UserStore>(Lifestyle.Scoped); 
            container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);
            //container.Register<IKeyPaymentService, StripeKeyPaymentService>();

            // more code to facilitate a scoped lifestyle
            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Verify();

            return container;
        }
    }
}