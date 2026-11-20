using Lab12_JeremyVizcarra.Models;

namespace Lab12_JeremyVizcarra.Repositories;

public interface INotificationRepository
{
    List<Notification> GetAll();
    Notification? GetById(int id);
    List<Notification> GetOlderThan(DateTime date);
    void Add(Notification notification);
    void RemoveRange(List<Notification> notifications);
}
