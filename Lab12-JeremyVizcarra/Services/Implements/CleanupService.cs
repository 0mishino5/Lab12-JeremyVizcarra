using Lab12_JeremyVizcarra.Repositories;

namespace Lab12_JeremyVizcarra.Services;

public class CleanupService : ICleanupService
{
    private readonly IUnitOfWork _unitOfWork;

    public CleanupService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CleanOldNotifications()
    {
        var date = DateTime.Now.AddDays(-30);
        var notifications = _unitOfWork.Notifications.GetOlderThan(date);

        _unitOfWork.Notifications.RemoveRange(notifications);
        _unitOfWork.GuardarCambios();

        Console.WriteLine($"Limpieza terminada: {notifications.Count} notificaciones eliminadas en {DateTime.Now}");
    }
}
