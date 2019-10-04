using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public class Hospital
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(4000)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
