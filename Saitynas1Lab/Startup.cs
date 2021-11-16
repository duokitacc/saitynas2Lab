using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saitynas1Lab.Data;
using Saitynas1Lab.Data.Repositories;

namespace Saitynas1Lab
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DemoRestContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            
            services.AddTransient<IPostsRepository, PostsRepository>();
            services.AddTransient<IReviewsRepository, ReviewsRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
