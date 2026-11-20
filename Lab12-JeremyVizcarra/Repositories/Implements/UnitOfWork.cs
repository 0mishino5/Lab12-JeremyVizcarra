using Lab12_JeremyVizcarra.Models;

namespace Lab12_JeremyVizcarra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lab12DbContext _context;

    public INotificationRepository Notifications { get; }

    public UnitOfWork(Lab12DbContext context)
    {
        _context = context;
        Notifications = new NotificationRepository(context);
    }

    public int GuardarCambios()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
