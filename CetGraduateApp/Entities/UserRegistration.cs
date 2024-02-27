using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class UserRegistration
{
    public int UserRegistrationId { get; set; }

    public string RegistrationCode { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime CreateDateTime { get; set; }

    public virtual User User { get; set; } = null!;
}
