using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.Repository.Database
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual ICollection<ApplicationUserProfile> ApplicationUserProfiles { get; set; } = new List<ApplicationUserProfile>();
    }
}
