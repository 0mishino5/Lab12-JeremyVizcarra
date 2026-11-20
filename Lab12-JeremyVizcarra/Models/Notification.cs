using System;
using System.Collections.Generic;

namespace Lab12_JeremyVizcarra.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime SentAt { get; set; }
}
