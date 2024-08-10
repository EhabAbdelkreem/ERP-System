using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.HR.Model
{
    [Table("HR_Department")]
    public class Department
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string DepartmentName { get; set; }
    }
}
