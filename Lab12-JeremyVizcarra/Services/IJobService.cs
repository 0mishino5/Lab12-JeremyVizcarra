namespace Lab12_JeremyVizcarra.Services;

public interface IJobService
{
    string EnqueueNotification(string user);
    string ScheduleNotification(string user, int minutes);
    string EnqueueFailedNotification(string user);
}
