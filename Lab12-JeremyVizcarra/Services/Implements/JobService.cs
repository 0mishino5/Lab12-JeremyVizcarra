using Hangfire;

namespace Lab12_JeremyVizcarra.Services;

public class JobService : IJobService
{
    public string EnqueueNotification(string user)
    {
        ValidateUser(user);
        var jobId = BackgroundJob.Enqueue<INotificationService>(service => service.SendNotification(user, "FireAndForget"));
        return jobId;
    }

    public string ScheduleNotification(string user, int minutes)
    {
        ValidateUser(user);
        if (minutes <= 0)
        {
            throw new ReglaNegocioException("Los minutos deben ser mayores a cero");
        }

        var jobId = BackgroundJob.Schedule<INotificationService>(service => service.SendNotification(user, "Delayed"), TimeSpan.FromMinutes(minutes));
        return jobId;
    }

    public string EnqueueFailedNotification(string user)
    {
        ValidateUser(user);
        var jobId = BackgroundJob.Enqueue<INotificationService>(service => service.SendFailedNotification(user));
        return jobId;
    }

    private void ValidateUser(string user)
    {
        if (string.IsNullOrWhiteSpace(user))
        {
            throw new ReglaNegocioException("El usuario es obligatorio");
        }
    }
}
