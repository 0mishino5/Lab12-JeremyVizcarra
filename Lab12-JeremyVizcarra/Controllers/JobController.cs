using Lab12_JeremyVizcarra.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab12_JeremyVizcarra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _service;

    public JobController(IJobService service)
    {
        _service = service;
    }

    [HttpPost("fire-and-forget")]
    public IActionResult FireAndForget(string user)
    {
        return Ok(new { jobId = _service.EnqueueNotification(user) });
    }

    [HttpPost("delayed")]
    public IActionResult Delayed(string user, int minutes = 10)
    {
        return Ok(new { jobId = _service.ScheduleNotification(user, minutes) });
    }

    [HttpPost("failed")]
    public IActionResult Failed(string user)
    {
        return Ok(new { jobId = _service.EnqueueFailedNotification(user) });
    }
}
