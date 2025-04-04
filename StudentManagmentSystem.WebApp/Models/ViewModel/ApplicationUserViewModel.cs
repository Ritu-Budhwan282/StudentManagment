using System.ComponentModel;

namespace StudentManagmentSystem.WebApp.Models.ViewModel
{
    public class ApplicationUserViewModel
    {
       
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string EmailId { get; set; } = null!;

        public string Address1 { get; set; } = null!;

        public string? Address2 { get; set; }

        public string MobileNumber { get; set; } = null!;
        

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public bool isDelete { get; set; }
        public IEnumerable<DropdownViewModel> Roles { get; set; } 
        public IEnumerable<DropdownViewModel> Departments { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
