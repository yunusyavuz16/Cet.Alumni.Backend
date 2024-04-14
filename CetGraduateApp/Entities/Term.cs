using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class Term
{
    public int TermId { get; set; }

    public int? TermYear { get; set; }

    public virtual ICollection<Alumni> Alumni { get; set; } = new List<Alumni>();
}
