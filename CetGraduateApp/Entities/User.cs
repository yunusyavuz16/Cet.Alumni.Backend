using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime? LastSignedInDateTime { get; set; }

    public bool IsVerified { get; set; }

    public virtual ICollection<UserRegistration> UserRegistrations { get; set; } = new List<UserRegistration>();
}
