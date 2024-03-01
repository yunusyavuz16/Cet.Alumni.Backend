using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentCode { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }
}
