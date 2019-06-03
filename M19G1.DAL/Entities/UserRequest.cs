using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public class UserRequest:BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? DateCreated { get; set; }
        [StringLength(1)]
        public string Gender { get; set; }
        public AspNetRole Role { get; set; }
        public int RoleId { get; set; }
    }
}
