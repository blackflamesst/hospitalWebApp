using Hospital.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<HospitalContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", async (HospitalContext context) =>
{
    var doctors = await context.Doctors
        .OrderBy(d => d.Id)
        .Select(d => d.Name)
        .ToListAsync();

    if (doctors == null || !doctors.Any())
    {
        return Results.Text("No doctors found.");
    }

    return Results.Json(doctors);
});

app.Run();
