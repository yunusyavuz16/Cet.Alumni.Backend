using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class AlumniRole
{
    public int AlumniRoleId { get; set; }

    public int? AlumniStudentNo { get; set; }

    public int? RoleId { get; set; }

    public virtual Alumni? AlumniStudentNoNavigation { get; set; }

    public virtual Role? Role { get; set; }
}
