using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class AlumniRegistration
{
    public int UserRegistrationId { get; set; }

    public string? RegistrationCode { get; set; }

    public int? AlumniStudentNo { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public virtual Alumni? AlumniStudentNoNavigation { get; set; }
}
