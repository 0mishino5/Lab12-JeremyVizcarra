using Hangfire;
using Lab12_JeremyVizcarra.Models;

namespace Lab12_JeremyVizcarra.Services;

public interface INotificationService
{
    List<Notification> GetAll();
    Notification GetById(int id);
    void SendNotification(string user, string type);

    [AutomaticRetry(Attempts = 3, DelaysInSeconds = new[] { 10, 10, 10 })]
    void SendFailedNotification(string user);
}
