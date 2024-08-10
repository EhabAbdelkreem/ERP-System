using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.CRM.ViewModel
{
    public class ContactCreatingViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string NameAR { get; set; }
        [StringLength(50), RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers")]
        public string Phone1 { get; set; }
        [StringLength(50), RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers")]
        public string Phone2 { get; set; }
        [StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string AccNum { get; set; }
        public bool CreateAccount { get; set; }
    }
}
