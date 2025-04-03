using Hospital.Context;
using Microsoft.EntityFrameworkCore;

namespace Hospital
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<HospitalContext>(options => options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

            builder.WebHost.UseUrls("http://localhost:5267");

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseCors("AllowAll");

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapFallbackToFile("index.html");

            app.Run();

        }
    }
}
