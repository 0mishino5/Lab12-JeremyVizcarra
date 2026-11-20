using Hangfire;
using Hangfire.PostgreSql;
using Lab12_JeremyVizcarra.Filters;
using Lab12_JeremyVizcarra.Models;
using Lab12_JeremyVizcarra.Repositories;
using Lab12_JeremyVizcarra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Lab12DbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));

builder.Services.AddHangfireServer();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ICleanupService, CleanupService>();
builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ManejadorDeExcepciones>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Lab12 - Hangfire", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Lab12DbContext>();
    var script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "NotificacionesBD.sql"));
    context.Database.ExecuteSqlRaw(script);
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab12 - Hangfire v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAutorizacion() }
});

RecurringJob.AddOrUpdate<INotificationService>("job-notificacion-diaria",
    service => service.SendNotification("usuario_diario", "Recurrente"), Cron.Daily);

RecurringJob.AddOrUpdate<ICleanupService>("job-limpieza-notificaciones",
    service => service.CleanOldNotifications(), Cron.Daily);

app.MapControllers();

app.Run();
