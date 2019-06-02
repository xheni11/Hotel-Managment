
using System;
using System.ComponentModel.DataAnnotations;

namespace M19G1.DAL.Entities
{
    public partial class UserRequest
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
