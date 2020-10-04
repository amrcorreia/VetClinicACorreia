using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;


namespace VetClinicACorreia.Web
{                           
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(cfg =>
           {
               cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
               cfg.SignIn.RequireConfirmedEmail = true;
               cfg.User.RequireUniqueEmail = true;
               cfg.Password.RequireDigit = false;
               cfg.Password.RequiredUniqueChars = 0;
               cfg.Password.RequireLowercase = true;
               cfg.Password.RequireNonAlphanumeric = true;
               cfg.Password.RequireUppercase = true;
               cfg.Password.RequiredLength = 6;
           })
           .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = this.Configuration["Tokens:Issuer"],
                        ValidAudience = this.Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(this.Configuration["Tokens:Key"]))
                    };
                });

            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddTransient<SeedDb>();

            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddScoped<IConverterHelper, ConverterHelper>();
            services.AddScoped<ICombosHelper, CombosHelper>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IAppointmentHelper, AppointmentHelper>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IVetAssistantRepository, VetAssistantRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ISpecialityRepository, SpecialityRepository>();
            services.AddScoped<IAppRepository, AppRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Account/NotAuthorized"; TODO esta opção é uma alternativa ao redirecionalemnto ao Login
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
