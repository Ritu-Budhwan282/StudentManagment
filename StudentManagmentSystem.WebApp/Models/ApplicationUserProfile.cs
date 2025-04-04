using System;
using System.Collections.Generic;

namespace StudentManagmentSystem.WebApp.Models;

public partial class ApplicationUserProfile
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string MobileNumber { get; set; } = null!;

    public int RoleId { get; set; }

    public int DepartmentId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ApplicationRole Role { get; set; } = null!;

    public virtual UserAccount? UserAccount { get; set; }
}
