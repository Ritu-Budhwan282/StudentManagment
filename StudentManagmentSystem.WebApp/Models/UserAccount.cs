using System;
using System.Collections.Generic;

namespace StudentManagmentSystem.WebApp.Models;

public partial class UserAccount
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ApplicationUserProfile IdNavigation { get; set; } = null!;
}
