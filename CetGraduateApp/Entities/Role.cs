using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string DisplayName { get; set; } = null!;

    public string RoleCode { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
