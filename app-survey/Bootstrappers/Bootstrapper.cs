using System.Configuration;
using System.Data.Entity.Infrastructure.Annotations;
using Autofac;
using AutofacSerilogIntegration;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using Nancy.Json;
using Nancy.TinyIoc;
using Serilog;
using SurveyService.Dal;
using SurveyService.DataStores;

namespace SurveyService.Bootstrappers
{
    /// <summary>
    /// Nancy/Autofac bootstrapper. Registers instances of all dependencies that can be injected to controllers
    /// of classes.
    /// </summary>
    /// <seealso cref="Nancy.Bootstrappers.Autofac.AutofacNancyBootstrapper" />
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            Log.Logger = new LoggerConfiguration()
              .ReadFrom.AppSettings()
              .Enrich.FromLogContext()
              .Enrich.WithMachineName()
              .Enrich.WithProcessId()
              .Enrich.WithThreadId()
              .Enrich.WithProperty("ServiceName", "SurveyService")
              .CreateLogger();            
        }
        #endregion

        #region methods


        /// <summary>
        /// Gets the application container.
        /// </summary>
        /// <returns>The global <see cref="ILifetimeScope"/> of the container</returns>
        /// <remarks>
        /// This is required since <see cref="IStartable"/> components will only get fired when the container gets built, not on container updates.
        /// </remarks>
        protected override ILifetimeScope GetApplicationContainer()
        {
            // Add a view location convention that looks for views in a folder
            //  named "views" next to the module class
            //
            //this.Conventions.ViewLocationConventions.Add((viewName, model, context) => $"Website/Modules/{context.ModuleName}/views/{viewName}");

            // Add a new path for static content so our typescript files located in
            //  the 'App' folder can be served to SystemJS
            //                                    
            this.Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("app"));
            this.Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("node_modules"));


            // Consul 
            var builder = new ContainerBuilder();            
            builder.RegisterLogger();       
            
            // register DAL            
            var connectionString = ConfigurationManager.ConnectionStrings["SurveyService"].ConnectionString;

            builder.RegisterType<SurveyDatabaseContext>()
                .WithParameter("nameOrConnectionString", connectionString)
                .InstancePerLifetimeScope();          

            builder.RegisterType<QuestionStore>().As<IQuestionStore>();
            builder.RegisterType<SurveyStore>().As<ISurveyStore>();            
            builder.RegisterType<AnswerStore>().As<IAnswerStore>();            
            builder.RegisterType<SurveyDashboardStore>().As<ISurveyDashboardStore>();          

            return builder.Build();
        }

        /// <summary>
        /// Configure the request container
        /// </summary>
        /// <param name="container">Request container instance</param>
        /// <param name="context"></param>
        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            var builder = new ContainerBuilder();  

            builder.RegisterType<JavaScriptSerializer>();                                 
            

            builder.Update(container.ComponentRegistry);
            base.ConfigureRequestContainer(container, context);
        }

        #endregion
    }
}