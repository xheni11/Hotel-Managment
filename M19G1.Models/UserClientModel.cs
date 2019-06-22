using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Models
{
    public class UserClientModel
    {
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(30, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        public bool Active { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        public string RoleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string HashedPassword { get; set; }
    }

}
