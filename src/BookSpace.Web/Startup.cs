﻿using System;
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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookSpace.Web.Controllers;

namespace BookSpace.Web
{
    public class Startup
    {

        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
        private IKernel kernel { get; set; }
        private IServiceProvider provider;

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

            services.AddScoped<IDbContext>(serviceProvider => (BookSpaceContext)provider.GetService(typeof(BookSpaceContext)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());

            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);

            services.AddMvc();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            this.provider = provider;
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

            kernel.Load<FuncModule>();

            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }


            kernel.Bind<IDbContext>()
                .To<BookSpaceContext>()
                .InScope(RequestScope)
                .WithConstructorArgument(typeof(DbContextOptions), provider.GetService(typeof(DbContextOptions)));

            kernel.Bind<UserManager<ApplicationUser>>()
                  .ToMethod((context => this.Get<UserManager<ApplicationUser>>()))
                  .InThreadScope();
                        

            kernel.Bind<SignInManager<ApplicationUser>>()
                 .ToMethod((context => this.Get<SignInManager<ApplicationUser>>()))
                 .InThreadScope();


            kernel.Bind<IEmailSender>()
                .To<EmailSender>()
                .InThreadScope();


            // Repositories
            kernel.Bind(typeof(IRepository<>))
                .To(typeof(BaseRepository<>))
                .InScope(RequestScope);

            kernel.Bind<IApplicationUserRepository>()
                .To<ApplicationUserRepository>()
                .InScope(RequestScope);

            kernel.Bind<IBookRepository>()
               .To<BookRepository>()
               .InScope(RequestScope);


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

        private T Get<T>()
        {
            return (T)this.provider.GetService(typeof(T));
        }
    }
}
