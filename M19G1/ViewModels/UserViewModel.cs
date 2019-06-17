using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M19G1.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(30, MinimumLength = 3,
        ErrorMessage = "Firstname Should be minimum 3 characters and a maximum of 30 characters")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(30, MinimumLength = 3,
        ErrorMessage = "Lastname Should be minimum 3 characters and a maximum of 30 characters")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        public bool Active { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3,
        ErrorMessage = "Username Should be minimum 3 characters and a maximum of 30 characters")]
        public string Username { get; set; }
        [Required]
        public List<string> RoleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
    }
}