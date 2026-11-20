using Hangfire.Dashboard;

namespace Lab12_JeremyVizcarra.Filters;

public class HangfireAutorizacion : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}
