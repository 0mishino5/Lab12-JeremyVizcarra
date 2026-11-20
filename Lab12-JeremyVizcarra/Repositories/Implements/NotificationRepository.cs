using Lab12_JeremyVizcarra.Models;

namespace Lab12_JeremyVizcarra.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly Lab12DbContext _context;

    public NotificationRepository(Lab12DbContext context)
    {
        _context = context;
    }

    public List<Notification> GetAll()
    {
        var notifications = _context.Notifications.OrderByDescending(n => n.SentAt).ToList();
        return notifications;
    }

    public Notification? GetById(int id)
    {
        var notification = _context.Notifications.Find(id);
        return notification;
    }

    public List<Notification> GetOlderThan(DateTime date)
    {
        var notifications = _context.Notifications.Where(n => n.SentAt < date).ToList();
        return notifications;
    }

    public void Add(Notification notification)
    {
        _context.Notifications.Add(notification);
    }

    public void RemoveRange(List<Notification> notifications)
    {
        _context.Notifications.RemoveRange(notifications);
    }
}
