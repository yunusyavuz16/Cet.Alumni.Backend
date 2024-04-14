using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? DisplayName { get; set; }

    public bool? IsAdmin { get; set; }

    public virtual ICollection<AlumniRole> AlumniRoles { get; set; } = new List<AlumniRole>();
}
