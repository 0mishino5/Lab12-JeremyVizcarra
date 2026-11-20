namespace Lab12_JeremyVizcarra.Repositories;

public interface IUnitOfWork : IDisposable
{
    INotificationRepository Notifications { get; }
    int GuardarCambios();
}
