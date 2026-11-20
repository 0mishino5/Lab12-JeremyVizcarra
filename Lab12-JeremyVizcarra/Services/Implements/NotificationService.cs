using Lab12_JeremyVizcarra.Models;
using Lab12_JeremyVizcarra.Repositories;

namespace Lab12_JeremyVizcarra.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<Notification> GetAll()
    {
        var notifications = _unitOfWork.Notifications.GetAll();
        return notifications;
    }

    public Notification GetById(int id)
    {
        var notification = _unitOfWork.Notifications.GetById(id);
        if (notification == null)
        {
            throw new RecursoNoEncontradoException($"No existe la notificacion {id}");
        }

        return notification;
    }

    public void SendNotification(string user, string type)
    {
        var notification = new Notification
        {
            Username = user,
            Message = $"Notificacion enviada a {user}",
            Type = type,
            SentAt = DateTime.Now
        };

        _unitOfWork.Notifications.Add(notification);
        _unitOfWork.GuardarCambios();

        Console.WriteLine($"Notificacion enviada a {user} en {DateTime.Now}");
    }

    public void SendFailedNotification(string user)
    {
        Console.WriteLine($"Intento fallido de notificar a {user} en {DateTime.Now}");
        throw new InvalidOperationException($"No se pudo notificar a {user}");
    }
}
