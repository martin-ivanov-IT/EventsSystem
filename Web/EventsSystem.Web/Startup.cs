namespace EventsSystem.Web
{
    using System.Reflection;

    using EventsSystem.Data;
    using EventsSystem.Data.Common;
    using EventsSystem.Data.Common.Repositories;
    using EventsSystem.Data.Models;
    using EventsSystem.Data.Repositories;
    using EventsSystem.Data.Seeding;
    using EventsSystem.Services.Data;
    using EventsSystem.Services.Mapping;
    using EventsSystem.Services.Messaging;
    using EventsSystem.Web.Hubs;
    using EventsSystem.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IPlacesService, PlacesService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IPlaceVotesService, PlaceVotesService>();
            services.AddTransient<IEventVotesService, EventVotesService>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/chat");

                        // endpoints.MapControllerRoute("eventRoute", "/{name}", new { controller = "Events", action = "EventsByName" });
                        endpoints.MapControllerRoute("allEvents", "page/", new { controller = "AllEvents", action = "ShowAllEvents" });
                        endpoints.MapControllerRoute("allEventsByCity", "page/", new { controller = "Events", action = "ShowAllEventsByCity" });
                        endpoints.MapControllerRoute("eventRouteId", "event/{id}", new { controller = "Events", action = "ById" });
                        endpoints.MapControllerRoute("placeRoute", "p/{name}", new { controller = "Places", action = "ByName" });
                        endpoints.MapControllerRoute("placeRouteId", "place/{id}", new { controller = "Places", action = "ById" });
                        endpoints.MapControllerRoute("eventForm", "/f/{name}", new { controller = "CreateEvent", action = "FillForm" });
                        endpoints.MapControllerRoute("eventForm", "r/{name}", new { controller = "Review", action = "AddReviewToPlace" });
                        endpoints.MapControllerRoute("Chat", "chats/", new { controller = "Chat", action = "Friends" });

                        // endpoints.MapControllerRoute("allEventsRouteWithPage", "/test/allEvents/{page:int}", new { controller = "AllEvents", action = "ShowAllEvents" });
                        // endpoints.MapControllerRoute("allEventsRoute", "/test/allEvents", new { controller = "AllEvents", action = "ShowAllEvents" });
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
