using System.ComponentModel.DataAnnotations;

namespace StudentManagmentSystem.Model
{
    public class ApplicationUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
        public string EmailId { get; set; } = null!;

        [Required(ErrorMessage = "Address1 is required.")]
        [StringLength(100, ErrorMessage = "Address1 cannot exceed 100 characters.")]
        public string Address1 { get; set; } = null!;

        public string? Address2 { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string MobileNumber { get; set; } = null!;

        [Required(ErrorMessage = "Role is required.")]
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public bool isDelete { get; set; }

        public IEnumerable<DropdownViewModel>? Roles { get; set; }

        public IEnumerable<DropdownViewModel>? Departments { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._-]{4,20}$", ErrorMessage = "Username must be 4-20 characters and can contain letters, numbers, periods, underscores, or dashes.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; } = null!;
    }
}
