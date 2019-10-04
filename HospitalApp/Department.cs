using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public class Department
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }
        [ForeignKey("ParentDepartmentId")]
        public virtual Department ParentDepartment { get; set; }

        public virtual ICollection<Department> ChildDepartments { get; set; }

        public int HospitalId { get; set; }
        [ForeignKey("HospitalId")]
        public virtual Hospital Hospital { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
