using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace M19G1.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public bool Enabled { get; set; }
    }
   
}