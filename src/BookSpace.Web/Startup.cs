using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookSpace.Web.Models;
using BookSpace.Web.Services;
using BookSpace.Data;
using BookSpace.Data.Contracts;
using BookSpace.Factories;
using BookSpace.Models;
using BookSpace.Repositories;
using BookSpace.Repositories.Contracts;
using Ninject;
using Ninject.Extensions.Factory;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System.Threading;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using Microsoft.AspNetCore.Http;

namespace BookSpace.Web
{
    public class Startup
    {

        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
        private IKernel kernel { get; set; }

        private object Resolve(Type type) => kernel.Get(type);
        private object RequestScope(IContext context) => scopeProvider.Value;

        private sealed class Scope : DisposableObject { }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookSpaceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookSpaceContext>()
                .AddDefaultTokenProviders();

            //Repositories 
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();



            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());

            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);

            services.AddMvc();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            this.kernel = this.RegisterApplicationComponents(app);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            var kernel = new StandardKernel();

            kernel.Load(new FuncModule());

            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }


            kernel.Bind<IApplicationUserFactory>()
                .ToFactory()
                .InSingletonScope();

            kernel.Bind<IBookFactory>()
                .ToFactory()
                .InSingletonScope();

            kernel.Bind<IAuthorFactory>()
                .ToFactory()
                .InSingletonScope();

            kernel.Bind<IGenreFactory>()
                .ToFactory()
                .InSingletonScope();

            kernel.Bind<ICommentFactory>()
                .ToFactory()
                .InSingletonScope();

            kernel.Bind<ITagFactory>()
                .ToFactory()
                .InSingletonScope();

            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }
    }
}
