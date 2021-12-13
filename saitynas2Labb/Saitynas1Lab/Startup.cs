using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Saitynas1Lab.Auth;
using Saitynas1Lab.Auth.Model;
using Saitynas1Lab.Data;
using Saitynas1Lab.Data.Dtos.Auth;
using Saitynas1Lab.Data.Repositories;
using System.Text;

namespace Saitynas1Lab
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<DemoRestUser,IdentityRole>()
                .AddEntityFrameworkStores<DemoRestContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options => {
                 options.TokenValidationParameters.ValidAudience = _configuration["JWT:ValidAudience"];
                 options.TokenValidationParameters.ValidIssuer = _configuration["JWT:ValidIssuer"];
                 options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
             });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();

            services.AddDbContext<DemoRestContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            
            services.AddTransient<IPostsRepository, PostsRepository>();
            services.AddTransient<IReviewsRepository, ReviewsRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<DatabaseSeeder, DatabaseSeeder>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
