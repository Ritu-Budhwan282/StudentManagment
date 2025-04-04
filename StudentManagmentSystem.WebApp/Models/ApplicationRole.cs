using System;
using System.Collections.Generic;

namespace StudentManagmentSystem.WebApp.Models;

public partial class ApplicationRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ApplicationUserProfile> ApplicationUserProfiles { get; set; } = new List<ApplicationUserProfile>();
}
